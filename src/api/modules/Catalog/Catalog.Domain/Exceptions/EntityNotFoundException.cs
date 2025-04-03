using FSH.Framework.Core.Exceptions;

namespace FSH.Starter.WebApi.Catalog.Domain.Exceptions;

public sealed class EntityNotFoundException : NotFoundException
{
    public EntityNotFoundException(string entityName, int id)
        : base($"{entityName} with id {id} not found")
    {
    }
}
