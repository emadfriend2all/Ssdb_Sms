using System.Text.RegularExpressions;
using FSH.Framework.Core.Domain.Contracts;
using FSH.Framework.Core.Domain;

namespace FSH.Starter.WebApi.Catalog.Domain;

public class Address : BaseEntity<int>, IAggregateRoot
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
    public Customer? Customer { get; set; }

    public Address Build()
    {
        BuildFullAddress();
        BuildShortAddress();
        return this;
    }

    public string BuildFullAddress()
    {
        FullAddress = CleanAddress(StreetNumber, StreetName, BuildingNumber, LandMark, Suburb, City, Province, Country, PostalCode);
        return FullAddress;
    }

    public string BuildShortAddress()
    {
        ShortAddress = CleanAddress(StreetName, BuildingNumber, City, Country);
        return ShortAddress;
    }

    private static string CleanAddress(params string?[] parts)
    {
        string rawAddress = string.Join(" ", parts.Where(p => !string.IsNullOrWhiteSpace(p)));
        return Regex.Replace(rawAddress, @"[^\w\s]", "");
    }



}
