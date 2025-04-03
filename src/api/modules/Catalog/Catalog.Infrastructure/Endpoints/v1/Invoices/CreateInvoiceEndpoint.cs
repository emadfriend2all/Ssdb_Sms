using FSH.Framework.Infrastructure.Auth.Policy;
using FSH.Starter.WebApi.Catalog.Application.Features.Invoices.Create.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1.Invoices;
public static class CreateInvoiceEndpoint
{
    internal static RouteHandlerBuilder MapInvoiceCreationEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPost("/", async (CreateInvoiceCommand request, ISender mediator) =>
            {
                var response = await mediator.Send(request);
                return Results.Ok(response);
            })
            .WithName(nameof(CreateInvoiceEndpoint))
            .WithSummary("creates a invoice")
            .WithDescription("creates a invoice")
            .Produces<CreateInvoiceResponse>()
            .RequirePermission("Permissions.Invoices.Create")
            .MapToApiVersion(1);
    }
}
