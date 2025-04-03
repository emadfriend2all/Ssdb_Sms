using FSH.Framework.Core.Domain;
using FSH.Framework.Core.Domain.Contracts;

namespace FSH.Starter.WebApi.Catalog.Domain;

public class Customer : BaseEntity<int>, IAggregateRoot
{
    public Customer()
    {
        Addresses = new HashSet<Address>();
        Contacts = new HashSet<Contact>();
    }

    public string? Name { get; set; }
    public string? IdentityNumber { get; set; }
    public string? TaxNumber { get; set; }
    public string? RegistrationNo { get; set; }
    public bool? IsCompany { get; set; }

    // Default Address (One-to-One)
    public int? DefaultAddressId { get; set; }
    public Address? DefaultAddress { get; set; }

    // Default Contact (One-to-One)
    public int? DefaultContactId { get; set; }
    public Contact? DefaultContact { get; set; }

    // One-to-Many Relationships
    public ICollection<Address> Addresses { get; set; }
    public ICollection<Contact> Contacts { get; set; }
}
