using FSH.Starter.WebApi.Catalog.Application.Features.TaxCategories.Create.v1;
using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.Features.TaxCategories.Update.v1;
public sealed class UpdateTaxCategoryCommand : IRequest<UpdateTaxCategoryResponse>
{
    public string Id { get; set; } = default!;
    public decimal Percent { get; set; } = 0;
    public string? TaxExemptionReason { get; set; }
    public string? TaxExemptionReasonCode { get; set; }
    public int TaxSchemeId { get; set; } = default!;
    public string? TaxSchemeName { get; set; }
}
