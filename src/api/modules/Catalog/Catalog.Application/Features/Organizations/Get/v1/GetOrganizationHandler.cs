using Microsoft.Extensions.DependencyInjection;
using FSH.Starter.WebApi.Catalog.Domain.Exceptions;
using FSH.Framework.Core.Persistence;
using FSH.Framework.Core.Caching;
using FSH.Starter.WebApi.Catalog.Domain;
using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Organizations.Get.v1;
public sealed class GetOrganizationHandler(
    [FromKeyedServices("catalog:organizations")] IReadRepository<Organization> repository,
    ICacheService cache)
    : IRequestHandler<GetOrganizationRequest, OrganizationResponse>
{
    public async Task<OrganizationResponse> Handle(GetOrganizationRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var item = await cache.GetOrSetAsync(
            $"organization:{request.Id}",
            async () =>
            {
                var spec = new GetOrganizationSpecs(request.Id);
                var organizationItem = await repository.FirstOrDefaultAsync(spec, cancellationToken);
                if (organizationItem == null) throw new OrganizationNotFoundException(request.Id);
                return organizationItem;
            },
            cancellationToken: cancellationToken);
        return item!;
    }
}
