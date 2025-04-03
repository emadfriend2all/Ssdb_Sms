using FSH.Starter.WebApi.Zatca.Application.Features.ClearInvoice.v1;
using Shared.Contracts.Zatca.Compliance;
using Shared.Contracts.Zatca.Shared;
using MediatR;

namespace FSH.Starter.WebApi.Zatca.Application.Features.ValidateInvoice.v1;
public class ValidateInvoiceResponse: ValidationResponse
{
    public bool IsValid { get; set; }
}

public class ValidateInvoiceCommand : ValidateInvoiceRequest, IRequest<ValidateInvoiceResponse>
{
    public string? Lang { get; set; }
}
