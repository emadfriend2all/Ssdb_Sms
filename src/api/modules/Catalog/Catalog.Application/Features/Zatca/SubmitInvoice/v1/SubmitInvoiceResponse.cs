using MediatR;
using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Invoices;
using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Shared;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Zatca.SubmitInvoice.v1;
public class SubmitInvoiceResponse
{
    public bool IsSuccessful { get; set; }
    public string? Status { get; set; }
    public string? QrCode { get; set; }
    public string? UUID { get; set; }
    public string? SubmitedInvoice { get; set; }
    public string? SingedXML { get; set; }
    public ValidationResults? ValidationResults { get; set; }
}

public class SubmitInvoiceCommand : StandardInvoice, IRequest<SubmitInvoiceResponse>
{
    public string? Lang { get; set; }
}
