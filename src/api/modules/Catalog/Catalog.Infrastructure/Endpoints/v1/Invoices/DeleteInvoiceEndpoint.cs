using FSH.Framework.Infrastructure.Auth.Policy;
using FSH.Starter.WebApi.Catalog.Application.Features.Invoices.Delete.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1.Invoices;
public static class DeleteInvoiceEndpoint
{
    internal static RouteHandlerBuilder MapInvoiceDeleteEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapDelete("/{id:guid}", async (int id, ISender mediator) =>
             {
                 await mediator.Send(new DeleteInvoiceCommand(id));
                 return Results.NoContent();
             })
            .WithName(nameof(DeleteInvoiceEndpoint))
            .WithSummary("deletes invoice by id")
            .WithDescription("deletes invoice by id")
            .Produces(StatusCodes.Status204NoContent)
            .RequirePermission("Permissions.Invoices.Delete")
            .MapToApiVersion(1);
    }
}
