using Ardalis.Specification;
using FSH.Starter.WebApi.Catalog.Domain;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Invoices.Get.v1;

public class GetInvoiceSpecs : Specification<Invoice, GetInvoiceResponse>
{
    public GetInvoiceSpecs(int id)
    {
        Query
            .Where(p => p.Id == id)
            .Include(p => p.InvoiceLines)
            .ThenInclude(p => p.TaxCategory)
            .Include(p => p.InvoiceSeller)
            .Include(p => p.InvoiceBuyer)
            .Include(p => p.Allowances)
            .Include(p => p.LegalMonetaryTotal)
            .Include(p => p.TaxTotal)
            .ThenInclude(p => p.TaxCategory)
            .Include(p => p.TaxTotal!.TaxSubtotals!);
    }
}
