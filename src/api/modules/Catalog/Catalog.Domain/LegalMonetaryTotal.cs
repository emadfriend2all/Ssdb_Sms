using FSH.Framework.Core.Domain.Contracts;
using FSH.Framework.Core.Domain;

namespace FSH.Starter.WebApi.Catalog.Domain;

public class LegalMonetaryTotal : BaseEntity<int>, IAggregateRoot
{
    public decimal LineExtensionAmount { get; set; }
    public decimal TaxExclusiveAmount { get; set; }
    public decimal TaxInclusiveAmount { get; set; }
    public decimal AllowanceTotalAmount { get; set; }
    public decimal PrepaidAmount { get; set; }
    public decimal PayableAmount { get; set; }
}
