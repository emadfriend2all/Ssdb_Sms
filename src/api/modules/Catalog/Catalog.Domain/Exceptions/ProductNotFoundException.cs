using FSH.Framework.Core.Exceptions;

namespace FSH.Starter.WebApi.Catalog.Domain.Exceptions;
public sealed class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(int id)
        : base($"product with id {id} not found")
    {
    }
}
