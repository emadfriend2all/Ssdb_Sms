using FSH.Starter.WebApi.Catalog.Application.Features.Google.PlaceDetails.v1;
using FSH.Starter.WebApi.Catalog.Application.Features.Google.PlaceDetailsByLocation.v1;
using FSH.Starter.WebApi.Catalog.Application.Features.Google.Predictions.v1;

namespace FSH.Starter.WebApi.Zatca.Application.Interfaces;
public interface IGooglePlacesService: ITransientService
{
    Task<GetGooglePlaceDetailsResponse?> GetPlaceDetailsAsync(GetGooglePlaceDetailsRequest request);
    Task<GetGooglePlaceDetailsResponse> GetPlaceDetailsByLocationAsync(GetGooglePlaceDetailsByLocationRequest request);
    Task<GooglePlacePredictionsResponse?> GetPlacePredictionsAsync(string input, List<string>? types = null);
}
