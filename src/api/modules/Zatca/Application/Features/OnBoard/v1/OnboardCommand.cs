using System.ComponentModel;
using FSH.Starter.WebApi.Zatca.Infrastructure.Exceptions;
using System.Net;
using Shared.Contracts.Zatca.Onboarding;
using MediatR;
using Zatca.EInvoice.SDK.Contracts.Models;

namespace FSH.Starter.WebApi.Zatca.Application.Features.OnBoard.v1;
public sealed class OnboardCommand : IRequest<OnboardingResponse>
{
    public OnboardCommand()
    {
    }
    public OnboardCommand(string commonName, string serialNumber, string organizationIdentifier, string organizationUnitName, string organizationName, string countryName, string invoiceType, string locationAddress, string industryBusinessCategory, string? otp = null, EnvironmentType? environment = null)
    {
        if (string.IsNullOrEmpty(Otp) && Environment == EnvironmentType.Production)
        {
            throw new ComplianceCheckException("OTP is required on production env." + 400, HttpStatusCode.BadRequest);
        }

        CommonName = commonName;
        SerialNumber = serialNumber;
        OrganizationIdentifier = organizationIdentifier;
        OrganizationUnitName = organizationUnitName;
        OrganizationName = organizationName;
        CountryName = countryName;
        InvoiceType = invoiceType;
        LocationAddress = locationAddress;
        IndustryBusinessCategory = industryBusinessCategory;
        Environment = environment ?? EnvironmentType.Simulation;
        Otp = otp?? "123456";
    }

    public EnvironmentType Environment { get; set; }
    public string Otp { get; set; } = string.Empty;
    public string CommonName { get; set; }
    public string SerialNumber { get; set; }
    public string OrganizationIdentifier { get; set; }
    public string OrganizationUnitName { get; set; }
    public string OrganizationName { get; set; }
    public string CountryName { get; set; }
    public string InvoiceType { get; set; }
    public string LocationAddress { get; set; }
    public string IndustryBusinessCategory { get; set; }
}
