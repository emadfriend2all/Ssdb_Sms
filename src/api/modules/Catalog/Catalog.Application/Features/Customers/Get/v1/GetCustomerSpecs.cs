using Ardalis.Specification;
using FSH.Starter.WebApi.Catalog.Domain;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Customers.Get.v1;

public class GetCustomerSpecs : Specification<Customer, CustomerResponse>
{
    public GetCustomerSpecs(int id)
    {
        Query
            .Where(p => p.Id == id);
    }
}
