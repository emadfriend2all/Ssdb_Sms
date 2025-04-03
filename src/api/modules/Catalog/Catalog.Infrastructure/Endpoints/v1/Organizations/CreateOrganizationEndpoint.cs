using FSH.Framework.Infrastructure.Auth.Policy;
using FSH.Starter.WebApi.Catalog.Application.Features.Organizations.Create.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1;
public static class CreateOrganizationEndpoint
{
    internal static RouteHandlerBuilder MapOrganizationCreationEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPost("/", async (CreateOrganizationCommand request, ISender mediator) =>
            {
                var response = await mediator.Send(request);
                return Results.Ok(response);
            })
            .WithName(nameof(CreateOrganizationEndpoint))
            .WithSummary("creates a organization")
            .WithDescription("creates a organization")
            .Produces<CreateOrganizationResponse>()
            .RequirePermission("Permissions.Organizations.Create")
            .MapToApiVersion(1);
    }
}
