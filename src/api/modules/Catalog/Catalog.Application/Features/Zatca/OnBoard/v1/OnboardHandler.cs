using MediatR;
using Mapster;
using FSH.Starter.WebApi.Zatca.Application.Interfaces;
using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Onboarding;
using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Shared;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Zatca.OnBoard.v1;
public sealed class OnboardHandler(IEgsService egsService) : IRequestHandler<OnboardCommand, OnboardingResponse>
{
    public async Task<OnboardingResponse> Handle(OnboardCommand request, CancellationToken cancellationToken)
    {
        var organization = request.Adapt<OrganizationModel>();
        var csrResult = egsService.GenerateCsr(organization, request.Environment);

        var result = await egsService.LoginAsync(new OnboardingRequest(csrResult.Csr, request.Otp));
        result.PrivateKey = csrResult.PrivateKey;
        return result;
    }
}
