using System.Diagnostics.Metrics;
using FSH.Starter.Aspire.ServiceDefaults;

namespace FSH.Starter.WebApi.Zatca.Domain;

public static class ZatcaMetrics
{
    private static readonly Meter Meter = new Meter(MetricsConstants.Todos, "1.0.0");
    public static readonly Counter<int> Created = Meter.CreateCounter<int>("items.created");
    public static readonly Counter<int> Updated = Meter.CreateCounter<int>("items.updated");
    public static readonly Counter<int> Deleted = Meter.CreateCounter<int>("items.deleted");
}

