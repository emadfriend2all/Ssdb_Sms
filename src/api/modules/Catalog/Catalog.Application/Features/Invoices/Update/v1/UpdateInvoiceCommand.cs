using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Invoices.Update.v1;
public sealed record UpdateInvoiceCommand(
    int Id,
    string? Name,
    decimal Price,
    string? Description = null,
    int? BrandId = null) : IRequest<UpdateInvoiceResponse>;
