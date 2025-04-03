using FluentValidation;
using FSH.Starter.WebApi.Zatca.Persistence;
using Shared.Contracts.Zatca.Invoices;

namespace FSH.Starter.WebApi.Zatca.Application.Features.ReportInvoice.v1;
public class ReportInvoiceValidator : AbstractValidator<StandardInvoice>
{
    public ReportInvoiceValidator(ZatcaDbContext context)
    {
        RuleFor(p => p.Buyer).NotEmpty();
        RuleFor(p => p.Seller).NotEmpty();
    }
}
