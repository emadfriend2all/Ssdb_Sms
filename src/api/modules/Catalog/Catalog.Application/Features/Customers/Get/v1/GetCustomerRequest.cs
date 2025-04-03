using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Customers.Get.v1;
public class GetCustomerRequest : IRequest<CustomerResponse>
{
    public int Id { get; set; }
    public GetCustomerRequest(int id) => Id = id;
}
