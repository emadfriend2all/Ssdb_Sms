using FluentValidation;
using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Invoices;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Zatca.ClearInvoice.v1;
public class ClearInvoiceValidator : AbstractValidator<StandardInvoice>
{
    public ClearInvoiceValidator()
    {
        RuleFor(p => p.InvoiceBuyer).NotEmpty();
        RuleFor(p => p.InvoiceSeller).NotEmpty();
    }
}
