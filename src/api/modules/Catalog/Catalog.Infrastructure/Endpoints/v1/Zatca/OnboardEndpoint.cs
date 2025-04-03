using Asp.Versioning;
using FSH.Framework.Infrastructure.Auth.Policy;
using FSH.Starter.WebApi.Catalog.Application.Features.Zatca.OnBoard.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1.Zatca;
public static class OnboardEndpoint
{
    internal static RouteHandlerBuilder MapOnboardEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPost("/login", async (OnboardCommand input, ISender mediator) =>
            {
                var response = await mediator.Send(input);
                return Results.Ok(response);
            })
            .WithName(nameof(OnboardEndpoint))
            .WithSummary("Save invoice and get QR code")
            .WithDescription("create eInvoice, hash it and get QR code")
            .Produces(StatusCodes.Status204NoContent)
            .RequirePermission("Permissions.Zatca.Create")
            .MapToApiVersion(1);
    }
}
