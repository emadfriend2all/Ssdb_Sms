using Shared.Contracts.Zatca.Shared;
using MediatR;
using Shared.Contracts.Zatca.Invoices;

namespace FSH.Starter.WebApi.Zatca.Application.Features.ClearInvoice.v1;
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

public class ClearInvoiceCommand: StandardInvoice, IRequest<ClearInvoiceResponse>
{
    public string Lang { get; internal set; }
}
