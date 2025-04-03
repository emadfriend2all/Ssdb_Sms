using FSH.Framework.Core.Paging;
using FSH.Starter.WebApi.Catalog.Application.Features.Organizations.Get.v1;
using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Organizations.Search.v1;

public class SearchOrganizationsCommand : PaginationFilter, IRequest<PagedList<OrganizationResponse>>
{
    public string? OrganizationIdentifier { get; set; }
    public string? OrganizationName { get; set; }
    public bool? IsMyOrganization { get; set; }
}
