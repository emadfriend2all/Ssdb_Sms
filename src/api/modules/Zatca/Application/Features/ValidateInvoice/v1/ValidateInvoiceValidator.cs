using FluentValidation;
using FSH.Starter.WebApi.Zatca.Persistence;
using Shared.Contracts.Zatca.Invoices;

namespace FSH.Starter.WebApi.Zatca.Application.Features.ValidateInvoice.v1;
public class ValidateInvoiceValidator : AbstractValidator<StandardInvoice>
{
    public ValidateInvoiceValidator(ZatcaDbContext context)
    {
        RuleFor(p => p.Buyer).NotEmpty();
        RuleFor(p => p.Seller).NotEmpty();
    }
}
