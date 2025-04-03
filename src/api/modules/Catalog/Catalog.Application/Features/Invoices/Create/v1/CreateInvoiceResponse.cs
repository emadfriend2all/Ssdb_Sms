using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Shared;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Invoices.Create.v1;
public sealed record CreateInvoiceResponse(){
    public bool IsSuccessful { get; set; }
    public string? Status { get; set; }
    public string? QrCode { get; set; }
    public string? UUID { get; set; }
    public string? SubmitedInvoice { get; set; }
    public string? SingedXML { get; set; }
    public ValidationResults? ValidationResults { get; set; }
}
