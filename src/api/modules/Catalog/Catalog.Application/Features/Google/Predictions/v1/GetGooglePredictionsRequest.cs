using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Google.Predictions.v1;

public class GetGooglePredictionsRequest : IRequest<GooglePlacePredictionsResponse>
{
    public string Input { get; set; }
    public GetGooglePredictionsRequest(string input) => Input = input;
}
