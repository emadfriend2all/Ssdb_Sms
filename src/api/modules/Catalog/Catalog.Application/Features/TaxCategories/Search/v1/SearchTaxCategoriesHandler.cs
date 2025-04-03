using FSH.Framework.Core.Paging;
using FSH.Framework.Core.Persistence;
using FSH.Starter.WebApi.Catalog.Application.Features.TaxCategories.Get.v1;
using FSH.Starter.WebApi.Catalog.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FSH.Starter.WebApi.Catalog.Application.Features.TaxCategories.Search.v1;
public sealed class SearchTaxCategoriesHandler(
    [FromKeyedServices("catalog:taxCategories")] IReadRepository<TaxCategory> repository)
    : IRequestHandler<SearchTaxCategoriesCommand, PagedList<TaxCategoryResponse>>
{
    public async Task<PagedList<TaxCategoryResponse>> Handle(SearchTaxCategoriesCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var spec = new SearchTaxCategoriesSpecs(request);

        var items = await repository.ListAsync(spec, cancellationToken).ConfigureAwait(false);
        var totalCount = await repository.CountAsync(spec, cancellationToken).ConfigureAwait(false);

        return new PagedList<TaxCategoryResponse>(items, request!.PageNumber, request!.PageSize, totalCount);
    }
}
