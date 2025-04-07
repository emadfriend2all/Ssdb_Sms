using FluentValidation;
using FSH.Starter.WebApi.Todo.Persistence;

namespace FSH.Starter.WebApi.Todo.Features.Create.v1;
public class SendSmsValidator : AbstractValidator<SendSmsCommand>
{
    public SendSmsValidator(TodoDbContext context)
    {
        RuleFor(p => p.PhoneNumber).NotEmpty().WithMessage("Sorry the Phone number is required to send the sms");
        RuleFor(p => p.Message).NotEmpty().Length(0,200).WithMessage("The message length is not correct the min is 0 and max is 200");
    }
}
