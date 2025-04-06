using FSH.Framework.Infrastructure.Auth.Policy;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Todo.Features.Get.v1;
public static class GetBalanceEndpoint
{
    internal static RouteHandlerBuilder MapGetTodoEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapGet("/GetBalance", async (ISender mediator) =>
                        {
                            var response = await mediator.Send(new GetBalanceRequest());
                            return Results.Ok(response);
                        })
                        .WithName(nameof(GetBalanceEndpoint))
                        .WithSummary("gets todo item by id")
                        .WithDescription("gets todo item by id")
                        .Produces<GetBalanceResponse>()
                        .RequirePermission("Permissions.Todos.View")
                        .MapToApiVersion(1);
    }
}
