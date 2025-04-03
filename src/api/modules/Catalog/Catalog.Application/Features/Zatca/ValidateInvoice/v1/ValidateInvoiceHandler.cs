using MediatR;
using FSH.Starter.WebApi.Zatca.Application.Interfaces;
using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Compliance;
using Mapster;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Zatca.ValidateInvoice.v1;
public sealed class ValidateInvoiceHandler(IEgsService egsService) : IRequestHandler<ValidateInvoiceCommand, ValidateInvoiceResponse>
{
    public async Task<ValidateInvoiceResponse> Handle(ValidateInvoiceCommand request, CancellationToken cancellationToken)
    {
        var validationResponse = await egsService.ValidateInvoiceAsync(new ValidateInvoiceRequest
        {
            Invoice = request.Invoice,
            InvoiceHash = request.InvoiceHash,
            UUID = request.UUID,
        });

        return validationResponse.Adapt<ValidateInvoiceResponse>();
    }
}
