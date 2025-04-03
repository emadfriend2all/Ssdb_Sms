
using FSH.Framework.Core.Caching;
using FSH.Framework.Core.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FSH.Starter.WebApi.Catalog.Domain.Events;
public record OrganizationCreated(int Id, string Title, string Note) : DomainEvent;

public class CompanyCreatedEventHandler(
    ILogger<CompanyCreatedEventHandler> logger,
    ICacheService cache)
    : INotificationHandler<OrganizationCreated>
{
    public async Task Handle(OrganizationCreated notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("handling todo item created domain event..");
        //var cacheResponse = new GetTodoResponse(notification.Id, notification.Title, notification.Note);
        //await cache.SetAsync($"todo:{notification.Id}", cacheResponse, cancellationToken: cancellationToken);
    }
}
