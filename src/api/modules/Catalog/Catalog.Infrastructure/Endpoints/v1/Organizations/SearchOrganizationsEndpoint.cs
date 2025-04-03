using FSH.Framework.Core.Paging;
using FSH.Framework.Infrastructure.Auth.Policy;
using FSH.Starter.WebApi.Catalog.Application.Features.Organizations.Get.v1;
using FSH.Starter.WebApi.Catalog.Application.Features.Organizations.Search.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1.Organizations;

public static class SearchOrganizationsEndpoint
{
    internal static RouteHandlerBuilder MapGetOrganizationListEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPost("/search", async (ISender mediator, [FromBody] SearchOrganizationsCommand command) =>
            {
                var response = await mediator.Send(command);
                return Results.Ok(response);
            })
            .WithName(nameof(SearchOrganizationsEndpoint))
            .WithSummary("Gets a list of organizations")
            .WithDescription("Gets a list of organizations with pagination and filtering support")
            .Produces<PagedList<OrganizationResponse>>()
            .RequirePermission("Permissions.Organizations.View")
            .MapToApiVersion(1);
    }
}

