using Shared;

namespace FSH.Framework.Core.Tenant.Dtos;
public class TenantDetail
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? ConnectionString { get; set; }
    public string AdminEmail { get; set; } = default!;
    public string SubscriptionType { get; set; } = default!;
    public bool IsActive { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? Contact { get; set; }
    public int Balance { get; set; } = default!;
    public DateTime ValidUpto { get; set; }
    public string? ReportHeader { get; set; }
    public string? ReportFooter { get; set; }
    public string? Logo { get; set; }
    public string? PrimaryColor { get; set; }
    public string? SecondaryColor { get; set; }
    public string? Issuer { get; set; }
}
