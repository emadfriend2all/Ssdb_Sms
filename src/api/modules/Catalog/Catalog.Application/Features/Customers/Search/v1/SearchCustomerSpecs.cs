using Ardalis.Specification;
using FSH.Framework.Core.Paging;
using FSH.Framework.Core.Specifications;
using FSH.Starter.WebApi.Catalog.Application.Features.Customers.Get.v1;
using FSH.Starter.WebApi.Catalog.Domain;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Customers.Search.v1;
public class SearchCustomerSpecs : EntitiesByPaginationFilterSpec<Customer, CustomerResponse>
{
    public SearchCustomerSpecs(SearchCustomersCommand command)
        : base(command) =>
        Query
        .Include(x => x.DefaultContact)
        .Include(x => x.DefaultAddress)
            .OrderBy(c => c.Name, !command.HasOrderBy())
            .Where(p => p.DefaultContact != null && p.DefaultContact.PhoneNumber.Contains(command.PhoneNumber!), !string.IsNullOrEmpty(command.PhoneNumber))
            .Where(p => p.DefaultAddress != null && p.DefaultAddress.FullAddress != null && p.DefaultAddress.FullAddress.Contains(command.PhoneNumber!), !string.IsNullOrEmpty(command.Address));
}
