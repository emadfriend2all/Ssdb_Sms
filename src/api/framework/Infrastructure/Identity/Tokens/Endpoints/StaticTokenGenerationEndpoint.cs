using FSH.Framework.Core.Identity.Tokens;
using FSH.Framework.Core.Identity.Tokens.Features.Generate;
using FSH.Framework.Infrastructure.Auth.Policy;
using FSH.Starter.Shared.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FSH.Framework.Infrastructure.Identity.Tokens.Endpoints;

public static class StaticTokenGenerationEndpoint
{
    internal static RouteHandlerBuilder MapStaticTokenGenerationEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapPost("/GenerateStaticToken", (StaticTokenGenerationCommand request,
            [FromHeader(Name = TenantConstants.Identifier)] string tenant,
            ITokenService service,
            HttpContext context,
            CancellationToken cancellationToken) =>
        {
            return service.GenerateStaticTokenAsync(request, cancellationToken);
        })
        .WithName(nameof(StaticTokenGenerationEndpoint))
        .WithSummary("generate JWTs")
        .WithDescription("generate JWTs")
        .RequirePermission("Permissions.SMS.Send");
    }
}
