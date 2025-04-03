using FSH.Framework.Infrastructure.Auth.Policy;
using FSH.Starter.WebApi.Catalog.Application.Features.Invoices.Get.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1.Invoices;
public static class GetInvoiceEndpoint
{
    internal static RouteHandlerBuilder MapGetInvoiceEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet("/{id:int}", async (int id, ISender mediator) =>
            {
                var response = await mediator.Send(new GetInvoiceRequest(id));
                return Results.Ok(response);
            })
            .WithName(nameof(GetInvoiceEndpoint))
            .WithSummary("gets invoice by id")
            .WithDescription("gets prodct by id")
            .Produces<GetInvoiceResponse>()
            .RequirePermission("Permissions.Invoices.View")
            .MapToApiVersion(1);
    }
}
