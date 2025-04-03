using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Shared;

namespace FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Reporting;

public class ReportingResponse
{
    public string? ReportingStatus { get; set; }
    public ValidationResults? ValidationResults { get; set; }
}
