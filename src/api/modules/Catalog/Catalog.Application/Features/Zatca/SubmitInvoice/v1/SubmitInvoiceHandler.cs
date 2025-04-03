using MediatR;
using Microsoft.Extensions.Logging;
using Mapster;
using FSH.Starter.WebApi.Zatca.Application.Interfaces;
using FSH.Starter.WebApi.Catalog.Application.Features.Zatca.ClearInvoice.v1;
using FSH.Starter.WebApi.Catalog.Application.Features.Zatca.ReportInvoice.v1;
using FSH.Starter.WebApi.Catalog.Application.Features.Invoices.Create.v1;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Zatca.SubmitInvoice.v1;
public sealed class SubmitInvoiceHandler(ILogger<SubmitInvoiceHandler> logger, IEgsService egsService, IMediator mediator) : IRequestHandler<SubmitInvoiceCommand, SubmitInvoiceResponse>
{
    public async Task<SubmitInvoiceResponse> Handle(SubmitInvoiceCommand request, CancellationToken cancellationToken)
    {   
        var isB2B = request.InvoiceTypeCode  == "0100000";
        if (isB2B)
        {
            var clearanceResponse = await mediator.Send(request.Adapt<ClearInvoiceCommand>(), cancellationToken);
            return clearanceResponse.Adapt<SubmitInvoiceResponse>();
        }
        else
        {
            var reportResponse = await mediator.Send(request.Adapt<ReportInvoiceCommand>(), cancellationToken);
            return reportResponse.Adapt<SubmitInvoiceResponse>();
        }
    }
}
