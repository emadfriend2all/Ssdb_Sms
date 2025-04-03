using FSH.Framework.Core.Tenant.Dtos;
using FSH.Framework.Core.Tenant.Features.CreateTenant;
using Shared;

namespace FSH.Framework.Core.Tenant.Abstractions;

public interface ITenantService
{
    Task<List<TenantDetail>> GetAllAsync();

    Task<bool> ExistsWithIdAsync(string id);

    Task<bool> ExistsWithNameAsync(string name);

    Task<TenantDetail> GetByIdAsync(string id);

    Task<string> CreateAsync(CreateTenantCommand request, CancellationToken cancellationToken);

    Task<string> ActivateAsync(string id, CancellationToken cancellationToken);

    Task<string> DeactivateAsync(string id);

    Task<DateTime> UpgradeSubscription(string id, DateTime extendedExpiryDate);
    Task<int> TopUp(string id, int balance);

    Task<int> Charge(string id, int amount);
    Task<TenantBasicInfoDto> GetBasicTenantInfoAsync(string id);
}
