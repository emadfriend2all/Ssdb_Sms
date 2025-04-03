using FSH.Framework.Infrastructure.Auth.Policy;
using FSH.Starter.WebApi.Catalog.Application.Features.TaxCategories.Delete.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1.TaxCategories;
public static class DeleteTaxCategoryEndpoint
{
    internal static RouteHandlerBuilder MapTaxCategoryDeleteEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapDelete("/{id:guid}", async (int id, ISender mediator) =>
             {
                 await mediator.Send(new DeleteTaxCategoryCommand(id));
                 return Results.NoContent();
             })
            .WithName(nameof(DeleteTaxCategoryEndpoint))
            .WithSummary("deletes taxCategory by id")
            .WithDescription("deletes taxCategory by id")
            .Produces(StatusCodes.Status204NoContent)
            .RequirePermission("Permissions.TaxCategories.Delete")
            .MapToApiVersion(1);
    }
}
