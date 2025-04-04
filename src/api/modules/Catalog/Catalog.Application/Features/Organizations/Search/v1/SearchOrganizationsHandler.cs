using FSH.Framework.Core.Paging;
using FSH.Framework.Core.Persistence;
using FSH.Starter.WebApi.Catalog.Application.Features.Organizations.Get.v1;
using FSH.Starter.WebApi.Catalog.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;


namespace FSH.Starter.WebApi.Catalog.Application.Features.Organizations.Search.v1;
public sealed class SearchOrganizationsHandler(
    [FromKeyedServices("catalog:organizations")] IReadRepository<Organization> repository)
    : IRequestHandler<SearchOrganizationsCommand, PagedList<OrganizationResponse>>
{
    public async Task<PagedList<OrganizationResponse>> Handle(SearchOrganizationsCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var spec = new SearchOrganizationSpecs(request);

        var items = await repository.ListAsync(spec, cancellationToken).ConfigureAwait(false);
        var totalCount = await repository.CountAsync(spec, cancellationToken).ConfigureAwait(false);

        return new PagedList<OrganizationResponse>(items, request!.PageNumber, request!.PageSize, totalCount);
    }
}

