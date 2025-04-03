using FSH.Framework.Core.Domain.Contracts;
using FSH.Framework.Core.Domain;

namespace FSH.Starter.WebApi.Catalog.Domain;
public class InvoiceLine : BaseEntity<int>, IAggregateRoot
{
    public decimal Quantity { get; set; }
    public decimal LineExtensionAmount { get; set; }
    public decimal VatAmount { get; set; }
    public decimal NetAmount { get; set; }
    public decimal AmountInclusiveVAT { get; set; }
    public string TaxCategoryId { get; set; }
    public TaxCategory TaxCategory { get; set; } = default!;
    public List<InvoiceLineAllowance>? Allowances { get; set; }
    public int ItemId { get; set; }
    public decimal ItemPrice { get; set; }
    public string ItemName { get; set; } = default!;
    public Product Item { get; set; } = default!;
}

