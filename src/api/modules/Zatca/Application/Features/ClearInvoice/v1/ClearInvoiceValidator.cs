using FluentValidation;
using FSH.Starter.WebApi.Zatca.Persistence;
using Shared.Contracts.Zatca.Invoices;

namespace FSH.Starter.WebApi.Zatca.Application.Features.ClearInvoice.v1;
public class ClearInvoiceValidator : AbstractValidator<StandardInvoice>
{
    public ClearInvoiceValidator(ZatcaDbContext context)
    {
        RuleFor(p => p.Buyer).NotEmpty();
        RuleFor(p => p.Seller).NotEmpty();
    }
}
