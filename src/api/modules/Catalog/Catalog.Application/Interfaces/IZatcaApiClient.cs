using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Clearance;
using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Compliance;
using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Onboarding;
using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Reporting;

namespace FSH.Starter.WebApi.Zatca.Application.Interfaces;
public interface IZatcaApiClient: ITransientService
{
    /// <summary>
    /// Validate your generated invoice against Zatca Rules and schema structure
    /// </summary>
    /// <param name="request"></param>
    /// <param name="auth">Basic authentication header</param>
    /// <returns></returns>
    Task<ValidationResponse> ValidateInvoiceAsync(ValidateInvoiceRequest request, string auth);

    /// <summary>
    /// Onboard your solution to EGS using your generated certificate and the Otp
    /// </summary>
    /// <param name="request"></param>
    /// <returns>Csid: to be used as username and the secret to be used as password on the authentication header when interacting with EGS.</returns>
    /// <exception cref="InvalidOperationException"></exception>
    Task<OnboardingResponse> OnboardAsync(OnboardingRequest request);

    /// <summary>
    /// Get the basic authentication header content
    /// </summary>
    /// <param name="binarySecurityToken">This is called CSID or Certificate (X509 Certificate) it should be not formatted with BEGIN and END Certificate as its used as userName</param>
    /// <param name="secret">This is the secret and used as password on basic auth</param>
    /// <returns></returns>
    string GetAuthHeaders(string binarySecurityToken, string secret);


    /// <summary>
    /// Reports a single SIMPLIFIED invoice, credit note, or debit note. Specifically, it accepts simplified invoice, 
    /// credit note, or debit note encoded in base64 and validates it.
    /// This is used for B2C invoices
    /// </summary>
    /// <param name="request">request body</param>
    /// <param name="Auth">Basic authentication header</param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    Task<ReportingResponse> ReportInvoiceAsync(ReportingRequest request, string Auth);

    /// <summary>
    /// Clears a single Standard invoice, credit note, or debit note. Specifically, it accepts standard invoice, credit note, 
    /// or debit note encoded in base64 and validates it to ensure the below. On successful validation, 
    /// the API then signs the invoice, applies a QR code and returns back
    /// This is used for B2B invoices
    /// </summary>
    /// <param name="request">request body</param>
    /// <param name="Auth">Basic authentication header</param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    Task<ClearanceResponse> ClearInvoiceAsync(ClearanceRequest request, string Auth);

    /// <summary>
    /// Authenticate 
    /// </summary>
    /// <param name="request">ComplianceRequestId is unique id for your onbording soulution request</param>
    /// <param name="auth">Basic authentication header from onBoardAsync</param>
    /// <returns></returns>
    Task<OnboardingResponse> ProductionOnboardAsync(ProdCsidOnboardingRequest request, string auth);
    
    /// <summary>
    /// This is to renew you csid secret, simply like refresh token
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<OnboardingResponse> ProductionRenewCsidAsync(OnboardingRequest request);
}
