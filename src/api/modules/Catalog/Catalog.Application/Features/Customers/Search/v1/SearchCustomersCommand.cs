using FSH.Framework.Core.Paging;
using FSH.Starter.WebApi.Catalog.Application.Features.Customers.Get.v1;
using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Customers.Search.v1;

public class SearchCustomersCommand : PaginationFilter, IRequest<PagedList<CustomerResponse>>
{
    public string? Name { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
}
