using FluentValidation;
using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Invoices;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Zatca.ReportInvoice.v1;
public class ReportInvoiceValidator : AbstractValidator<StandardInvoice>
{
    public ReportInvoiceValidator()
    {
        RuleFor(p => p.InvoiceBuyer).NotEmpty();
        RuleFor(p => p.InvoiceSeller).NotEmpty();
    }
}
