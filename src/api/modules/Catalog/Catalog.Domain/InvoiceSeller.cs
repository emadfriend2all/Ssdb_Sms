using System.Numerics;
using FSH.Framework.Core.Domain;
using FSH.Framework.Core.Domain.Contracts;

namespace FSH.Starter.WebApi.Catalog.Domain;

public class InvoiceSeller : BaseEntity<int>, IAggregateRoot
{
    public string? Identifier { get; set; }
    public string? IdentifierType { get; set; }
    public string? Name { get; set; }
    public int? AddressId { get; set; }
    public Address? Address { get; set; }
}
