using Asp.Versioning;
using FSH.Framework.Infrastructure.Auth.Api;
using FSH.Framework.Infrastructure.Auth.Policy;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Todo.Features.Create.v1;
public static class SendSmsEndpoint
{
    internal static RouteHandlerBuilder MapSendSmsEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapPost("/SendSms", async (SendSmsCommand request, ISender mediator) =>
                {
                    var response = await mediator.Send(request);
                    return response;
                })
                .WithName(nameof(SendSmsEndpoint))
                .WithSummary("Send Sms")
                .WithDescription("This is the send sms that is used as the main sms in SSDB")
                .Produces<SendSmsResponse>(StatusCodes.Status201Created)
                .AllowAnonymous()
                .MapToApiVersion(new ApiVersion(1, 0));

    }
}
