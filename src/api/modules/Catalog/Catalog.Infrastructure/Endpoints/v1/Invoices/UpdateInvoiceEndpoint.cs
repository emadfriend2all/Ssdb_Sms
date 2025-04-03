using FSH.Framework.Infrastructure.Auth.Policy;
using FSH.Starter.WebApi.Catalog.Application.Features.Invoices.Update.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1.Invoices;
public static class UpdateInvoiceEndpoint
{
    internal static RouteHandlerBuilder MapInvoiceUpdateEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPut("/{id:guid}", async (int id, UpdateInvoiceCommand request, ISender mediator) =>
            {
                if (id != request.Id) return Results.BadRequest();
                var response = await mediator.Send(request);
                return Results.Ok(response);
            })
            .WithName(nameof(UpdateInvoiceEndpoint))
            .WithSummary("update a invoice")
            .WithDescription("update a invoice")
            .Produces<UpdateInvoiceResponse>()
            .RequirePermission("Permissions.Invoices.Update")
            .MapToApiVersion(1);
    }
}
