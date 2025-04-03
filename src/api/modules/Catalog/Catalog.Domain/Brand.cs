using FSH.Framework.Core.Domain;
using FSH.Framework.Core.Domain.Contracts;
using FSH.Starter.WebApi.Catalog.Domain.Events;

namespace FSH.Starter.WebApi.Catalog.Domain;
public class Brand : BaseEntity<int>, IAggregateRoot
{
    public string PrimaryColor { get; private set; } = default!;
    public string? SecondaryColor { get; private set; }
    public string? Logo { get; set; }

}


