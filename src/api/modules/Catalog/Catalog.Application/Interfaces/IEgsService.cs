using System.Xml;
using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Clearance;
using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Compliance;
using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.GenerateCsr;
using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Onboarding;
using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Reporting;
using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Shared;
using Zatca.EInvoice.SDK.Contracts.Models;

namespace FSH.Starter.WebApi.Zatca.Application.Interfaces;
public interface IEgsService : IScopedService
{
    /// <summary>
    /// Generate the Certificate using organization info
    /// </summary>
    /// <param name="organizationInfo">Organization Info</param>
    /// <param name="environment">EnvironmentType:Production, Simulation or NonProduction</param>
    /// <returns></returns>
    GenerateCsrResponse GenerateCsr(OrganizationModel organizationInfo, EnvironmentType environment);

    /// <summary>
    /// Login to EGS using your generated certificate and the Otp
    /// </summary>
    /// <param name="request"></param>
    /// <returns>Csid to be used as username and the secret to be used as password on the authentication header when interacting with EGS.</returns>
    /// <exception cref="InvalidOperationException"></exception>
    Task<OnboardingResponse> LoginAsync(OnboardingRequest request);

    /// <summary>
    /// This will validate the issued invoice only to get its status
    /// </summary>
    /// <param name="request">validation request </param>
    /// <returns> Validation response, clearance and validation statuses</returns>
    /// <exception cref="InvalidOperationException"></exception>
    Task<ValidationResponse> ValidateInvoiceAsync(ValidateInvoiceRequest request);

    /// <summary>
    /// Clears a single Standard invoice, credit note, or debit note. Specifically, it accepts standard invoice, credit note, 
    /// or debit note encoded in base64 and validates it to ensure the below. On successful validation, 
    /// the API then signs the invoice, applies a QR code and returns back
    /// This is used for B2B invoices
    /// </summary>
    /// <param name="request">request body</param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    Task<ClearanceResponse> ClearInvoiceAsync(ClearanceRequest request);

    /// <summary>
    /// Reports a single SIMPLIFIED invoice, credit note, or debit note. Specifically, it accepts simplified invoice, 
    /// credit note, or debit note encoded in base64 and validates it.
    /// This is used for B2C invoices
    /// </summary>
    /// <param name="request">request body</param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    Task<ReportingResponse> ReportInvoiceAsync(ReportingRequest request);

    /// <summary>
    /// Save credentials to be used in the next api calls when needed.
    /// </summary>
    /// <param name="certificate">This is the certificate as username</param>
    /// <param name="secret">This is the secret as password</param>
    void SetCredentials(string certificate, string secret);

    /// <summary>
    /// Login to EGS prodction environment using your csr and secret
    /// </summary>
    /// <param name="request">ComplianceRequestId is unique id for your onbording soulution request</param>
    /// <returns>Csid to be used as username and the secret to be used as password on the authentication header when interacting with EGS.</returns>
    /// <exception cref="InvalidOperationException"></exception>
    Task<OnboardingResponse> ProductionLoginAsync(ProdCsidOnboardingRequest request);
}
