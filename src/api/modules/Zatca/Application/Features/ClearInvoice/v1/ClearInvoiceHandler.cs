using MediatR;
using Microsoft.Extensions.Logging;
using Zatca.EInvoice.SDK;
using Mapster;
using FSH.Starter.WebApi.Zatca.Application.Features.OnBoard.v1;
using FSH.Starter.WebApi.Zatca.Application.Interfaces;
using FSH.Starter.WebApi.Zatca.Infrastructure.Utils;
using System.Text;
using Shared.Contracts.Zatca.Onboarding;
using Shared.Contracts.Zatca.Shared;
using Shared.Contracts.Zatca.Clearance;

namespace FSH.Starter.WebApi.Zatca.Application.Features.ClearInvoice.v1;
public sealed class ClearInvoiceHandler(ILogger<ClearInvoiceHandler> logger, IEgsService egsService, IMediator mediator) : IRequestHandler<ClearInvoiceCommand, ClearInvoiceResponse>
{
    public async Task<ClearInvoiceResponse> Handle(ClearInvoiceCommand request, CancellationToken cancellationToken)
    {
        ClearInvoiceResponse response = new();
        var organization = new OrganizationModel(
                "TST-886431145-399999999900003",                             // CommonName
                "1-TST|2-TST|3-ed22f1d8-e6a2-1118-9b58-d9a8f11e445f",        // SerialNumber (Must be 15 digits, start and end with '3')
                "399999999900003",                                           // OrganizationIdentifier (Must be 15 digits, start and end with '3')
                "Riyadh Branch",                                             // OrganizationUnitName (Should not contain special characters)
                "شركة توريد التكنولوجيا بأقصى سرعة المحدودة",                // OrganizationName (Should not contain special characters)
                "SA",                                                        // CountryName (Must be exactly 2 characters)
                "1100",                                                      // InvoiceType (Must be 4 digits and match [0-1]{4})
                "RRRD2929",                                                  // LocationAddress (Should not contain special characters)
                "أنشطة توريدات"                                              // IndustryBusinessCategory (Should not contain special characters)
            );

        // 1. Issue Compliance Certificate
        var onBoardRequest = organization.Adapt<OnboardCommand>();
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
