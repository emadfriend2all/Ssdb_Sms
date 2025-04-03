using FSH.Framework.Core.Paging;
using FSH.Framework.Infrastructure.Auth.Policy;
using FSH.Starter.WebApi.Catalog.Application.Features.Invoices.Get.v1;
using FSH.Starter.WebApi.Catalog.Application.Features.Invoices.Search.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1.Invoices;

public static class SearchInvoicesEndpoint
{
    internal static RouteHandlerBuilder MapGetInvoiceListEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPost("/search", async (ISender mediator, [FromBody] SearchInvoicesCommand command) =>
            {
                var response = await mediator.Send(command);
                return Results.Ok(response);
            })
            .WithName(nameof(SearchInvoicesEndpoint))
            .WithSummary("Gets a list of invoices")
            .WithDescription("Gets a list of invoices with pagination and filtering support")
            .Produces<PagedList<SearchInvoiceResponse>>()
            .RequirePermission("Permissions.Invoices.View")
            .MapToApiVersion(1);
    }
}

