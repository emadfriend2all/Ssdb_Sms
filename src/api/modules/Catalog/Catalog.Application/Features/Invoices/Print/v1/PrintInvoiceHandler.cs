using Microsoft.Extensions.DependencyInjection;
using FSH.Starter.WebApi.Catalog.Domain.Exceptions;
using FSH.Framework.Core.Persistence;
using FSH.Framework.Core.Caching;
using FSH.Starter.WebApi.Catalog.Domain;
using MediatR;
using QuestPDF.Fluent;
using FSH.Starter.WebApi.Catalog.Application.Utils;
using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Invoices;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Invoices.Print.v1;
public sealed class PrintInvoiceHandler(
    [FromKeyedServices("catalog:invoices")] IReadRepository<Invoice> repository,
    ICacheService cache)
    : IRequestHandler<PrintInvoiceRequest, PrintInvoiceResponse>
{
    public async Task<PrintInvoiceResponse> Handle(PrintInvoiceRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var item = await cache.GetOrSetAsync(
            $"invoice:{request.Id}",
            async () =>
            {
                var spec = new PrintInvoiceSpecs(request.Id);
                var invoiceItem = await repository.FirstOrDefaultAsync(spec, cancellationToken);

                if(invoiceItem == null)
                {
                    throw new AppBadRequestException($"Invalid invoice id.");
                }
                var document = new StandardInvoiceDocument(invoiceItem);
                var bytes = document.GeneratePdf();

                PDFA3Result result = PdfHelper.ConvertToPDFA3(bytes, new Dictionary<string, string>
                {
                    {"QrCode.txt",invoiceItem.QrCode?? string.Empty},
                    {"SingedXML.xml",invoiceItem.SingedXML?? string.Empty}
                });

                return new PrintInvoiceResponse { 
                    File = result.PDFA3ContentFile,
                    FileName = result.PDFA3FileName
                };
            },
            cancellationToken: cancellationToken);
        return item!;
    }
}
