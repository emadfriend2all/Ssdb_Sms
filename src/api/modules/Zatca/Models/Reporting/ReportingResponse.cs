using Shared.Contracts.Zatca.Shared;

namespace Shared.Contracts.Zatca.Reporting;

public class ReportingResponse
{
    public string? ReportingStatus { get; set; }
    public ValidationResults? ValidationResults { get; set; }
}
