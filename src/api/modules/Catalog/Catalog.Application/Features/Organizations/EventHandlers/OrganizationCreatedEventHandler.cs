using FSH.Starter.WebApi.Catalog.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Organizations.EventHandlers;

public class OrganizationCreatedEventHandler(ILogger<OrganizationCreatedEventHandler> logger) : INotificationHandler<OrganizationCreated>
{
    public async Task Handle(OrganizationCreated notification,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("handling organization created domain event..");
        await Task.FromResult(notification);
        logger.LogInformation("finished handling organization created domain event..");
    }
}

