namespace FSH.Starter.WebApi.Catalog.Application.Features.Google.Predictions.v1;

public class GooglePlacePredictionsResponse
{
    public List<GooglePlacePrediction>? Predictions { get; set; }
    public string? Status { get; set; }
}

public class GooglePlacePrediction
{
    public string? Description { get; set; }
    public string? Place_Id { get; set; }
    public List<string> Types { get; set; }
}
