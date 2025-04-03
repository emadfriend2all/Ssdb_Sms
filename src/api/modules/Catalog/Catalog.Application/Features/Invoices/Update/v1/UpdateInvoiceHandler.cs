using FSH.Framework.Core.Persistence;
using FSH.Starter.WebApi.Catalog.Domain;
using FSH.Starter.WebApi.Catalog.Domain.Exceptions;
using Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Invoices.Update.v1;
public sealed class UpdateInvoiceHandler(
    ILogger<UpdateInvoiceHandler> logger,
    [FromKeyedServices("catalog:invoices")] IRepository<Invoice> repository)
    : IRequestHandler<UpdateInvoiceCommand, UpdateInvoiceResponse>
{
    public async Task<UpdateInvoiceResponse> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var invoice = await repository.GetByIdAsync(request.Id, cancellationToken);
        _ = invoice ?? throw new InvoiceNotFoundException(request.Id);
        var updatedInvoice = request.Adapt(invoice);
        await repository.UpdateAsync(updatedInvoice, cancellationToken);
        logger.LogInformation("invoice with id : {InvoiceId} updated.", invoice.Id);
        return new UpdateInvoiceResponse(invoice.Id);
    }
}
