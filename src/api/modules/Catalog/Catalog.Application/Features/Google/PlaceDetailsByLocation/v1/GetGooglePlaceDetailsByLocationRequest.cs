using FSH.Starter.WebApi.Catalog.Application.Features.Google.PlaceDetails.v1;
using Mapster;
using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Google.PlaceDetailsByLocation.v1;

public class GetGooglePlaceDetailsByLocationRequest : IRequest<GetGooglePlaceDetailsResponse>
{
    public double Lat { get; set; }
    public double Lng { get; set; }
}
