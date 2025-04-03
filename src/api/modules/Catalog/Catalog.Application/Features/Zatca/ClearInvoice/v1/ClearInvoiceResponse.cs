using MediatR;
using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Invoices;
using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Shared;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Zatca.ClearInvoice.v1;
public class ClearInvoiceResponse
{
    public bool IsSuccessful { get; set; }
    public string? Status { get; set; }
    public string? QrCode { get; set; }
    public string? UUID { get; set; }
    public string? ClearedInvoice { get; set; }
    public string? SingedXML { get; set; }
    public ValidationResults? ValidationResults { get; set; }
}

public class ClearInvoiceCommand : StandardInvoice, IRequest<ClearInvoiceResponse>
{
    public string? Lang { get; set; }
    public string? Environment { get; set; }
}
