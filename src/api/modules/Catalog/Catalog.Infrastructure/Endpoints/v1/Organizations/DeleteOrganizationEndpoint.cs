using FSH.Framework.Infrastructure.Auth.Policy;
using FSH.Starter.WebApi.Catalog.Application.Features.Organizations.Delete.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1.Organizations;
public static class DeleteOrganizationEndpoint
{
    internal static RouteHandlerBuilder MapOrganizationDeleteEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapDelete("/{id:guid}", async (int id, ISender mediator) =>
             {
                 await mediator.Send(new DeleteOrganizationCommand(id));
                 return Results.NoContent();
             })
            .WithName(nameof(DeleteOrganizationEndpoint))
            .WithSummary("deletes organization by id")
            .WithDescription("deletes organization by id")
            .Produces(StatusCodes.Status204NoContent)
            .RequirePermission("Permissions.Organizations.Delete")
            .MapToApiVersion(1);
    }
}
