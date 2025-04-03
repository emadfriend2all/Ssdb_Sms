using FSH.Framework.Core.Paging;
using FSH.Framework.Core.Persistence;
using FSH.Starter.WebApi.Catalog.Application.Features.Invoices.Get.v1;
using FSH.Starter.WebApi.Catalog.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;


namespace FSH.Starter.WebApi.Catalog.Application.Features.Invoices.Search.v1;
public sealed class SearchInvoicesHandler(
    [FromKeyedServices("catalog:invoices")] IReadRepository<Invoice> repository)
    : IRequestHandler<SearchInvoicesCommand, PagedList<SearchInvoiceResponse>>
{
    public async Task<PagedList<SearchInvoiceResponse>> Handle(SearchInvoicesCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var spec = new SearchInvoiceSpecs(request);

        var items = await repository.ListAsync(spec, cancellationToken).ConfigureAwait(false);

        var totalCount = await repository.CountAsync(spec, cancellationToken).ConfigureAwait(false);

        return new PagedList<SearchInvoiceResponse>(items, request!.PageNumber, request!.PageSize, totalCount);
    }
}

