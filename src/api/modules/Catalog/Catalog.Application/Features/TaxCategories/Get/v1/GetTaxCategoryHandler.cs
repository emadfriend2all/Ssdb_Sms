using Microsoft.Extensions.DependencyInjection;
using FSH.Starter.WebApi.Catalog.Domain.Exceptions;
using FSH.Framework.Core.Persistence;
using FSH.Framework.Core.Caching;
using FSH.Starter.WebApi.Catalog.Domain;
using MediatR;
using Mapster;

namespace FSH.Starter.WebApi.Catalog.Application.Features.TaxCategories.Get.v1;
public sealed class GetTaxCategoryHandler(
    [FromKeyedServices("catalog:taxCategories")] IReadRepository<TaxCategory> repository,
    ICacheService cache)
    : IRequestHandler<GetTaxCategoryRequest, TaxCategoryResponse>
{
    public async Task<TaxCategoryResponse> Handle(GetTaxCategoryRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var item = await cache.GetOrSetAsync(
            $"taxCategory:{request.Code}",
            async () =>
            {
                var taxSchemeItem = await repository.GetByIdAsync(request.Code, cancellationToken);
                if (taxSchemeItem == null) throw new TaxCategoryNotFoundException(request.Code);
                return taxSchemeItem.Adapt<TaxCategoryResponse>();
            },
            cancellationToken: cancellationToken);
        return item!;
    }
}
