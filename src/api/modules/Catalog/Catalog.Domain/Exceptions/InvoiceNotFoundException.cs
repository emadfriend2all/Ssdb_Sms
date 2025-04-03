using FSH.Framework.Core.Exceptions;

namespace FSH.Starter.WebApi.Catalog.Domain.Exceptions;
public sealed class InvoiceNotFoundException : NotFoundException
{
    public InvoiceNotFoundException(int id)
        : base($"Invoice with id {id} not found")
    {
    }
}
