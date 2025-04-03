using Ardalis.Specification;
using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Invoices;
using FSH.Starter.WebApi.Catalog.Domain;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Invoices.Print.v1;

public class PrintInvoiceSpecs : Specification<Invoice, Invoice>
{
    public PrintInvoiceSpecs(int id)
    {
        Query
            .Where(p => p.Id == id)
            .Include(p => p.InvoiceLines)
            .ThenInclude(p => p.TaxCategory)
            .Include(p => p.InvoiceSeller)
            .Include(p => p.InvoiceSeller.Address)
            .Include(p => p.InvoiceBuyer)
            .Include(p => p.InvoiceBuyer.Address)
            .Include(p => p.Allowances)
            .Include(p => p.LegalMonetaryTotal)
            .Include(p => p.TaxTotal)
            .ThenInclude(p => p.TaxCategory)
            .Include(p => p.TaxTotal!.TaxSubtotals)
            ;
    }
}
