using FSH.Framework.Core.Domain;
using FSH.Framework.Core.Domain.Contracts;

namespace FSH.Starter.WebApi.Catalog.Domain;

public partial class Contact : BaseEntity<int>, IAggregateRoot
{
    public string? Position { get; set; }
    public string? Email { get; set; }
    public string PhoneNumber { get; set; }
    public int? CustomerId { get; set; }
    public Customer? Customer { get; set; }
}
