using FluentValidation;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Customers.Create.v1;
public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(p => p.Name).NotEmpty();
        RuleFor(p => p.TaxNumber).NotEmpty();
    }
}
