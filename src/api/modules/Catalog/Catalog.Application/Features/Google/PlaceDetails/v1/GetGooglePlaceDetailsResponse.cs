namespace FSH.Starter.WebApi.Catalog.Application.Features.Google.PlaceDetails.v1;

public class GetGooglePlaceDetailsResponse
{
    public string? StreetNumber { get; set; }
    public string? StreetName { get; set; }
    public string? Suburb { get; set; }
    public string? City { get; set; }
    public string? Province { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
}
