using Microsoft.Extensions.DependencyInjection;
using FSH.Starter.WebApi.Catalog.Domain.Exceptions;
using FSH.Framework.Core.Persistence;
using FSH.Framework.Core.Caching;
using FSH.Starter.WebApi.Catalog.Domain;
using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Customers.Get.v1;
public sealed class GetCustomerHandler(
    [FromKeyedServices("catalog:customers")] IReadRepository<Customer> repository,
    ICacheService cache)
    : IRequestHandler<GetCustomerRequest, CustomerResponse>
{
    public async Task<CustomerResponse> Handle(GetCustomerRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var item = await cache.GetOrSetAsync(
            $"customer:{request.Id}",
            async () =>
            {
                var spec = new GetCustomerSpecs(request.Id);
                var customerItem = await repository.FirstOrDefaultAsync(spec, cancellationToken);
                if (customerItem == null) throw new EntityNotFoundException(nameof(Customer),request.Id);
                return customerItem;
            },
            cancellationToken: cancellationToken);
        return item!;
    }
}
