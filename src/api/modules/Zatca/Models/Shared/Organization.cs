using FSH.Framework.Core.Domain;
using FSH.Framework.Core.Domain.Contracts;

namespace Shared.Contracts.Zatca.Shared;
public class OrganizationModel
{
    public OrganizationModel()
    {
    }

    public OrganizationModel(string commonName, string serialNumber, string organizationIdentifier, string organizationUnitName, string organizationName, string countryName, string invoiceType, string locationAddress, string industryBusinessCategory)
    {
        CommonName = commonName;
        SerialNumber = serialNumber;
        OrganizationIdentifier = organizationIdentifier;
        OrganizationUnitName = organizationUnitName;
        OrganizationName = organizationName;
        CountryName = countryName;
        InvoiceType = invoiceType;
        LocationAddress = locationAddress;
        IndustryBusinessCategory = industryBusinessCategory;
    }

    /// <summary>
    /// CommonName
    /// </summary>
    public string CommonName { get; set; }

    /// <summary>
    /// SerialNumber (Must be 15 digits, start and end with '3')
    /// </summary>
    public string SerialNumber { get; set; }

    /// <summary>
    /// OrganizationIdentifier (Must be 15 digits, start and end with '3')
    /// </summary>
    public string OrganizationIdentifier { get; set; }

    /// <summary>
    /// OrganizationUnitName (Should not contain special characters)
    /// </summary>
    public string OrganizationUnitName { get; set; }

    /// <summary>
    /// OrganizationName (Should not contain special characters)
    /// </summary>
    public string OrganizationName { get; set; }

    /// <summary>
    /// CountryName (Must be exactly 2 characters)
    /// </summary>
    public string CountryName { get; set; }

    /// <summary>
    /// InvoiceType (Must be 4 digits and match [0-1]{4})
    /// </summary>
    public string InvoiceType { get; set; }

    /// <summary>
    /// LocationAddress (Should not contain special characters)
    /// </summary>
    public string LocationAddress { get; set; }

    /// <summary>
    /// IndustryBusinessCategory (Should not contain special characters)
    /// </summary>
    public string IndustryBusinessCategory { get; set; }

    /// <summary>
    /// This is to flag that organization is representing the Current Tenant Organization
    /// </summary>
    public bool IsMyOrganization { get; set; }
}
