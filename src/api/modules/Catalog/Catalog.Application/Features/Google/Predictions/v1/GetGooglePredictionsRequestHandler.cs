using FSH.Starter.WebApi.Zatca.Application.Interfaces;
using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Google.Predictions.v1;

public class GetGooglePredictionsRequestHandler : IRequestHandler<GetGooglePredictionsRequest, GooglePlacePredictionsResponse>
{
    private readonly IGooglePlacesService _googlePlacesService;

    public GetGooglePredictionsRequestHandler(IGooglePlacesService googlePlacesService)
    {
        _googlePlacesService = googlePlacesService;
    }

    public async Task<GooglePlacePredictionsResponse> Handle(GetGooglePredictionsRequest request, CancellationToken cancellationToken)
    {
        var response = await _googlePlacesService.GetPlacePredictionsAsync(request.Input);
        if (response is null)
        {
            return new GooglePlacePredictionsResponse();
        }

        return response;
    }
}
