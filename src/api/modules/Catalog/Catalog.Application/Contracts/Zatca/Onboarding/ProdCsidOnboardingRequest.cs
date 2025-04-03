namespace FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Onboarding;

public class ProdCsidOnboardingRequest
{
    public ProdCsidOnboardingRequest(string complianceRequestId)
    {
        ComplianceRequestId = complianceRequestId;
    }

    public string ComplianceRequestId { get; }
}
