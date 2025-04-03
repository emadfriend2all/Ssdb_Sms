using FSH.Framework.Core.Exceptions;

namespace FSH.Starter.WebApi.Catalog.Domain.Exceptions;

public sealed class OrganizationNotFoundException : NotFoundException
{
    public OrganizationNotFoundException(int id)
        : base($"product with id {id} not found")
    {
    }
}
