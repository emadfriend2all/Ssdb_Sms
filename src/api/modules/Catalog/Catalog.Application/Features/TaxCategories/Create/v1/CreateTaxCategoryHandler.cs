using FSH.Framework.Core.Persistence;
using FSH.Starter.WebApi.Catalog.Domain;
using Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FSH.Starter.WebApi.Catalog.Application.Features.TaxCategories.Create.v1;
public sealed class CreateTaxCategoryHandler(
    ILogger<CreateTaxCategoryHandler> logger,
    [FromKeyedServices("catalog:taxCategories")] IRepository<TaxCategory> repository)
    : IRequestHandler<CreateTaxCategoryCommand, CreateTaxCategoryResponse>
{
    public async Task<CreateTaxCategoryResponse> Handle(CreateTaxCategoryCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        var taxCategory = request.Adapt<TaxCategory>();
        await repository.AddAsync(taxCategory, cancellationToken);
        logger.LogInformation("taxCategory created {TaxCategoryId}", taxCategory.Id);
        return new CreateTaxCategoryResponse(taxCategory.Id);
    }
}
