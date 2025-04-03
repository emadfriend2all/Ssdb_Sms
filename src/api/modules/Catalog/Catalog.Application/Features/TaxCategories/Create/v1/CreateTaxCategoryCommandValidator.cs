using FluentValidation;

namespace FSH.Starter.WebApi.Catalog.Application.Features.TaxCategories.Create.v1;
public class CreateTaxCategoryCommandValidator : AbstractValidator<CreateTaxCategoryCommand>
{
    public CreateTaxCategoryCommandValidator()
    {
        RuleFor(b => b.Id).NotEmpty();
        RuleFor(b => b.TaxSchemeId).NotEmpty();
    }
}
