using FSH.Framework.Core.Domain.Contracts;
using FSH.Framework.Core.Domain;

namespace FSH.Starter.WebApi.Catalog.Domain;

public class TaxTotal : BaseEntity<int>, IAggregateRoot
{
    public decimal TaxAmount { get; set; }
    public decimal RoundingAmount { get; set; }
    public string? TaxCategoryId { get; set; }
    public TaxCategory? TaxCategory { get; set; }
    public List<TaxSubtotal>? TaxSubtotals { get; set; }
    
}
