using FSH.Framework.Infrastructure.Auth.Policy;
using FSH.Starter.WebApi.Catalog.Application.Features.Customers.Create.v1;
using FSH.Starter.WebApi.Catalog.Application.Features.Google.PlaceDetails.v1;
using FSH.Starter.WebApi.Catalog.Application.Features.Google.PlaceDetailsByLocation.v1;
using FSH.Starter.WebApi.Catalog.Application.Features.Google.Predictions.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1.Customers;
public static class GoogleServicesEndpoints
{
    internal static void MapGetPredictionsEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("GetPredictions/{input}", async (string input, ISender mediator) =>
        {
            var response = await mediator.Send(new GetGooglePredictionsRequest(input));
            return Results.Ok(response);
        })
        .WithName("GetGooglePredictions")
        .WithSummary("Get place predictions.")
        .RequirePermission("Permissions.Google.View")
        .Produces<GooglePlacePredictionsResponse>()
        .MapToApiVersion(1);
    }

    internal static void MapGetPlaceByIdEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("GetPlaceById", async ([AsParameters] GetGooglePlaceDetailsRequest request, ISender mediator) =>
        {
            var response = await mediator.Send(request);
            return Results.Ok(response);
        })
        .WithName("GetPlaceById")
        .WithSummary("Get place details.")
        .RequirePermission("Permissions.Google.View")
        .Produces<GetGooglePlaceDetailsResponse>()
        .MapToApiVersion(1);
    }

    internal static void MapGetPlaceByLocationEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("GePlaceByLocation", async ([AsParameters] GetGooglePlaceDetailsByLocationRequest request, ISender mediator) =>
        {
            var response = await mediator.Send(request);
            return Results.Ok(response);
        })
        .WithName("GetPlaceByLocation")
        .WithSummary("Get place details by location.")
        .RequirePermission("Permissions.Google.View")
        .Produces<GetGooglePlaceDetailsResponse>()
        .MapToApiVersion(1);
    }
}
