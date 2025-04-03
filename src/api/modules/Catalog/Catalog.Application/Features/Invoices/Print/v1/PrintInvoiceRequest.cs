using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Invoices.Print.v1;
public class PrintInvoiceRequest : IRequest<PrintInvoiceResponse>
{
    public int Id { get; set; }
    public PrintInvoiceRequest(int id) => Id = id;
}
