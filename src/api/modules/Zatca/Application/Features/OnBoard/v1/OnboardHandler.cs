using MediatR;
using Microsoft.Extensions.Logging;
using Zatca.EInvoice.SDK.Contracts.Models;
using Zatca.EInvoice.SDK;
using Mapster;
using FSH.Starter.WebApi.Zatca.Application.Interfaces;
using System.Globalization;
using System.Text;
using Azure;
using FSH.Starter.WebApi.Zatca.Infrastructure.Exceptions;
using System.Net;
using Shared.Contracts.Zatca.Onboarding;
using Shared.Contracts.Zatca.Shared;

namespace FSH.Starter.WebApi.Zatca.Application.Features.OnBoard.v1;
public sealed class OnboardHandler(IEgsService egsService) : IRequestHandler<OnboardCommand, OnboardingResponse>
{
    public async Task<OnboardingResponse> Handle(OnboardCommand request, CancellationToken cancellationToken)
    {
        var organization = request.Adapt<OrganizationModel>();
        var csrResult = egsService.GenerateCsr(organization, request.Environment);

        var result = await egsService.LoginAsync( new OnboardingRequest (csrResult.Csr, request.Otp));
        result.PrivateKey = csrResult.PrivateKey;
        return result;
    }
}
