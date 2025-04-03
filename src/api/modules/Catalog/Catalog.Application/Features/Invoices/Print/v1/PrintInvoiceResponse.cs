using FSH.Starter.WebApi.Catalog.Domain;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Invoices.Print.v1;
public class PrintInvoiceResponse
{
    public byte[] File { get; set; }
    public string? FileName { get; set; }
}
