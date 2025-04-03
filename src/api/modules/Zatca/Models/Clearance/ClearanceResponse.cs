using Shared.Contracts.Zatca.Shared;

namespace Shared.Contracts.Zatca.Clearance;

public class ClearanceResponse
{
    public string? ClearanceStatus { get; set; }
    public string? ClearedInvoice { get; set; }
    public string? InvoiceHash { get; set; }
    public ValidationResults? ValidationResults { get; set; }

}
