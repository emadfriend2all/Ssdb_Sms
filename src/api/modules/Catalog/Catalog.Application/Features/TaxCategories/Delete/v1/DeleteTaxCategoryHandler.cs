using FSH.Framework.Core.Persistence;
using FSH.Starter.WebApi.Catalog.Domain;
using FSH.Starter.WebApi.Catalog.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FSH.Starter.WebApi.Catalog.Application.Features.TaxCategories.Delete.v1;
public sealed class DeleteTaxCategoryHandler(
    ILogger<DeleteTaxCategoryHandler> logger,
    [FromKeyedServices("catalog:taxCategories")] IRepository<TaxCategory> repository)
    : IRequestHandler<DeleteTaxCategoryCommand>
{
    public async Task Handle(DeleteTaxCategoryCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var taxCategory = await repository.GetByIdAsync(request.Id, cancellationToken);
        _ = taxCategory ?? throw new TaxCategoryNotFoundException(request.Id.ToString());
        await repository.DeleteAsync(taxCategory, cancellationToken);
        logger.LogInformation("TaxCategory with id : {TaxCategoryId} deleted", taxCategory.Id);
    }
}
