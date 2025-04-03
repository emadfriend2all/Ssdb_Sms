using Asp.Versioning;
using FSH.Framework.Infrastructure.Auth.Policy;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Zatca.Application.Features.OnBoard.v1;
public static class OnboardEndpoint
{
    internal static RouteHandlerBuilder MapOnboardEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPost("/Login", async (OnboardCommand input, ISender mediator) =>
            {
                var response = await mediator.Send(input);
                return Results.Ok(response);
            })
            .WithName(nameof(OnboardEndpoint))
            .WithSummary("Save invoice and get QR code")
            .WithDescription("create eInvoice, hash it and get QR code")
            .Produces(StatusCodes.Status204NoContent)
            .RequirePermission("Permissions.Products.View")
            .MapToApiVersion(1);
    }
}
