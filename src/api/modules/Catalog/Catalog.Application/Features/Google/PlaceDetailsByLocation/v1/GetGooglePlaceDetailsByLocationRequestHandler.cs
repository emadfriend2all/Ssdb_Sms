using FSH.Starter.WebApi.Catalog.Application.Features.Google.PlaceDetails.v1;
using FSH.Starter.WebApi.Zatca.Application.Interfaces;
using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Google.PlaceDetailsByLocation.v1;

public class GetGooglePlaceDetailsByLocationRequestHandler : IRequestHandler<GetGooglePlaceDetailsByLocationRequest, GetGooglePlaceDetailsResponse>
{
    private readonly IGooglePlacesService _googlePlacesService;

    public GetGooglePlaceDetailsByLocationRequestHandler(IGooglePlacesService googlePlacesService)
    {
        _googlePlacesService = googlePlacesService;
    }

    public async Task<GetGooglePlaceDetailsResponse> Handle(GetGooglePlaceDetailsByLocationRequest request, CancellationToken cancellationToken)
    {
        var googleResponse = await _googlePlacesService.GetPlaceDetailsByLocationAsync(request);
        if (googleResponse is null)
        {
            return new GetGooglePlaceDetailsResponse();
        }

        return googleResponse;
    }
}
