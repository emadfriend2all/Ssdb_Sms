using FSH.Framework.Infrastructure.Auth.Policy;
using FSH.Starter.WebApi.Catalog.Application.Features.Organizations.Get.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1.Organizations;
public static class GetOrganizationEndpoint
{
    internal static RouteHandlerBuilder MapGetOrganizationEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet("/{id:guid}", async (int id, ISender mediator) =>
            {
                var response = await mediator.Send(new GetOrganizationRequest(id));
                return Results.Ok(response);
            })
            .WithName(nameof(GetOrganizationEndpoint))
            .WithSummary("gets organization by id")
            .WithDescription("gets prodct by id")
            .Produces<OrganizationResponse>()
            .RequirePermission("Permissions.Organizations.View")
            .MapToApiVersion(1);
    }
}
