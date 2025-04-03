using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.Features.TaxCategories.Get.v1;
public class GetTaxCategoryRequest : IRequest<TaxCategoryResponse>
{
    public string Code { get; set; }
    public GetTaxCategoryRequest(string code) => Code = code;
}
