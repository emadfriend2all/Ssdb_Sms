using Ardalis.Specification;
using FSH.Starter.WebApi.Catalog.Domain;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Organizations.Get.v1;

public class GetOrganizationSpecs : Specification<Organization, OrganizationResponse>
{
    public GetOrganizationSpecs(int id)
    {
        Query
            .Where(p => p.Id == id);
    }
}
