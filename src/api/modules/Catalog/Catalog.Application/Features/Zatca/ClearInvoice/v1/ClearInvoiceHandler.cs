using MediatR;
using Microsoft.Extensions.Logging;
using Zatca.EInvoice.SDK;
using Mapster;
using FSH.Starter.WebApi.Zatca.Application.Interfaces;
using FSH.Starter.WebApi.Catalog.Application.Utils;
using System.Text;
using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Onboarding;
using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Shared;
using FSH.Starter.WebApi.Catalog.Application.Features.Zatca.OnBoard.v1;
using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Clearance;
using FSH.Starter.WebApi.Catalog.Domain;
using FSH.Starter.WebApi.Catalog.Application.Features.Invoices.Update.v1;
using FSH.Framework.Core.Identity.Users.Abstractions;
using FSH.Framework.Core.Tenant.Abstractions;
using Zatca.EInvoice.SDK.Contracts.Models;
using FSH.Starter.WebApi.Catalog.Domain.Exceptions;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Zatca.ClearInvoice.v1;
public sealed class ClearInvoiceHandler(ILogger<ClearInvoiceHandler> logger, IEgsService egsService, IMediator mediator, ICurrentUser currentUser, ITenantService tenantService) : IRequestHandler<ClearInvoiceCommand, ClearInvoiceResponse>
{
    public async Task<ClearInvoiceResponse> Handle(ClearInvoiceCommand request, CancellationToken cancellationToken)
    {
        ClearInvoiceResponse response = new();

        var tenantId = currentUser.GetTenant() ?? string.Empty;
        var user = await tenantService.GetBasicTenantInfoAsync(tenantId);
        var organization = user.Organization.Adapt<OrganizationModel>();

        // 1. Issue Compliance Certificate
        var onBoardRequest = organization.Adapt<OnboardCommand>();

       var envType = (EnvironmentType?)Enum.Parse(typeof(EnvironmentType), request.Environment?? "Simulation")
            ?? throw new AppBadRequestException("Invalid environment");

        onBoardRequest.Environment = envType;
        onBoardRequest.InvoiceType = InvoiceHelper.GetInvoiceTypeId(request.InvoiceType!);
        var certResult = await mediator.Send(onBoardRequest, cancellationToken);

        // 2. Generate XML invoice
        var generateXmlResult = StandardInvoiceXmlGenerator.Generate(request);
        var eInvoice = generateXmlResult.XmlInvoice;

        // 3. Sign Invoice
        var signer = new EInvoiceSigner();
        var signResult = signer.SignDocument(eInvoice, certResult.IssuedCertificate, certResult.PrivateKey);
        logger.LogInformation("Signed Invoice: {SignedInvoice}", signResult.SignedEInvoice);
        signResult.SignedEInvoice.Save($"E:\\signed{DateTime.Now:yyyyMMHHmmss}.xml");

        // Generate QR Code
        var qrGenerator = new EInvoiceQRGenerator();
        var qrResult = qrGenerator.GenerateEInvoiceQRCode(signResult.SignedEInvoice);

        await egsService.ProductionLoginAsync(new ProdCsidOnboardingRequest(certResult.RequestID!));
        var clearanceResult = await egsService.ClearInvoiceAsync(new ClearanceRequest
        {
            ClearanceStatus = 1,
            Invoice = Convert.ToBase64String(Encoding.UTF8.GetBytes(signResult.SignedEInvoice.OuterXml)),
            InvoiceHash = generateXmlResult.InvoiceHash,
            UUID = generateXmlResult.UUID,
        });

        response.IsSuccessful = signResult.IsValid && qrResult.IsValid && clearanceResult.ClearanceStatus == "CLEARED";
        response.ClearedInvoice = clearanceResult.ClearedInvoice;
        response.ValidationResults = clearanceResult.ValidationResults;
        response.SingedXML = signResult.SignedEInvoice.OuterXml;
        response.QrCode = qrResult.QR;
        response.Status = clearanceResult.ClearanceStatus;
        response.UUID = generateXmlResult.UUID;

        return response;
    }
}
