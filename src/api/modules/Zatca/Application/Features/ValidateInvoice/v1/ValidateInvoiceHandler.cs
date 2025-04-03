using MediatR;
using FSH.Starter.WebApi.Zatca.Application.Interfaces;
using Shared.Contracts.Zatca.Compliance;
using Mapster;

namespace FSH.Starter.WebApi.Zatca.Application.Features.ValidateInvoice.v1;
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
