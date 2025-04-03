using FSH.Framework.Infrastructure.Auth.Policy;
using FSH.Starter.WebApi.Catalog.Application.Features.TaxCategories.Update.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1.TaxCategories;
public static class UpdateTaxCategoryEndpoint
{
    internal static RouteHandlerBuilder MapTaxCategoryUpdateEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPut("/{code}", async (string code, UpdateTaxCategoryCommand request, ISender mediator) =>
            {
                if (code != request.Id) return Results.BadRequest();
                var response = await mediator.Send(request);
                return Results.Ok(response);
            })
            .WithName(nameof(UpdateTaxCategoryEndpoint))
            .WithSummary("update a taxCategory")
            .WithDescription("update a taxCategory")
            .Produces<UpdateTaxCategoryResponse>()
            .RequirePermission("Permissions.TaxCategories.Update")
            .MapToApiVersion(1);
    }
}
