using FSH.Framework.Core.Domain.Contracts;
using FSH.Framework.Core.Domain;

namespace FSH.Starter.WebApi.Catalog.Domain;

public class TaxScheme : BaseEntity<string>, IAggregateRoot
{
    public string Id { get; set; }
    public string? Name { get; set; }
}
