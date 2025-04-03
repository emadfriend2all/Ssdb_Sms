
using System.Globalization;
using FSH.Starter.WebApi.Catalog.Application.Features.Google.PlaceDetails.v1;
using FSH.Starter.WebApi.Catalog.Application.Features.Google.PlaceDetailsByLocation.v1;
using FSH.Starter.WebApi.Catalog.Application.Features.Google.Predictions.v1;
using FSH.Starter.WebApi.Catalog.Infrastructure.GeoLocation;
using FSH.Starter.WebApi.Zatca.Application.Interfaces;
namespace FSH.Starter.WebApi.Catalog.Infrastructure;
public class GooglePlacesService : IGooglePlacesService
{
    private readonly HttpClient _httpClient;
    private string _apiKey = "AIzaSyAJjQL8vtuDgcicjS9XPQsJ7EbYRKQXM48";
    private string _language = "ar";
    private string _country = "SA";
    private string _predictionUrl = "https://maps.googleapis.com/maps/api/place/autocomplete/json";
    private string _geocodesUrl = "https://maps.googleapis.com/maps/api/geocode/json";
    private string _fields = "formatted_address,name,address_component,geometry";
    public GooglePlacesService()
    {
        _httpClient = new HttpClient();
    }

    #region Predictions
    public async Task<GooglePlacePredictionsResponse?> GetPlacePredictionsAsync(string input, List<string>? types = null)
    {
        string apiUrl = $"{_predictionUrl}?input={input}&key={_apiKey}&components=country:{_country}&language={_language}";

        if (types != null && types.Any())
        {
            string typesString = string.Join("|", types);
            apiUrl += $"&types={typesString}";
        }

        var response = await _httpClient.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<GooglePlacePredictionsResponse>(content);
        }

        return null;
    }
    #endregion

    public async Task<GetGooglePlaceDetailsResponse?> GetPlaceDetailsAsync(GetGooglePlaceDetailsRequest request)
    {
        string apiUrl = $"{_geocodesUrl}?place_id={request.PlaceId}&language={_language}&fields={_fields}&key={_apiKey}";

        var response = await _httpClient.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            var placeResult = Newtonsoft.Json.JsonConvert.DeserializeObject<GooglePlaceDetailsByLocationResponse>(content);
            return MapeToResponse(placeResult);
        }

        return null;
    }

    public async Task<GetGooglePlaceDetailsResponse> GetPlaceDetailsByLocationAsync(GetGooglePlaceDetailsByLocationRequest request)
    {

        string lat = request.Lat.ToString(new CultureInfo("en-US", true)).Replace(',', '.');
        string lng = request.Lng.ToString(new CultureInfo("en-US", true)).Replace(',', '.');
        string apiUrl = $"{_geocodesUrl}?latlng={lat},{lng}&language={_language}&fields={_fields}&key={_apiKey}";

        var response = await _httpClient.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            var placeResult = Newtonsoft.Json.JsonConvert.DeserializeObject<GooglePlaceDetailsByLocationResponse>(content);
            return MapeToResponse(placeResult);
        }

        return null;
    }

    private static GetGooglePlaceDetailsResponse MapeToResponse(GooglePlaceDetailsByLocationResponse? placeResult)
    {
        return new GetGooglePlaceDetailsResponse()
        {
            StreetNumber = placeResult?.Results?.FirstOrDefault()?.AddressComponents?.FirstOrDefault(x => x.Types.Contains("street_number"))?.LongName,
            StreetName = placeResult?.Results?.FirstOrDefault()?.AddressComponents?.FirstOrDefault(x => x.Types.Contains("route"))?.LongName,
            Suburb = placeResult?.Results?.FirstOrDefault()?.AddressComponents?.FirstOrDefault(x => x.Types.Contains("sublocality"))?.LongName,
            City = placeResult?.Results?.FirstOrDefault()?.AddressComponents?.FirstOrDefault(x => x.Types.Contains("locality"))?.LongName,
            Province = placeResult?.Results?.FirstOrDefault()?.AddressComponents?.FirstOrDefault(x => x.Types.Contains("administrative_area_level_1"))?.LongName,
            PostalCode = placeResult?.Results?.FirstOrDefault()?.AddressComponents?.FirstOrDefault(x => x.Types.Contains("postal_code"))?.LongName,
            Country = placeResult?.Results?.FirstOrDefault()?.AddressComponents?.FirstOrDefault(x => x.Types.Contains("country"))?.LongName,
            Latitude = ConvertToDouble(placeResult?.Results?.FirstOrDefault()?.Geometry?.Location?.Lat.ToString(new CultureInfo("en-US", true))),
            Longitude = ConvertToDouble(placeResult?.Results?.FirstOrDefault()?.Geometry?.Location?.Lng.ToString(new CultureInfo("en-US", true))),
        };
    }

    private static double? ConvertToDouble(string? value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            value = value.Replace('٫', '.');
            return Convert.ToDouble(value, CultureInfo.InvariantCulture);
        }

        return null;
    }
}
