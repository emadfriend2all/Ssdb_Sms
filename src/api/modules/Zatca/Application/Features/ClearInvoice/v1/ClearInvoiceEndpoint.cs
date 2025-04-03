using FSH.Framework.Infrastructure.Auth.Policy;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Zatca.Application.Features.ClearInvoice.v1;
public static class ClearInvoiceEndpoint
{
    internal static RouteHandlerBuilder MapClearInvoiceEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPost("/ClearInvoice", async (HttpContext context, ClearInvoiceCommand input, ISender mediator) =>
            {
                if (context.Request.Headers.TryGetValue("Accept-Language", out var acceptLanguage))
                {
                    input.Lang = acceptLanguage.ToString();
                }

                var response = await mediator.Send(input);
                return Results.Ok(response);
            })
            .WithName(nameof(ClearInvoiceEndpoint))
            .WithSummary("Clear invoice")
            .WithDescription("This will clear the B2B eInvoice, hash it and get QR code")
            .Produces(StatusCodes.Status204NoContent)
            .RequirePermission("Permissions.Products.View")
            .MapToApiVersion(1);
    }
}
