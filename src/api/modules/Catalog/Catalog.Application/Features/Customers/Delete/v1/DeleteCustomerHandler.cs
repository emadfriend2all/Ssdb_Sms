using FSH.Framework.Core.Persistence;
using FSH.Starter.WebApi.Catalog.Domain;
using FSH.Starter.WebApi.Catalog.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Customers.Delete.v1;
public sealed class DeleteCustomerHandler(
    ILogger<DeleteCustomerHandler> logger,
    [FromKeyedServices("catalog:customers")] IRepository<Customer> repository)
    : IRequestHandler<DeleteCustomerCommand>
{
    public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var customer = await repository.GetByIdAsync(request.Id, cancellationToken);
        _ = customer ?? throw new EntityNotFoundException(nameof(Customer),request.Id);
        await repository.DeleteAsync(customer, cancellationToken);
        logger.LogInformation("customer with id : {CustomerId} deleted", customer.Id);
    }
}
