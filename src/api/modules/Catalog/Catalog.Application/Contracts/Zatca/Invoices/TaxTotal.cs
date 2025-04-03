
namespace FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Invoices;

public class TaxTotal
{
    public decimal TaxAmount { get; set; }
    public decimal RoundingAmount { get; set; }
    public List<TaxSubtotal>? TaxSubtotals { get; set; }
}

public class TaxSubtotal
{
    public decimal TaxableAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public string? TaxCategoryId { get; set; }
    public decimal? TaxCategoryPercent { get; set; }
    public string? TaxExemptionReason { get; set; }
    public string? TaxExemptionReasonCode { get; set; }
    public string? TaxSchemeId { get; set; }
    public TaxCategory? Category { get; set; }
}

public class TaxCategory
{
    public string Id { get; set; } = default!;
    public decimal Percent { get; set; } = 0;
    public string? TaxExemptionReason { get; set; }
    public string? TaxExemptionReasonCode { get; set; }
    public string? TaxSchemeId { get; set; }
    public string? TaxSchemeName { get; set; }
}
