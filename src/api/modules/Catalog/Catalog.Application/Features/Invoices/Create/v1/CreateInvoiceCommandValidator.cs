using FluentValidation;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Invoices.Create.v1;
public class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand>
{
    public CreateInvoiceCommandValidator()
    {
        RuleFor(p => p.Number).NotEmpty();
        RuleFor(p => p.InvoiceBuyer.Name).NotEmpty();
    }
}
