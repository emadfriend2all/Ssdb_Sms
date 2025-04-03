using FluentValidation;

namespace FSH.Framework.Core.Tenant.Features.TopUp;
public class TopUpValidator : AbstractValidator<TopUpCommand>
{
    public TopUpValidator()
    {
        RuleFor(t => t.Tenant).NotEmpty();
        RuleFor(t => t.Balance).GreaterThan(0);
    }
}
