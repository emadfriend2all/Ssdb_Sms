using System.Xml.Serialization;

namespace FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Invoices;
public class InvoiceLineItem
{
    public int Id { get; set; }
    public decimal Quantity { get; set; }
    public decimal LineExtensionAmount { get; set; }
    public decimal VatAmount { get; set; }
    public decimal NetAmount { get; set; }
    public decimal AmountInclusiveVAT { get; init; }
    public string? TaxCategoryId { get; set; }
    public decimal? TaxCategoryPercent { get; set; }
    public TaxCategory? LineTaxCategory { get; set; }
    public List<Allowance> Allowances { get; set; }
    public int ItemId { get; set; }
    public decimal ItemPrice { get; set; }
    public string ItemName { get; set; } = default!;

}

public class Allowance
{
    public bool ChargeIndicator { get; set; }
    public string AllowanceChargeReason { get; set; }
    public decimal Amount { get; set; } = 0;
    public string? TaxCategoryId { get; set; }
    public TaxCategory? TaxCategory { get; set; }
}

