namespace Shared.Contracts.Zatca.Invoices;
public class LegalMonetaryTotal
{
    public decimal LineExtensionAmount { get; set; }
    public decimal TaxExclusiveAmount { get; set; }
    public decimal TaxInclusiveAmount { get; set; }
    public decimal AllowanceTotalAmount { get; set; }
    public decimal PrepaidAmount { get; set; }
    public decimal PayableAmount { get; set; }
}
