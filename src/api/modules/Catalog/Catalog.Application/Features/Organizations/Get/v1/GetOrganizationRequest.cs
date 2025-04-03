using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Organizations.Get.v1;
public class GetOrganizationRequest : IRequest<OrganizationResponse>
{
    public int Id { get; set; }
    public GetOrganizationRequest(int id) => Id = id;
}
