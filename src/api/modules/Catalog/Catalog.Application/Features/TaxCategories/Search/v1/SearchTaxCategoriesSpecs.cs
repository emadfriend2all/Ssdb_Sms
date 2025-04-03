using Ardalis.Specification;
using FSH.Framework.Core.Paging;
using FSH.Framework.Core.Specifications;
using FSH.Starter.WebApi.Catalog.Application.Features.TaxCategories.Get.v1;
using FSH.Starter.WebApi.Catalog.Domain;

namespace FSH.Starter.WebApi.Catalog.Application.Features.TaxCategories.Search.v1;
public class SearchTaxCategoriesSpecs : EntitiesByPaginationFilterSpec<TaxCategory, TaxCategoryResponse>
{
    public SearchTaxCategoriesSpecs(SearchTaxCategoriesCommand command)
        : base(command) =>
        Query
            .OrderBy(c => c.Id, !command.HasOrderBy())
            .Where(b => b.Id.Contains(command.Keyword!), !string.IsNullOrEmpty(command.Keyword))
            .Where(b => b.TaxSchemeId.Contains(command.TaxSchemeCode!), !string.IsNullOrEmpty(command.TaxSchemeCode))
            .AsNoTracking();
}
