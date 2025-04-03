using FluentValidation;
using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Invoices;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Zatca.SubmitInvoice.v1;
public class SubmitInvoiceValidator : AbstractValidator<StandardInvoice>
{
    public SubmitInvoiceValidator()
    {
        RuleFor(p => p.InvoiceBuyer).NotEmpty();
        RuleFor(p => p.InvoiceSeller).NotEmpty();
    }
}
