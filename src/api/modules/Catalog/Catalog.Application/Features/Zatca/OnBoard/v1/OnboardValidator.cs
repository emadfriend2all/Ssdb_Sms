using FluentValidation;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Zatca.OnBoard.v1;
public class OnboardValidator : AbstractValidator<OnboardCommand>
{
    public OnboardValidator()
    {
        RuleFor(p => p.CommonName).NotEmpty();
        RuleFor(p => p.SerialNumber).NotEmpty();
    }
}
