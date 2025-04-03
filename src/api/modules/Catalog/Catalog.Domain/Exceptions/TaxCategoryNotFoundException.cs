using FSH.Framework.Core.Exceptions;

namespace FSH.Starter.WebApi.Catalog.Domain.Exceptions;
public sealed class TaxCategoryNotFoundException : NotFoundException
{
    public TaxCategoryNotFoundException(string categoryCode)
        : base($"TaxCategory with code {categoryCode} not found")
    {
    }
}
