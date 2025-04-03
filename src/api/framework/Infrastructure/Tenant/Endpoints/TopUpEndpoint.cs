using FSH.Framework.Core.Tenant.Features.TopUp;
using FSH.Framework.Infrastructure.Auth.Policy;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FSH.Framework.Infrastructure.Tenant.Endpoints;

public static class TopUpEndpoint
{
    internal static RouteHandlerBuilder MapTopUpTenantBalanceEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapPost("/topup", (TopUpCommand command, ISender mediator) => mediator.Send(command))
                                .WithName(nameof(TopUpEndpoint))
                                .WithSummary("top-up tenant balance")
                                .RequirePermission("Permissions.Tenants.Update")
                                .WithDescription("top-up tenant balance");
    }
}
