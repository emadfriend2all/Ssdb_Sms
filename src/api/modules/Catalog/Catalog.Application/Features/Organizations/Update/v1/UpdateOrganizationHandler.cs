using FSH.Framework.Core.Persistence;
using FSH.Starter.WebApi.Catalog.Domain;
using FSH.Starter.WebApi.Catalog.Domain.Exceptions;
using Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Organizations.Update.v1;
public sealed class UpdateOrganizationHandler(
    ILogger<UpdateOrganizationHandler> logger,
    [FromKeyedServices("catalog:organizations")] IRepository<Organization> repository)
    : IRequestHandler<UpdateOrganizationCommand, UpdateOrganizationResponse>
{
    public async Task<UpdateOrganizationResponse> Handle(UpdateOrganizationCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var organization = await repository.GetByIdAsync(request.Id, cancellationToken);
        _ = organization ?? throw new OrganizationNotFoundException(request.Id);
        var updatedOrganization = request.Adapt<Organization>();
        await repository.UpdateAsync(updatedOrganization, cancellationToken);
        logger.LogInformation("organization with id : {OrganizationId} updated.", organization.Id);
        return new UpdateOrganizationResponse(organization.Id);
    }
}
