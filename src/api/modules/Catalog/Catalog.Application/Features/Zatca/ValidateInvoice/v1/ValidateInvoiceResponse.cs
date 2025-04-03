using MediatR;
using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Compliance;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Zatca.ValidateInvoice.v1;
public class ValidateInvoiceResponse : ValidationResponse
{
    public bool IsValid { get; set; }
}

public class ValidateInvoiceCommand : ValidateInvoiceRequest, IRequest<ValidateInvoiceResponse>
{
    public string? Lang { get; set; }
}
