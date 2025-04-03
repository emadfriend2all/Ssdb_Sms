using FluentValidation;

namespace FSH.Starter.WebApi.Catalog.Application.Features.TaxCategories.Update.v1;
public class UpdateTaxCategoryCommandValidator : AbstractValidator<UpdateTaxCategoryCommand>
{
    public UpdateTaxCategoryCommandValidator()
    {
        RuleFor(b => b.Id).NotEmpty();
        RuleFor(b => b.TaxSchemeId).NotEmpty();
    }
}
