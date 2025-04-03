using FSH.Framework.Infrastructure.Auth.Policy;
using FSH.Starter.WebApi.Catalog.Application.Features.TaxCategories.Create.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1.TaxCategories;
public static class CreateTaxCategoryEndpoint
{
    internal static RouteHandlerBuilder MapTaxCategoryCreationEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPost("/", async (CreateTaxCategoryCommand request, ISender mediator) =>
            {
                var response = await mediator.Send(request);
                return Results.Ok(response);
            })
            .WithName(nameof(CreateTaxCategoryEndpoint))
            .WithSummary("creates a taxCategory")
            .WithDescription("creates a taxCategory")
            .Produces<CreateTaxCategoryResponse>()
            .RequirePermission("Permissions.TaxCategories.Create")
            .MapToApiVersion(1);
    }
}
