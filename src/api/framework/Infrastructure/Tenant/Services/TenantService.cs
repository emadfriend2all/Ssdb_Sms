﻿using Finbuckle.MultiTenant;
using Finbuckle.MultiTenant.Abstractions;
using FSH.Framework.Core.Exceptions;
using FSH.Framework.Core.Persistence;
using FSH.Framework.Core.Tenant.Abstractions;
using FSH.Framework.Core.Tenant.Dtos;
using FSH.Framework.Core.Tenant.Features.CreateTenant;
using FSH.Framework.Infrastructure.Tenant.Persistence;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shared;

namespace FSH.Framework.Infrastructure.Tenant.Services;

public sealed class TenantService : ITenantService
{
    private readonly IMultiTenantStore<FshTenantInfo> _tenantStore;
    private readonly DatabaseOptions _config;
    private readonly IServiceProvider _serviceProvider;

    public TenantService(IMultiTenantStore<FshTenantInfo> tenantStore, IOptions<DatabaseOptions> config, IServiceProvider serviceProvider)
    {
        _tenantStore = tenantStore;
        _config = config.Value;
        _serviceProvider = serviceProvider;
    }

    public async Task<string> ActivateAsync(string id, CancellationToken cancellationToken)
    {
        var tenant = await GetTenantInfoAsync(id).ConfigureAwait(false);

        if (tenant.IsActive)
        {
            throw new FshException($"tenant {id} is already activated");
        }

        tenant.Activate();

        await _tenantStore.TryUpdateAsync(tenant).ConfigureAwait(false);

        return $"tenant {id} is now activated";
    }

    public async Task<string> CreateAsync(CreateTenantCommand request, CancellationToken cancellationToken)
    {
        var connectionString = request.ConnectionString;
        if (request.ConnectionString?.Trim() == _config.ConnectionString.Trim())
        {
            connectionString = string.Empty;
        }

        FshTenantInfo tenant = new(request.Id, request.Name, connectionString, request.AdminEmail, request.SubscriptionType, request.Address, request.PhoneNumber, request.Logo, request.PrimaryColor, request.SecondaryColor, request.ReportHeader, request.ReportFooter, request.Issuer);
        tenant.Organization = request.Organization.Adapt<FshTenantOrganization>();
        
        await _tenantStore.TryAddAsync(tenant).ConfigureAwait(false);

        await InitializeDatabase(tenant).ConfigureAwait(false);

        return tenant.Id;
    }

    private async Task InitializeDatabase(FshTenantInfo tenant)
    {
        // First create a new scope
        using var scope = _serviceProvider.CreateScope();

        // Then set current tenant so the right connection string is used
        scope.ServiceProvider.GetRequiredService<IMultiTenantContextSetter>()
            .MultiTenantContext = new MultiTenantContext<FshTenantInfo>()
            {
                TenantInfo = tenant
            };

        // using the scope, perform migrations / seeding
        var initializers = scope.ServiceProvider.GetServices<IDbInitializer>();
        foreach (var initializer in initializers)
        {
            await initializer.MigrateAsync(CancellationToken.None).ConfigureAwait(false);
            await initializer.SeedAsync(CancellationToken.None).ConfigureAwait(false);
        }
    }

    public async Task<string> DeactivateAsync(string id)
    {
        var tenant = await GetTenantInfoAsync(id).ConfigureAwait(false);
        if (!tenant.IsActive)
        {
            throw new FshException($"tenant {id} is already deactivated");
        }

        tenant.Deactivate();
        await _tenantStore.TryUpdateAsync(tenant).ConfigureAwait(false);
        return $"tenant {id} is now deactivated";
    }

    public async Task<bool> ExistsWithIdAsync(string id) =>
        await _tenantStore.TryGetAsync(id).ConfigureAwait(false) is not null;

    public async Task<bool> ExistsWithNameAsync(string name) =>
        (await _tenantStore.GetAllAsync().ConfigureAwait(false)).Any(t => t.Name == name);

    public async Task<List<TenantDetail>> GetAllAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TenantDbContext>();

        var tenants = await dbContext.TenantInfo.Include(t => t.Organization).ToListAsync();
        return tenants.Adapt<List<TenantDetail>>();
    }

    public async Task<TenantDetail> GetByIdAsync(string id) =>
        (await GetTenantInfoAsync(id).ConfigureAwait(false))
            .Adapt<TenantDetail>();

    public async Task<DateTime> UpgradeSubscription(string id, DateTime extendedExpiryDate)
    {
        var tenant = await GetTenantInfoAsync(id).ConfigureAwait(false);
        tenant.SetValidity(extendedExpiryDate);
        await _tenantStore.TryUpdateAsync(tenant).ConfigureAwait(false);
        return tenant.ValidUpto;
    }

    public async Task<int> TopUp(string id, int balance)
    {
        var tenant = await GetTenantInfoAsync(id).ConfigureAwait(false);
        tenant.TopUp(balance);
        await _tenantStore.TryUpdateAsync(tenant).ConfigureAwait(false);
        return tenant.Balance;
    }

    public async Task<int> Charge(string id, int amount)
    {
        var tenant = await GetTenantInfoAsync(id).ConfigureAwait(false);
        tenant.Charge(amount);
        await _tenantStore.TryUpdateAsync(tenant).ConfigureAwait(false);
        return tenant.Balance;
    }

    private async Task<FshTenantInfo> GetTenantInfoAsync(string id)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TenantDbContext>();

        return await dbContext.TenantInfo
            .Include(t => t.Organization)
            .FirstOrDefaultAsync(t => t.Id == id)
            ?? throw new NotFoundException($"{typeof(FshTenantInfo).Name} {id} Not Found.");

    }

    public async Task<TenantBasicInfoDto> GetBasicTenantInfoAsync(string id)
    {
        var tenant = await GetTenantInfoAsync(id).ConfigureAwait(false);
        return tenant.Adapt<TenantBasicInfoDto>();
    }
}
