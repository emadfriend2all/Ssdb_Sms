using FluentValidation;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Organizations.Create.v1;
public class CreateOrganizationCommandValidator : AbstractValidator<CreateOrganizationCommand>
{
    public CreateOrganizationCommandValidator()
    {
        RuleFor(p => p.OrganizationName).NotEmpty();
        RuleFor(p => p.CommonName).NotEmpty();
    }
}
