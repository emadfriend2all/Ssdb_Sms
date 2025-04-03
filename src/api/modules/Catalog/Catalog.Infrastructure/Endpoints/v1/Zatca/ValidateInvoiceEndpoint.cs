using FSH.Framework.Infrastructure.Auth.Policy;
using FSH.Starter.WebApi.Catalog.Application.Features.Zatca.ValidateInvoice.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1.Zatca;
public static class ValidateInvoiceEndpoint
{
    internal static RouteHandlerBuilder MapValidateInvoiceEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPost("/validateInvoice", async (ValidateInvoiceCommand input, ISender mediator) =>
            {
                var response = await mediator.Send(input);
                return Results.Ok(response);
            })
            .WithName(nameof(ValidateInvoiceEndpoint))
            .WithSummary("Validate eInvoice")
            .WithDescription("Validate eInvoice and return validation response")
            .Produces(StatusCodes.Status204NoContent)
            .RequirePermission("Permissions.Zatca.Create")
            .MapToApiVersion(1);
    }
}
