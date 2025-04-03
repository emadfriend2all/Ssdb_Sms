using FSH.Framework.Infrastructure.Auth.Policy;
using FSH.Starter.WebApi.Catalog.Application.Features.TaxCategories.Get.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1.TaxCategories;
public static class GetTaxCategoryEndpoint
{
    internal static RouteHandlerBuilder MapGetTaxCategoryEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet("/{code}", async (string code, ISender mediator) =>
            {
                var response = await mediator.Send(new GetTaxCategoryRequest(code));
                return Results.Ok(response);
            })
            .WithName(nameof(GetTaxCategoryEndpoint))
            .WithSummary("gets taxCategory by id")
            .WithDescription("gets taxCategory by id")
            .Produces<TaxCategoryResponse>()
            .RequirePermission("Permissions.TaxCategories.View")
            .MapToApiVersion(1);
    }
}
