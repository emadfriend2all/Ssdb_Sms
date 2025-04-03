using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Invoices.Get.v1;
public class GetInvoiceRequest : IRequest<GetInvoiceResponse>
{
    public int Id { get; set; }
    public GetInvoiceRequest(int id) => Id = id;
}
