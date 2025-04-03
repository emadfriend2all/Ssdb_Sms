using Shared.Contracts.Zatca.Shared;

namespace Shared.Contracts.Zatca.Compliance;

public class ValidationResponse
{
    public ValidationResults? ValidationResults { get; set; }
    public string? ReportingStatus { get; set; }
    public string? ClearanceStatus { get; set; }
    public string? QrSellerStatus { get; set; }
    public string? QrBuyerStatus { get; set; }
}
