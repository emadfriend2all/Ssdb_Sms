using FSH.Starter.WebApi.Catalog.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Invoices.EventHandlers;

public class InvoiceCreatedEventHandler(ILogger<InvoiceCreatedEventHandler> logger) : INotificationHandler<InvoiceCleared>
{
    public async Task Handle(InvoiceCleared notification,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("handling invoice created domain event..");
        await Task.FromResult(notification);
        logger.LogInformation("finished handling invoice created domain event..");
    }
}

