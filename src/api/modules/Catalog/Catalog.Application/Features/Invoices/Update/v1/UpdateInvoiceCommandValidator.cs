using FluentValidation;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Invoices.Update.v1;
public class UpdateInvoiceCommandValidator : AbstractValidator<UpdateInvoiceCommand>
{
    public UpdateInvoiceCommandValidator()
    {
        RuleFor(p => p.Name).NotEmpty().MinimumLength(2).MaximumLength(75);
        RuleFor(p => p.Price).GreaterThan(0);
    }
}
