using FluentValidation;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Customers.Update.v1;
public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(p => p.Name).NotEmpty();
        RuleFor(p => p.TaxNumber).NotEmpty();
    }
}
