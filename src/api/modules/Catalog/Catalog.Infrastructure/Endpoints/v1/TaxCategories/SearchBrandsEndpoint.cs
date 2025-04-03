using FSH.Framework.Core.Paging;
using FSH.Framework.Infrastructure.Auth.Policy;
using FSH.Starter.WebApi.Catalog.Application.Features.TaxCategories.Get.v1;
using FSH.Starter.WebApi.Catalog.Application.Features.TaxCategories.Search.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1.TaxCategories;

public static class SearchTaxCategoriesEndpoint
{
    internal static RouteHandlerBuilder MapGetTaxCategoryListEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPost("/search", async (ISender mediator, [FromBody] SearchTaxCategoriesCommand command) =>
            {
                var response = await mediator.Send(command);
                return Results.Ok(response);
            })
            .WithName(nameof(SearchTaxCategoriesEndpoint))
            .WithSummary("Gets a list of taxCategories")
            .WithDescription("Gets a list of taxCategories with pagination and filtering support")
            .Produces<PagedList<TaxCategoryResponse>>()
            .RequirePermission("Permissions.TaxCategories.View")
            .MapToApiVersion(1);
    }
}
