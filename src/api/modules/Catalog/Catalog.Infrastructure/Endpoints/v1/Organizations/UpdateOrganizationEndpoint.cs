using FSH.Framework.Infrastructure.Auth.Policy;
using FSH.Starter.WebApi.Catalog.Application.Features.Organizations.Update.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1.Organizations;
public static class UpdateOrganizationEndpoint
{
    internal static RouteHandlerBuilder MapOrganizationUpdateEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPut("/{id:guid}", async (int id, UpdateOrganizationCommand request, ISender mediator) =>
            {
                if (id != request.Id) return Results.BadRequest();
                var response = await mediator.Send(request);
                return Results.Ok(response);
            })
            .WithName(nameof(UpdateOrganizationEndpoint))
            .WithSummary("update a organization")
            .WithDescription("update a organization")
            .Produces<UpdateOrganizationResponse>()
            .RequirePermission("Permissions.Organizations.Update")
            .MapToApiVersion(1);
    }
}
