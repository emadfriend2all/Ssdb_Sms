﻿using Showmatics.Blazor.Client.Components.EntityTable;
using Showmatics.Blazor.Client.Infrastructure.ApiClient;
using Showmatics.Blazor.Client.Infrastructure.Auth;
using Showmatics.Blazor.Client.Shared;
using Showmatics.Shared.Authorization;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Microsoft.AspNetCore.Components.Authorization;

namespace Showmatics.Blazor.Client.Pages.Multitenancy;

public partial class Tenants
{
    [Inject]
    private ITenantsClient TenantsClient { get; set; } = default!;
    private string? _searchString;
    protected EntityClientTableContext<TenantDetail, string, CreateTenantRequest> Context { get; set; } = default!;
    private List<TenantDetail> _tenants = new();
    public EntityTable<TenantDetail, string, CreateTenantRequest> EntityTable { get; set; } = default!;
    [CascadingParameter]
    protected Task<AuthenticationState> AuthState { get; set; } = default!;
    [Inject]
    protected IAuthorizationService AuthService { get; set; } = default!;

    private bool _canUpgrade;
    private bool _canModify;

    protected override async Task OnInitializedAsync()
    {
        Context = new(
            entityName: L["Tenant"],
            entityNamePlural: L["Tenants"],
            entityResource: FSHResource.Tenants,
            searchAction: FSHAction.View,
            deleteAction: string.Empty,
            updateAction: FSHAction.Update,
            fields: new()
            {
                new(tenant => tenant.Id, L["Id"]),
                new(tenant => tenant.Name, L["Name"]),
                new(tenant => tenant.AdminEmail, L["Admin Email"]),
                new(tenant => tenant.PhoneNumber, L["PhoneNumber"]),
                new(tenant => tenant.ValidUpto.ToString("MMM dd, yyyy"), L["Valid Upto"]),
                new(tenant => tenant.IsActive, L["Active"], Type: typeof(bool))
            },
            idFunc: tenant => tenant.Id,
            loadDataFunc: async () => _tenants = (await TenantsClient.GetListAsync()).Adapt<List<TenantDetail>>(),
            searchFunc: (searchString, tenantDto) =>
                string.IsNullOrWhiteSpace(searchString)
                    || tenantDto.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase),
            createFunc: tenant => TenantsClient.CreateAsync(tenant),
            updateFunc: async (id, request) => await TenantsClient.UpdateAsync(id, request.Adapt<UpdateTenantRequest>()),
            hasExtraActionsFunc: () => true,
            exportAction: string.Empty);

        var state = await AuthState;
        _canUpgrade = await AuthService.HasPermissionAsync(state.User, FSHAction.UpgradeSubscription, FSHResource.Tenants);
        _canModify = await AuthService.HasPermissionAsync(state.User, FSHAction.Update, FSHResource.Tenants);
    }

    private void ViewTenantDetails(string id)
    {
        var tenant = _tenants.First(f => f.Id == id);
        tenant.ShowDetails = !tenant.ShowDetails;
        foreach (var otherTenants in _tenants.Except(new[] { tenant }))
        {
            otherTenants.ShowDetails = false;
        }
    }

    private async Task ViewUpgradeSubscriptionModalAsync(string id)
    {
        var tenant = _tenants.First(f => f.Id == id);
        var parameters = new DialogParameters
        {
            {
                nameof(UpgradeSubscriptionModal.Request),
                new UpgradeSubscriptionRequest
                {
                    TenantId = tenant.Id,
                    ExtendedExpiryDate = tenant.ValidUpto
                }
            }
        };
        var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
        var dialog = DialogService.Show<UpgradeSubscriptionModal>(L["Upgrade Subscription"], parameters, options);
        var result = await dialog.Result;
        if (!result.Cancelled)
        {
            await EntityTable.ReloadDataAsync();
        }
    }

    private async Task DeactivateTenantAsync(string id)
    {
        if (await ApiHelper.ExecuteCallGuardedAsync(
            () => TenantsClient.DeactivateAsync(id),
            Snackbar,
            null,
            L["Tenant Deactivated."]) is not null)
        {
            await EntityTable.ReloadDataAsync();
        }
    }

    private async Task ActivateTenantAsync(string id)
    {
        if (await ApiHelper.ExecuteCallGuardedAsync(
            () => TenantsClient.ActivateAsync(id),
            Snackbar,
            null,
            L["Tenant Activated."]) is not null)
        {
            await EntityTable.ReloadDataAsync();
        }
    }

    public class TenantDetail : TenantDto
    {
        public bool ShowDetails { get; set; }
    }
}