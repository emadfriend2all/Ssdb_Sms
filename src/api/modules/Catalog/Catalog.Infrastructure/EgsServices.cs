using FSH.Starter.WebApi.Zatca.Application.Interfaces;
using Mapster;
using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Clearance;
using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Compliance;
using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.GenerateCsr;
using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Onboarding;
using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Reporting;
using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Shared;
using Zatca.EInvoice.SDK;
using Zatca.EInvoice.SDK.Contracts.Models;

namespace FSH.Starter.WebApi.Catalog.Infrastructure;
public class EgsService(IZatcaApiClient apiService) : IEgsService
{
    private readonly IZatcaApiClient _api = apiService ?? throw new ArgumentNullException(nameof(apiService));
    private string Certificate { get; set; } = default!;
    private string Secret { get; set; } = default!;

    public void SetCredentials(string certificate, string secret)
    {
        if (string.IsNullOrEmpty(certificate))
        {
            throw new InvalidOperationException("The Certificate/private key is required.");
        }

        if (string.IsNullOrEmpty(secret))
        {
            throw new InvalidOperationException("The api secret is required.");
        }

        Certificate = certificate;
        Secret = secret;
    }

    public bool IsAuthenticated()
    {
        return Certificate is not null && Secret is not null; 
    }

    public GenerateCsrResponse GenerateCsr(OrganizationModel organizationInfo, EnvironmentType environment)
    {
        var csrDto = organizationInfo.Adapt<CsrGenerationDto>();
        var csrResult = new CsrGenerator().GenerateCsr(csrDto, environment, true);

        return new GenerateCsrResponse
        {
            Csr = csrResult.Csr,
            PrivateKey = csrResult.PrivateKey,
        };
    }

    public async Task<OnboardingResponse> LoginAsync(OnboardingRequest request)
    {
        if (string.IsNullOrEmpty(request.Csr))
            throw new InvalidOperationException("You need to generate a CSR first.");

        if (string.IsNullOrEmpty(request.Otp))
            throw new InvalidOperationException("Otp is required.");

        var prodResponse = await _api.OnboardAsync(request);
        SetCredentials(prodResponse.BinarySecurityToken, prodResponse.Secret);
        return prodResponse;
    }

    public async Task<OnboardingResponse> ProductionLoginAsync(ProdCsidOnboardingRequest request)
    {
        if (!IsAuthenticated())
        {
            throw new InvalidOperationException("You are not authorized to access production onboarding.");
        }

        var auth = _api.GetAuthHeaders(Certificate, Secret);
        var response = await _api.ProductionOnboardAsync(new ProdCsidOnboardingRequest(request.ComplianceRequestId), auth);
        SetCredentials(response.BinarySecurityToken, response.Secret);
        return response;
    }

    public async Task<ClearanceResponse> ClearInvoiceAsync(ClearanceRequest request)
    {
        if (!IsAuthenticated())
        {
            throw new InvalidOperationException("You are not authorized to clear the invoice compliance.");
        }

        var auth = _api.GetAuthHeaders(Certificate, Secret);
        return await _api.ClearInvoiceAsync(request, auth);
    }

    public async Task<ReportingResponse> ReportInvoiceAsync(ReportingRequest request)
    {
        if (!IsAuthenticated())
        {
            throw new InvalidOperationException("You are not authorized to report the invoice compliance.");
        }
        var auth = _api.GetAuthHeaders(Certificate, Secret);
        return await _api.ReportInvoiceAsync(request, auth);
    }

    public async Task<ValidationResponse> ValidateInvoiceAsync(ValidateInvoiceRequest request)
    {
        if (!IsAuthenticated())
        {
            throw new InvalidOperationException("You are not authorized to validate the invoice compliance.");
        }

        var auth = _api.GetAuthHeaders(Certificate, Secret);
        var checkResult = await _api.ValidateInvoiceAsync(request, auth);

        return checkResult;
    }
}
