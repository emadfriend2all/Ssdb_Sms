using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.Features.TaxCategories.Delete.v1;
public sealed record DeleteTaxCategoryCommand(
    int Id) : IRequest;
