using FSH.Framework.Core.Paging;
using FSH.Starter.WebApi.Catalog.Application.Features.Products.Get.v1;
using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Products.Search.v1;

public class SearchProductsCommand : PaginationFilter, IRequest<PagedList<ProductResponse>>
{
    public int? BrandId { get; set; }
    public decimal? MinimumRate { get; set; }
    public decimal? MaximumRate { get; set; }
}
