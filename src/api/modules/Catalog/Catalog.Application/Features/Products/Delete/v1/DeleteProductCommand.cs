using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Products.Delete.v1;
public sealed record DeleteProductCommand(
    int Id) : IRequest;
