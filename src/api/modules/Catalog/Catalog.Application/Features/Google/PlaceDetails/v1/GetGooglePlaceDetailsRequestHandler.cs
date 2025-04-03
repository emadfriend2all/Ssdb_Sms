using FSH.Starter.WebApi.Zatca.Application.Interfaces;
using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Google.PlaceDetails.v1;

public class GetGooglePlaceDetailsRequestHandler : IRequestHandler<GetGooglePlaceDetailsRequest, GetGooglePlaceDetailsResponse>
{
    private readonly IGooglePlacesService _googlePlacesService;

    public GetGooglePlaceDetailsRequestHandler(IGooglePlacesService googlePlacesService)
    {
        _googlePlacesService = googlePlacesService;
    }

    public async Task<GetGooglePlaceDetailsResponse> Handle(GetGooglePlaceDetailsRequest request, CancellationToken cancellationToken)
    {
        var googleResponse = await _googlePlacesService.GetPlaceDetailsAsync(request);
        if (googleResponse is null)
        {
            return new GetGooglePlaceDetailsResponse();
        }

        return googleResponse;
    }
}
