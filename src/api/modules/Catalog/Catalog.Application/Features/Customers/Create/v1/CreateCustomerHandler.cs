using FSH.Framework.Core.Persistence;
using FSH.Starter.WebApi.Catalog.Domain;
using Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Customers.Create.v1;
public sealed class CreateCustomerHandler(
    ILogger<CreateCustomerHandler> logger,
    [FromKeyedServices("catalog:customers")] IRepository<Customer> repository)
    : IRequestHandler<CreateCustomerCommand, CreateCustomerResponse>
{
    public async Task<CreateCustomerResponse> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var customer = request.Adapt<Customer>();
        await repository.AddAsync(customer, cancellationToken);
        logger.LogInformation("customer created {CustomerId}", customer.Id);
        return new CreateCustomerResponse(customer.Id);
    }
}
