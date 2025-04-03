using FSH.Framework.Core.Domain.Contracts;
using FSH.Framework.Core.Domain;

namespace FSH.Starter.WebApi.Catalog.Domain;

public class InvoiceAllowance : BaseEntity<int>, IAggregateRoot
{
    public bool ChargeIndicator { get; set; }
    public string AllowanceChargeReason { get; set; }
    public decimal Amount { get; set; } = 0;
    public int? InvoiceLineId { get; set; }
    public string TaxCategoryId { get; set; } = string.Empty;
    public TaxCategory TaxCategory { get; set; } = new();
    public int? InvoiceId { get; set; }
}

