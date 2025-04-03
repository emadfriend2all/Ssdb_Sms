using FSH.Framework.Core.Paging;
using FSH.Starter.WebApi.Catalog.Application.Features.Invoices.Get.v1;
using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Invoices.Search.v1;

public class SearchInvoicesCommand : PaginationFilter, IRequest<PagedList<SearchInvoiceResponse>>
{
    public string? BuyerName { get; set; }
    public string? TaxNumber { get; set; }
    public new string[]? OrderBy { get; set; } = ["id desc"];
}
