using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Organizations.Update.v1;
public sealed record UpdateOrganizationCommand : IRequest<UpdateOrganizationResponse>
{
    public int Id { get; set; }
    public string CommonName { get; set; } = default!;
    public string SerialNumber { get; set; } = default!;
    public string OrganizationIdentifier { get; set; } = default!;
    public string OrganizationUnitName { get; set; } = default!;
    public string OrganizationName { get; set; } = default!;
    public string CountryName { get; set; } = default!;
    public string InvoiceType { get; set; } = default!;
    public string LocationAddress { get; set; } = default!;
    public string IndustryBusinessCategory { get; set; } = default!;
}
