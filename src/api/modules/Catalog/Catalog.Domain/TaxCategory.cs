using FSH.Framework.Core.Domain.Contracts;
using FSH.Framework.Core.Domain;

namespace FSH.Starter.WebApi.Catalog.Domain;

public class TaxCategory : BaseEntity<string>, IAggregateRoot
{
    public string Id { get; set; } = default!;
    public decimal Percent { get; set; } = 0;
    public string? TaxExemptionReason { get; set; }
    public string? TaxExemptionReasonCode { get; set; }
    public string TaxSchemeId { get; set; } = default!;
    public string? TaxSchemeName { get; set; }
}
