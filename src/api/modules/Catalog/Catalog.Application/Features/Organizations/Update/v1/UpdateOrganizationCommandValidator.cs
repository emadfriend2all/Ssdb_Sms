using FluentValidation;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Organizations.Update.v1;
public class UpdateOrganizationCommandValidator : AbstractValidator<UpdateOrganizationCommand>
{
    public UpdateOrganizationCommandValidator()
    {
        RuleFor(p => p.CommonName).NotEmpty();
        RuleFor(p => p.OrganizationName).NotEmpty();
    }
}
