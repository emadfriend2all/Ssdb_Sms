using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Products.Get.v1;
public class GetProductRequest : IRequest<ProductResponse>
{
    public int Id { get; set; }
    public GetProductRequest(int id) => Id = id;
}
