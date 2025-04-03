using Carter;
using FSH.Framework.Core.Persistence;
using FSH.Framework.Infrastructure.Persistence;
using FSH.Starter.WebApi.Todo.Domain;
using FSH.Starter.WebApi.Todo.Features.Create.v1;
using FSH.Starter.WebApi.Todo.Features.Delete.v1;
using FSH.Starter.WebApi.Todo.Features.Get.v1;
using FSH.Starter.WebApi.Todo.Features.GetList.v1;
using FSH.Starter.WebApi.Todo.Features.Update.v1;
using FSH.Starter.WebApi.Todo.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace FSH.Starter.WebApi.Todo;
public static class TodoModule
{

    public class Endpoints : CarterModule
    {
        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            var smsGroup = app.MapGroup("SMS").WithTags("SMS");
            smsGroup.MapSendSmsEndpoint();
            smsGroup.MapGetTodoEndpoint();
            smsGroup.MapGetTodoListEndpoint();
            smsGroup.MapTodoItemUpdationEndpoint();
            smsGroup.MapTodoItemDeletionEndpoint();
        }
    }
    public static WebApplicationBuilder RegisterTodoServices(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.Services.BindDbContext<TodoDbContext>();
        builder.Services.AddScoped<IDbInitializer, TodoDbInitializer>();
        builder.Services.AddKeyedScoped<IRepository<TodoItem>, TodoRepository<TodoItem>>("todo");
        builder.Services.AddKeyedScoped<IReadRepository<TodoItem>, TodoRepository<TodoItem>>("todo");
        return builder;
    }
    public static WebApplication UseZatcaModule(this WebApplication app)
    {
        return app;
    }
}
