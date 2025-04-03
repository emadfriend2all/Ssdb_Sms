using MediatR;
using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Invoices;
using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Reporting;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Zatca.ReportInvoice.v1;
public class ReportInvoiceResponse : ReportingResponse
{
    public bool IsSuccessful { get; set; }
    public string? Status { get; set; }
    public string? InvoiceHash { get; set; }
    public string? ReportedInvoice { get; set; }
    public string? SingedXML { get; set; }
    public string? QrCode { get; set; }
    public string? UUID { get; set; }
    public IList<string> ErrorMessages { get; set; } = [];
    public IList<string> WarningMessages { get; set; } = [];
}
public class ReportInvoiceCommand : StandardInvoice, IRequest<ReportInvoiceResponse>
{
}
