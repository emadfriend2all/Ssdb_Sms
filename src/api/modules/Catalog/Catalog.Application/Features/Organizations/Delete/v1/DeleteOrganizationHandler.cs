using FSH.Framework.Core.Persistence;
using FSH.Starter.WebApi.Catalog.Domain;
using FSH.Starter.WebApi.Catalog.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Organizations.Delete.v1;
public sealed class DeleteOrganizationHandler(
    ILogger<DeleteOrganizationHandler> logger,
    [FromKeyedServices("catalog:organizations")] IRepository<Organization> repository)
    : IRequestHandler<DeleteOrganizationCommand>
{
    public async Task Handle(DeleteOrganizationCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var organization = await repository.GetByIdAsync(request.Id, cancellationToken);
        _ = organization ?? throw new OrganizationNotFoundException(request.Id);
        await repository.DeleteAsync(organization, cancellationToken);
        logger.LogInformation("organization with id : {OrganizationId} deleted", organization.Id);
    }
}
