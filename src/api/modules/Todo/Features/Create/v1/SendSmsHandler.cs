using FSH.Framework.Core.Persistence;
using FSH.Starter.WebApi.Todo.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FSH.Starter.WebApi.Todo.Features.Create.v1;
public sealed class SendSmsHandler(
    ILogger<SendSmsHandler> logger,
    [FromKeyedServices("todo")] IRepository<TodoItem> repository)
    : IRequestHandler<SendSmsCommand, SendSmsResponse>
{
    public async Task<SendSmsResponse> Handle(SendSmsCommand request, CancellationToken cancellationToken)
    {
        var barqRequest = new BarqSendSmsCommand(request.phoneNumber, "Edikhark", "plain", request.message, null, null);
        var barqResponse = await ApiHelper.PostAsync<BarqSendSmsCommand, BarqSendSmsResponse>("/sms/send", barqRequest);

        #region save
        //ArgumentNullException.ThrowIfNull(request);
        //var item = TodoItem.Create(request.phoneNumber, request.message);
        //await repository.AddAsync(item, cancellationToken).ConfigureAwait(false);
        //await repository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        //logger.LogInformation("todo item created {TodoItemId}", item.Id); 
        #endregion
        return new SendSmsResponse(barqResponse.Status,barqResponse.Message);
    }
}

// Send SMS Request
public record BarqSendSmsCommand(string recipient,string sender_id,string type, string message, DateTime? schedule_time, string? dlt_template_id);

// Send SMS Response
public record BarqSendSmsResponse(
    string Status,
    string Message,
    SmsData Data
);

public record SmsData(
    string Uid,
    string To,
    string From,
    string Message,
    string Status,
    string Cost,
    int SmsCount
);
