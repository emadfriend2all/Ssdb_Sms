using FSH.Framework.Core.Domain.Contracts;
using FSH.Framework.Core.Domain;

namespace FSH.Starter.WebApi.Catalog.Domain;

public class TaxSubtotal : BaseEntity<int>, IAggregateRoot
{
    public decimal TaxableAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public string TaxCategoryId { get; set; }
    public int TaxTotalId { get; set; }
    public TaxTotal? TaxTotal { get; set; }
    public TaxCategory TaxCategory { get; set; }
}
