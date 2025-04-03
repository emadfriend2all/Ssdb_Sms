
namespace FSH.Starter.WebApi.Catalog.Application.Features.Products.Get.v1;
public sealed record ProductResponse(int? Id, string Name, string? Description, decimal Price);
