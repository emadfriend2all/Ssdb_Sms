using System.ComponentModel;
using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.Features.TaxCategories.Create.v1;
public sealed class CreateTaxCategoryCommand: IRequest<CreateTaxCategoryResponse>
{
    public string Id { get; set; } = default!;
    public decimal Percent { get; set; } = 0;
    public string? TaxExemptionReason { get; set; }
    public string? TaxExemptionReasonCode { get; set; }
    public string TaxSchemeId { get; set; } = default!;
    public string? TaxSchemeName { get; set; }
}
