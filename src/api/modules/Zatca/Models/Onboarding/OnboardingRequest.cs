namespace Shared.Contracts.Zatca.Onboarding;

public class OnboardingRequest
{
    public OnboardingRequest(string csr, string otp, string? lang = null)
    {
        Csr = csr;
        Otp = string.IsNullOrEmpty(otp) ? "123456" : otp;
        Lang = lang ?? "ar";
    }

    /// <summary>
    /// Generated Certificate from your organization info
    /// </summary>
    public string Csr { get; set; }

    /// <summary>
    /// On NonProduction environment this can be any 6 digits, otherwise you will need to have this when regestering the solution on EGS
    /// </summary>
    public string Otp { get; set; }

    /// <summary>
    /// Specifies the langauge in which the response will be returned
    /// </summary>
    public string? Lang { get; set; }
}
