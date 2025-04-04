using FSH.Framework.Infrastructure.Auth.Policy;
using FSH.Starter.WebApi.Catalog.Application.Features.Customers.Update.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1.Customers;
public static class UpdateCustomerEndpoint
{
    internal static RouteHandlerBuilder MapCustomerUpdateEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPut("/{id:guid}", async (int id, UpdateCustomerCommand request, ISender mediator) =>
            {
                if (id != request.Id) return Results.BadRequest();
                var response = await mediator.Send(request);
                return Results.Ok(response);
            })
            .WithName(nameof(UpdateCustomerEndpoint))
            .WithSummary("update a customer")
            .WithDescription("update a customer")
            .Produces<UpdateCustomerResponse>()
            .RequirePermission("Permissions.Customers.Update")
            .MapToApiVersion(1);
    }
}
