using FSH.Framework.Infrastructure.Auth.Policy;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Zatca.Application.Features.ValidateInvoice.v1;
public static class ValidateInvoiceEndpoint
{
    internal static RouteHandlerBuilder MapValidateInvoiceEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPost("/ValidateInvoice", async (ValidateInvoiceCommand input, ISender mediator) =>
            {
                var response = await mediator.Send(input);
                return Results.Ok(response);
            })
            .WithName(nameof(ValidateInvoiceEndpoint))
            .WithSummary("Validate eInvoice")
            .WithDescription("Validate eInvoice and return validation response")
            .Produces(StatusCodes.Status204NoContent)
            .RequirePermission("Permissions.Products.View")
            .MapToApiVersion(1);
    }
}
