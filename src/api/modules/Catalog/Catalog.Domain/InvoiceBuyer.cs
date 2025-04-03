using FSH.Framework.Core.Domain.Contracts;
using FSH.Framework.Core.Domain;

namespace FSH.Starter.WebApi.Catalog.Domain;

public class InvoiceBuyer : BaseEntity<int>, IAggregateRoot
{
    public string Name { get; set; } = default!;
    public string TaxNumber { get; set; } = default!;
    public Address? Address { get; set; }
}
