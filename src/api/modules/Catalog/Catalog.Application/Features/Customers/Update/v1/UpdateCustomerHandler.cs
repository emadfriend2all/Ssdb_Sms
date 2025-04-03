using FSH.Framework.Core.Persistence;
using FSH.Starter.WebApi.Catalog.Domain;
using FSH.Starter.WebApi.Catalog.Domain.Exceptions;
using Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Customers.Update.v1;
public sealed class UpdateCustomerHandler(
    ILogger<UpdateCustomerHandler> logger,
    [FromKeyedServices("catalog:customers")] IRepository<Customer> repository)
    : IRequestHandler<UpdateCustomerCommand, UpdateCustomerResponse>
{
    public async Task<UpdateCustomerResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var customer = await repository.GetByIdAsync(request.Id, cancellationToken);
        _ = customer ?? throw new EntityNotFoundException(nameof(Customer), request.Id);
        var updatedCustomer = request.Adapt(customer);
        await repository.UpdateAsync(updatedCustomer, cancellationToken);
        logger.LogInformation("customer with id : {CustomerId} updated.", customer.Id);
        return new UpdateCustomerResponse(customer.Id);
    }
}
