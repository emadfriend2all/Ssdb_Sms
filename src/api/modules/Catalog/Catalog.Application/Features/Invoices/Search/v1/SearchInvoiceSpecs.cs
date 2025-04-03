using Ardalis.Specification;
using FSH.Framework.Core.Paging;
using FSH.Framework.Core.Specifications;
using FSH.Starter.WebApi.Catalog.Application.Features.Invoices.Get.v1;
using FSH.Starter.WebApi.Catalog.Domain;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Invoices.Search.v1;
public class SearchInvoiceSpecs : EntitiesByPaginationFilterSpec<Invoice, SearchInvoiceResponse>
{
    public SearchInvoiceSpecs(SearchInvoicesCommand command)
        : base(command) =>
        Query
            .Include(p => p.InvoiceBuyer)
            .Include(p => p.InvoiceSeller)
            .Include(p => p.LegalMonetaryTotal)
            .OrderByDescending(c => c.Id, !command.HasOrderBy())
            .Where(p => p.InvoiceBuyer.Name!.Contains(command.BuyerName!),!string.IsNullOrEmpty(command.BuyerName))
            .Where(p => p.InvoiceBuyer.TaxNumber!.Contains(command.TaxNumber!),!string.IsNullOrEmpty(command.TaxNumber));
}
