using FluentValidation;
using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Invoices;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Zatca.ValidateInvoice.v1;
public class ValidateInvoiceValidator : AbstractValidator<StandardInvoice>
{
    public ValidateInvoiceValidator()
    {
        RuleFor(p => p.InvoiceBuyer).NotEmpty();
        RuleFor(p => p.InvoiceSeller).NotEmpty();
    }
}
