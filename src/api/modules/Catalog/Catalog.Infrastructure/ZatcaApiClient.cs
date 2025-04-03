using System.Net;
using System.Net.Http.Headers;
using System.Text;
using FSH.Starter.WebApi.Zatca.Application.Interfaces;
using FSH.Starter.WebApi.Catalog.Infrastructure.Exceptions;
using Newtonsoft.Json;
using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Clearance;
using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Compliance;
using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Onboarding;
using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Reporting;

namespace FSH.Starter.WebApi.Catalog.Infrastructure;

public class ZatcaApiClient : IZatcaApiClient
{
    public const string BaseUrl = $"https://gw-fatoora.zatca.gov.sa/e-invoicing/developer-portal/";
    public const string ComplianceCsidEndpoint = "compliance";
    public const string ComplianceProductionCsidEndpoint = "production/csids";
    public const string ComplianceProductionRenewCsidEndpoint = "production/csids";
    public const string ValidateInvoice = "compliance/invoices";
    public const string InvoiceClearanceEndPoint = "invoices/clearance/single";
    public const string InvoiceReportingEndPoint = "invoices/reporting/single";
    private const string Version = "V2";
    private readonly HttpClient _httpClient;

    public ZatcaApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public string GetAuthHeaders(string binarySecurityToken, string secret)
    {
        if (string.IsNullOrWhiteSpace(binarySecurityToken) || string.IsNullOrWhiteSpace(secret))
            return string.Empty;

        var authHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{binarySecurityToken}:{secret}"));
        return authHeader;
    }

    public async Task<OnboardingResponse> OnboardAsync(OnboardingRequest request)
    {
        using var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{BaseUrl}{ComplianceCsidEndpoint}");
        requestMessage.Headers.Add("Accept-Version", Version);
        requestMessage.Headers.Add("OTP", request.Otp);

        var requestPayload = new { csr = Convert.ToBase64String(Encoding.UTF8.GetBytes(request.Csr)) };
        var jsonStr = JsonConvert.SerializeObject(requestPayload, Formatting.Indented);
        var requestContent = new StringContent(jsonStr, Encoding.UTF8, "application/json");
        requestMessage.Content = requestContent;

        using var response = await _httpClient.SendAsync(requestMessage);

        if (!response.IsSuccessStatusCode)
        {
            throw new ComplianceCheckException(
                "Error issuing a compliance certificate. HTTP Status: " + response.StatusCode,
                response.StatusCode
            );
        }

        var responseBody = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<OnboardingResponse>(responseBody)!;
    }

    public async Task<OnboardingResponse> ProductionOnboardAsync(ProdCsidOnboardingRequest request, string auth)
    {
        using var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{BaseUrl}{ComplianceProductionCsidEndpoint}");
        requestMessage.Headers.Add("Accept-Version", Version);
        requestMessage.Headers.Add("Accept-Language", "en");
        if (!string.IsNullOrEmpty(auth))
        {
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", auth);
        }

        var requestObj = new { compliance_request_id = request.ComplianceRequestId };
        var jsonStr = JsonConvert.SerializeObject(requestObj, Formatting.Indented);
        var requestContent = new StringContent(jsonStr, Encoding.UTF8, "application/json");
        requestMessage.Content = requestContent;

        using var response = await _httpClient.SendAsync(requestMessage);
        var responseBody = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ComplianceCheckException(
                "Error in compliance check. HTTP Status: " + response.StatusCode,
                response.StatusCode
            );
        }

        return JsonConvert.DeserializeObject<OnboardingResponse>(responseBody)!;
    }

    public async Task<OnboardingResponse> ProductionRenewCsidAsync(OnboardingRequest request)
    {
        using var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{BaseUrl}{ComplianceProductionCsidEndpoint}");
        requestMessage.Headers.Add("Accept-Version", Version);
        requestMessage.Headers.Add("Accept-Language", "en");
        requestMessage.Headers.Add("OTP", request.Otp);

        var requestPayload = new { csr = Convert.ToBase64String(Encoding.UTF8.GetBytes(request.Csr)) };
        var jsonStr = JsonConvert.SerializeObject(requestPayload, Formatting.Indented);
        var requestContent = new StringContent(jsonStr, Encoding.UTF8, "application/json");
        requestMessage.Content = requestContent;

        using var response = await _httpClient.SendAsync(requestMessage);
        var responseBody = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ComplianceCheckException(
                "Error in compliance check. HTTP Status: " + response.StatusCode,
                response.StatusCode
            );
        }

        return JsonConvert.DeserializeObject<OnboardingResponse>(responseBody)!;
    }

    public async Task<ValidationResponse> ValidateInvoiceAsync(ValidateInvoiceRequest request, string auth)
    {
        var jsonStr = JsonConvert.SerializeObject(request, Formatting.Indented);
        var requestContent = new StringContent(jsonStr, Encoding.UTF8, "application/json");

        using var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{BaseUrl}{ValidateInvoice}");
        requestMessage.Headers.Add("Accept-Version", Version);
        requestMessage.Headers.Add("Accept-Language", "en");
        if (!string.IsNullOrEmpty(auth))
        {
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", auth);
        }
        requestMessage.Content = requestContent;

        using var response = await _httpClient.SendAsync(requestMessage);
        var responseBody = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ComplianceCheckException(
                "Error in compliance check. HTTP Status: " + response.StatusCode,
                response.StatusCode
            );
        }

        return JsonConvert.DeserializeObject<ValidationResponse>(responseBody)!;
    }

    public async Task<ClearanceResponse> ClearInvoiceAsync(ClearanceRequest request, string Auth)
    {
        var jsonStr = JsonConvert.SerializeObject(request, Formatting.Indented);
        var requestContent = new StringContent(jsonStr, Encoding.UTF8, "application/json");

        using var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{BaseUrl}{InvoiceClearanceEndPoint}");
        requestMessage.Headers.Add("Accept-Version", Version);
        requestMessage.Headers.Add("Accept-Language", request.Lang);
        requestMessage.Headers.Add("Clearance-Status", request.ClearanceStatus.ToString());

        if (!string.IsNullOrEmpty(Auth))
        {
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", Auth);
        }

        requestMessage.Content = requestContent;

        using var response = await _httpClient.SendAsync(requestMessage);
        var responseBody = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<ClearanceResponse>(responseBody)!;
    }

    public async Task<ReportingResponse> ReportInvoiceAsync(ReportingRequest request, string Auth)
    {
        var jsonStr = JsonConvert.SerializeObject(request, Formatting.Indented);
        var requestContent = new StringContent(jsonStr, Encoding.UTF8, "application/json");

        using var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{BaseUrl}{InvoiceReportingEndPoint}");
        requestMessage.Headers.Add("Accept-Version", Version);
        requestMessage.Headers.Add("Accept-Language", request.Lang);

        if (!string.IsNullOrEmpty(Auth))
        {
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", Auth);
        }

        requestMessage.Content = requestContent;

        using var response = await _httpClient.SendAsync(requestMessage);
        var responseBody = await response.Content.ReadAsStringAsync();
        
        return JsonConvert.DeserializeObject<ReportingResponse>(responseBody)!;
    }
}
