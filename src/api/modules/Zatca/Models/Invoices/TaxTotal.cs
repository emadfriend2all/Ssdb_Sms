using FSH.Starter.WebApi.Zatca.Application.Features.ClearInvoice.v1;

namespace Shared.Contracts.Zatca.Invoices;

public class TaxTotal
{
    public decimal TaxAmount { get; set; }
    public decimal TaxableAmount { get; set; }
    public TaxCategory? TaxCategory { get; set; }
    public IReadOnlyList<TaxSubtotal> TaxSubtotals { get; set; } = new List<TaxSubtotal>();
}

public class TaxSubtotal
{
    public decimal TaxableAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public TaxCategory TaxCategory { get; set; }
}

public class TaxCategory
{
    public string? CategoryCode { get; set; }
    public decimal Percent { get; set; } = 0;
    public string? TaxExemptionReason { get; set; }
    public string? TaxExemptionReasonCode { get; set; }

    public TaxScheme TaxScheme { get; set; } = new();
}

public class TaxScheme
{
    public string? Code { get; set; }
    public string? Name { get; set; }
}
