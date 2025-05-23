﻿using Showmatics.Blazor.Client.Infrastructure.ApiClient;
using Showmatics.Blazor.Client.Infrastructure.Notifications;
using Showmatics.Blazor.Client.Shared;
using Showmatics.Shared.Notifications;
using MediatR.Courier;
using Microsoft.AspNetCore.Components;

namespace Showmatics.Blazor.Client.Pages.Personal;

public partial class Dashboard
{
    [Parameter]
    public int ItemCount { get; set; }
    [Parameter]
    public int BrandCount { get; set; }
    [Parameter]
    public int UserCount { get; set; }
    [Parameter]
    public int RoleCount { get; set; }

    [Inject]
    private IDashboardClient DashboardClient { get; set; } = default!;
    [Inject]
    private ICourier Courier { get; set; } = default!;

    private readonly string[] _dataEnterBarChartXAxisLabels = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
    private readonly List<MudBlazor.ChartSeries> _dataEnterBarChartSeries = new();
    private bool _loaded;

    protected override async Task OnInitializedAsync()
    {
        Courier.SubscribeWeak<NotificationWrapper<StatsChangedNotification>>(async _ =>
        {
            await LoadDataAsync();
            StateHasChanged();
        });

        await LoadDataAsync();

        _loaded = true;
    }

    private async Task LoadDataAsync()
    {
        if (await ApiHelper.ExecuteCallGuardedAsync(
                () => DashboardClient.GetAsync(),
                Snackbar)
            is StatsDto statsDto)
        {
            ItemCount = statsDto.ItemCount;
            BrandCount = statsDto.BrandCount;
            UserCount = statsDto.UserCount;
            RoleCount = statsDto.RoleCount;
            foreach (var item in statsDto.DataEnterBarChart)
            {
                _dataEnterBarChartSeries
                    .RemoveAll(x => x.Name.Equals(item.Name, StringComparison.OrdinalIgnoreCase));
                _dataEnterBarChartSeries.Add(new MudBlazor.ChartSeries { Name = item.Name, Data = item.Data?.ToArray() });
            }
        }
    }
}