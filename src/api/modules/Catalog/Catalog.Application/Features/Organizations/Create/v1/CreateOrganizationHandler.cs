using FSH.Framework.Core.Persistence;
using FSH.Starter.WebApi.Catalog.Domain;
using Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Organizations.Create.v1;
public sealed class CreateOrganizationHandler(
    ILogger<CreateOrganizationHandler> logger,
    [FromKeyedServices("catalog:organizations")] IRepository<Organization> repository)
    : IRequestHandler<CreateOrganizationCommand, CreateOrganizationResponse>
{
    public async Task<CreateOrganizationResponse> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var organization = request.Adapt<Organization>();
        await repository.AddAsync(organization, cancellationToken);
        logger.LogInformation("organization created {OrganizationId}", organization.Id);
        return new CreateOrganizationResponse(organization.Id);
    }
}
