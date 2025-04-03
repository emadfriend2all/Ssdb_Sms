using Finbuckle.MultiTenant.Abstractions;
using FSH.Framework.Core.Domain.Contracts;
using FSH.Framework.Core.Domain;
using FSH.Framework.Core.Exceptions;
using FSH.Framework.Infrastructure.Tenant.Abstractions;
using FSH.Starter.Shared.Authorization;

namespace FSH.Framework.Infrastructure.Tenant;
public sealed class FshTenantInfo : IFshTenantInfo
{
    public FshTenantInfo()
    {

    }

    public FshTenantInfo(string id, string name, string? connectionString, string adminEmail,string?subscriptionType, string? address, string? phoneNumber, string? logo, string? primaryColor, string? secondaryColor, string? reportHeader, string? reportFooter, string? issuer = null)
    {
        Id = id;
        Identifier = id;
        Name = name;
        ConnectionString = connectionString ?? string.Empty;
        AdminEmail = adminEmail;
        IsActive = true;
        Issuer = issuer;
        SubscriptionType = subscriptionType;
        Address = address;
        PhoneNumber = phoneNumber;
        Logo = logo;
        PrimaryColor = primaryColor;
        SecondaryColor = secondaryColor;
        ReportHeader = reportHeader;
        ReportFooter = reportFooter;
        // Add Default 1 Month Validity for all new tenants. Something like a DEMO period for tenants.
        ValidUpto = DateTime.UtcNow.AddDays(7);
    }
    public string Id { get; set; } = default!;
    public string Identifier { get; set; } = default!;

    public string Name { get; set; } = default!;
    public string ConnectionString { get; set; } = default!;

    public string AdminEmail { get; set; } = default!;
    public bool IsActive { get; set; }
    public DateTime ValidUpto { get; set; }
    public string? Issuer { get; set; }
    public string? SubscriptionType { get; set; }
    public int Balance { get; set; } = default!;
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Logo { get; set; }
    public string? PrimaryColor { get; set; }
    public string? SecondaryColor { get; set; }
    public string? ReportHeader { get; set; }
    public string? ReportFooter { get; set; }
    public FshTenantOrganization? Organization { get; set; }

    public void AddValidity(int months) =>
        ValidUpto = ValidUpto.AddMonths(months);

    public void SetValidity(in DateTime validTill) =>
        ValidUpto = ValidUpto < validTill
            ? validTill
            : throw new FshException("Subscription cannot be backdated.");

    public void TopUp(in int balance)
    {
        if (balance <= 0)
        {
            throw new FshException("Invalid balance.");
        }
        Balance += balance;
    }

    public void Charge(in int amount)
    {
        Balance -= amount;
    }

    public void Activate()
    {
        if (Id == TenantConstants.Root.Id)
        {
            throw new InvalidOperationException("Invalid Tenant");
        }

        IsActive = true;
    }

    public void Deactivate()
    {
        if (Id == TenantConstants.Root.Id)
        {
            throw new InvalidOperationException("Invalid Tenant");
        }

        IsActive = false;
    }
    string? ITenantInfo.Id { get => Id; set => Id = value ?? throw new InvalidOperationException("Id can't be null."); }
    string? ITenantInfo.Identifier { get => Identifier; set => Identifier = value ?? throw new InvalidOperationException("Identifier can't be null."); }
    string? ITenantInfo.Name { get => Name; set => Name = value ?? throw new InvalidOperationException("Name can't be null."); }
    string? IFshTenantInfo.ConnectionString { get => ConnectionString; set => ConnectionString = value ?? throw new InvalidOperationException("ConnectionString can't be null."); }
}
