using FluentValidation;

namespace FSH.Framework.Core.Tenant.Features.Charge;
public class ChargeValidator : AbstractValidator<ChargeCommand>
{
    public ChargeValidator()
    {
        RuleFor(t => t.Tenant).NotEmpty();
        RuleFor(t => t.Amount).GreaterThan(0);
    }
}
