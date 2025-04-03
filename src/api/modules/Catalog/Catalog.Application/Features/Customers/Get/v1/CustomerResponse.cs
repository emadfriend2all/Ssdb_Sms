
using System.Text.Json.Serialization;
using FSH.Starter.WebApi.Catalog.Domain;
using Newtonsoft.Json;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Customers.Get.v1;
public sealed record CustomerResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? IdentityNumber { get; set; }
    public string? TaxNumber { get; set; }
    public string? RegistrationNo { get; set; }
    public bool? IsCompany { get; set; }
    public GetCustomerCommandAddress? DefaultAddress { get; set; }
    public GetCustomerCommandContact? DefaultContact { get; set; }
}

public partial class GetCustomerCommandContact
{
    public string? Position { get; set; }
    public string? Email { get; set; }
    public string PhoneNumber { get; set; }
    public int? CustomerId { get; set; }
}

public class GetCustomerCommandAddress
{
    public string? StreetName { get; set; }
    public string? StreetNumber { get; set; }
    public string? BuildingNumber { get; set; }
    public string? LandMark { get; set; }
    public string? Suburb { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; }
    public string? Province { get; set; }
    public string? Country { get; set; }
    public string? CountryCode { get; set; }
    public string? FullAddress { get; set; }
    public string? ShortAddress { get; set; }
    public int? CustomerId { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
}
