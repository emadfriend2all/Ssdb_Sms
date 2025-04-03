using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Products.Update.v1;
public sealed record UpdateProductCommand(
    int Id,
    string? Name,
    decimal Price,
    string? Description = null,
    int? BrandId = null) : IRequest<UpdateProductResponse>;
