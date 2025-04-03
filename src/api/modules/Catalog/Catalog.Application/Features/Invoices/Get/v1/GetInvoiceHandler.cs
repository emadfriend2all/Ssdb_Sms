using Microsoft.Extensions.DependencyInjection;
using FSH.Starter.WebApi.Catalog.Domain.Exceptions;
using FSH.Framework.Core.Persistence;
using FSH.Framework.Core.Caching;
using FSH.Starter.WebApi.Catalog.Domain;
using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Invoices.Get.v1;
public sealed class GetInvoiceHandler(
    [FromKeyedServices("catalog:invoices")] IReadRepository<Invoice> repository,
    ICacheService cache)
    : IRequestHandler<GetInvoiceRequest, GetInvoiceResponse>
{
    public async Task<GetInvoiceResponse> Handle(GetInvoiceRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var item = await cache.GetOrSetAsync(
            $"invoice:{request.Id}",
            async () =>
            {
                var spec = new GetInvoiceSpecs(request.Id);
                var invoiceItem = await repository.FirstOrDefaultAsync(spec, cancellationToken);
                if (invoiceItem == null) throw new InvoiceNotFoundException(request.Id);
                return invoiceItem;
            },
            cancellationToken: cancellationToken);
        return item!;
    }
}
