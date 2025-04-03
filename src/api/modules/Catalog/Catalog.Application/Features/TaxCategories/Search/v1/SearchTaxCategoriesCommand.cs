using FSH.Framework.Core.Paging;
using FSH.Starter.WebApi.Catalog.Application.Features.TaxCategories.Get.v1;
using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.Features.TaxCategories.Search.v1;

public class SearchTaxCategoriesCommand : PaginationFilter, IRequest<PagedList<TaxCategoryResponse>>
{
    public string? TaxSchemeCode { get; set; }
}
