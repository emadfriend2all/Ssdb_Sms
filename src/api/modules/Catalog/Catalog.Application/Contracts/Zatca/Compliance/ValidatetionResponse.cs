using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Shared;

namespace FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Compliance;

public class ValidationResponse
{
    public ValidationResults? ValidationResults { get; set; }
    public string? ReportingStatus { get; set; }
    public string? ClearanceStatus { get; set; }
    public string? QrSellerStatus { get; set; }
    public string? QrBuyerStatus { get; set; }
}
