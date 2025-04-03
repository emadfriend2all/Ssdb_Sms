namespace Shared;
public class TenantBasicInfoDto
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? Contact { get; set; }
    public int Balance { get; set; } = default!;
    public DateTime? ValidUpto { get; set; }
    public string? ReportHeader { get; set; }
    public string? ReportFooter { get; set; }
    public string? Logo { get; set; }
    public string? PrimaryColor { get; set; }
    public string? SecondaryColor { get; set; }
    public bool IsActive { get; set; }
    public BasicTenantOrganization? Organization { get; set; }
}

public sealed class BasicTenantOrganization
{
    public string? CommonName { get; set; }
    public string? SerialNumber { get; set; }
    public string? OrganizationIdentifier { get; set; }
    public string? IdentifierType { get; set; }
    public string? OrganizationUnitName { get; set; }
    public string? OrganizationName { get; set; }
    public string? CountryName { get; set; }
    public string? LocationAddress { get; set; }
    public string? IndustryBusinessCategory { get; set; }
}
