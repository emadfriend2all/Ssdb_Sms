using System.Text;

namespace Shared.Contracts.Zatca.Onboarding;
public class OnboardingResponse
{
    /// <summary>
    /// Unique identifier for your request in EGS
    /// </summary>
    public string? RequestID { get; set; }

    /// <summary>
    /// Status of your certificate request, e.g., "ISSUED" or "FAILED".
    /// </summary>
    public string? DispositionMessage { get; set; }

    /// <summary>
    /// Issued certificate as Base64
    /// </summary>
    public string BinarySecurityToken { get; set; } = string.Empty;

    /// <summary>
    /// Secret on EGS is used as password
    /// </summary>
    public string Secret { get; set; } = string.Empty;

    /// <summary>
    /// Certificate private key is used to digitally sign invoice hashes before sending them to ZATCA
    /// </summary>
    public string PrivateKey { get; set; } = string.Empty;

    /// <summary>
    /// The issued certificate string (Not decorated) on EGS is used as username 
    /// </summary>
    public string IssuedCertificate => BinarySecurityToken is not null
                ? Encoding.UTF8.GetString(Convert.FromBase64String(BinarySecurityToken))
                : string.Empty;

    /// <summary>
    /// The formatted issued certificate decorated with Begin and End certificate 
    /// </summary>
    public string FormatedCertificate
    {
        get
        {
            return IssuedCertificate is not null
                ? $"-----BEGIN CERTIFICATE-----\n{Encoding.UTF8.GetString(Convert.FromBase64String(BinarySecurityToken))}\n-----END CERTIFICATE-----"
                : string.Empty;
        }
    }
}
