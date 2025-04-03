using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Customers.Delete.v1;
public sealed record DeleteCustomerCommand(
    int Id) : IRequest;
