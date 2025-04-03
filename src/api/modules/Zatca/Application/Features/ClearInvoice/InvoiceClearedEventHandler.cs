using FSH.Starter.WebApi.Catalog.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FSH.Starter.WebApi.Zatca.Application.Features.ClearInvoice;
public class InvoiceClearedEventHandler(ILogger<InvoiceClearedEventHandler> logger,IMediator mediator) : INotificationHandler<InvoiceCleared>
{
    public async Task Handle(InvoiceCleared notification,
        CancellationToken cancellationToken)
    {
        await mediator.Send(notification);
        logger.LogInformation("handling product created domain event..");
        await Task.FromResult(notification);
        logger.LogInformation("finished handling product created domain event..");
    }
}
