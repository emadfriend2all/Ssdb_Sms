using Ardalis.Specification;
using FSH.Starter.WebApi.Catalog.Domain;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Products.Get.v1;

public class GetProductSpecs : Specification<Product, ProductResponse>
{
    public GetProductSpecs(int id)
    {
        Query
            .Where(p => p.Id == id);
    }
}
