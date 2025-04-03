using System.Linq;
using Ardalis.Specification;
using FSH.Framework.Core.Paging;
using FSH.Framework.Core.Specifications;
using FSH.Starter.WebApi.Catalog.Application.Features.Organizations.Get.v1;
using FSH.Starter.WebApi.Catalog.Domain;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Organizations.Search.v1;
public class SearchOrganizationSpecs : EntitiesByPaginationFilterSpec<Organization, OrganizationResponse>
{
    public SearchOrganizationSpecs(SearchOrganizationsCommand command)
        : base(command) =>
        Query
            .OrderBy(c => c.CommonName, !command.HasOrderBy())
            .Where(p => p.OrganizationIdentifier.Contains(command.OrganizationIdentifier!), !string.IsNullOrEmpty(command.OrganizationIdentifier))
            .Where(p => p.OrganizationName.Contains(command.OrganizationName!), !string.IsNullOrEmpty(command.OrganizationName))
        ;
}
