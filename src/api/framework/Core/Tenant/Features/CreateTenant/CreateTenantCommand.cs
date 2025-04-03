using MediatR;
using Shared;

namespace FSH.Framework.Core.Tenant.Features.CreateTenant;
public sealed record CreateTenantCommand(string Id,
    string Name,
    string? ConnectionString,
    string AdminEmail, string? SubscriptionType,
    string? Address, string? PhoneNumber, string? Logo, string? PrimaryColor, string? SecondaryColor, string? ReportHeader, string? ReportFooter,
    string? Issuer, BasicTenantOrganization Organization) : IRequest<CreateTenantResponse>
{
};
