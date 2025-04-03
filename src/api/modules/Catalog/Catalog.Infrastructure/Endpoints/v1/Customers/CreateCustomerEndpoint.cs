using FSH.Framework.Infrastructure.Auth.Policy;
using FSH.Starter.WebApi.Catalog.Application.Features.Customers.Create.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1.Customers;
public static class CreateCustomerEndpoint
{
    internal static RouteHandlerBuilder MapCustomerCreationEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPost("/", async (CreateCustomerCommand request, ISender mediator) =>
            {
                var response = await mediator.Send(request);
                return Results.Ok(response);
            })
            .WithName(nameof(CreateCustomerEndpoint))
            .WithSummary("creates a customer")
            .WithDescription("creates a customer")
            .Produces<CreateCustomerResponse>()
            .RequirePermission("Permissions.Customers.Create")
            .MapToApiVersion(1);
    }
}
