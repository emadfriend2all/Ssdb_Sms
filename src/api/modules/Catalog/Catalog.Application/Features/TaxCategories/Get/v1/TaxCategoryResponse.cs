namespace FSH.Starter.WebApi.Catalog.Application.Features.TaxCategories.Get.v1;
public sealed record TaxCategoryResponse{
    public string Id { get; set; }
    public decimal Percent { get; set; } = 0;
    public string? TaxExemptionReason { get; set; }
    public string? TaxExemptionReasonCode { get; set; }
    public string TaxSchemeId { get; set; } = default!;
    public string? TaxSchemeName { get; set; }
}

