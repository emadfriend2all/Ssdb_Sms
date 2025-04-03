using Mapster;
using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Google.PlaceDetails.v1;

public class GetGooglePlaceDetailsRequest : IRequest<GetGooglePlaceDetailsResponse>
{
    public string? PlaceId { get; set; }
}
