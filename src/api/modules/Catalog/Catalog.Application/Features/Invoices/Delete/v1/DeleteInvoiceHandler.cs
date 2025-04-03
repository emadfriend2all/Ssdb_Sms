using FSH.Framework.Core.Persistence;
using FSH.Starter.WebApi.Catalog.Domain;
using FSH.Starter.WebApi.Catalog.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Invoices.Delete.v1;
public sealed class DeleteInvoiceHandler(
    ILogger<DeleteInvoiceHandler> logger,
    [FromKeyedServices("catalog:invoices")] IRepository<Invoice> repository)
    : IRequestHandler<DeleteInvoiceCommand>
{
    public async Task Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var invoice = await repository.GetByIdAsync(request.Id, cancellationToken);
        _ = invoice ?? throw new InvoiceNotFoundException(request.Id);
        await repository.DeleteAsync(invoice, cancellationToken);
        logger.LogInformation("invoice with id : {InvoiceId} deleted", invoice.Id);
    }
}
