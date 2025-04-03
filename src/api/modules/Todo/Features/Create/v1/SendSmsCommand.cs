using System.ComponentModel;
using MediatR;

namespace FSH.Starter.WebApi.Todo.Features.Create.v1;
public record SendSmsCommand(
    [property: DefaultValue("249912962753")] string phoneNumber,
    [property: DefaultValue("Here is your BBan 12345.")] string message) : IRequest<SendSmsResponse>;



