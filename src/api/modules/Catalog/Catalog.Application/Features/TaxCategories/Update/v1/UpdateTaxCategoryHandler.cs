using FSH.Framework.Core.Persistence;
using FSH.Starter.WebApi.Catalog.Domain;
using FSH.Starter.WebApi.Catalog.Domain.Exceptions;
using Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FSH.Starter.WebApi.Catalog.Application.Features.TaxCategories.Update.v1;
public sealed class UpdateTaxCategoryHandler(
    ILogger<UpdateTaxCategoryHandler> logger,
    [FromKeyedServices("catalog:taxCategories")] IRepository<TaxCategory> repository)
    : IRequestHandler<UpdateTaxCategoryCommand, UpdateTaxCategoryResponse>
{
    public async Task<UpdateTaxCategoryResponse> Handle(UpdateTaxCategoryCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var taxCategory = await repository.GetByIdAsync(request.Id, cancellationToken);
        _ = taxCategory ?? throw new TaxCategoryNotFoundException(request.Id);
        var updatedTaxCategory = request.Adapt(taxCategory);
        await repository.UpdateAsync(updatedTaxCategory, cancellationToken);
        logger.LogInformation("TaxCategory with id : {TaxCategoryId} updated.", taxCategory.Id);
        return new UpdateTaxCategoryResponse(taxCategory.Id);
    }
}
