using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Invoices.Delete.v1;
public sealed record DeleteInvoiceCommand(
    int Id) : IRequest;
