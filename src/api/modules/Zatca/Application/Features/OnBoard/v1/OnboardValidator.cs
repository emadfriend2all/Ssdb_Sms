using FluentValidation;
using FSH.Starter.WebApi.Zatca.Persistence;

namespace FSH.Starter.WebApi.Zatca.Application.Features.OnBoard.v1;
public class OnboardValidator : AbstractValidator<OnboardCommand>
{
    public OnboardValidator(ZatcaDbContext context)
    {
        RuleFor(p => p.CommonName).NotEmpty();
        RuleFor(p => p.SerialNumber).NotEmpty();
    }
}
