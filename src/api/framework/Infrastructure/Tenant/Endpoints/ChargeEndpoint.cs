using FSH.Framework.Core.Tenant.Features.Charge;
using FSH.Framework.Infrastructure.Auth.Policy;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FSH.Framework.Infrastructure.Tenant.Endpoints;

public static class ChargeEndpoint
{
    internal static RouteHandlerBuilder MapChargeTenantBalanceEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapPost("/charge", (ChargeCommand command, ISender mediator) => mediator.Send(command))
                                .WithName(nameof(ChargeEndpoint))
                                .WithSummary("charge tenant balance")
                                .RequirePermission("Permissions.Tenants.Update")
                                .WithDescription("charge tenant balance");
    }
}
