using Carter;
using FSH.Framework.Core.Persistence;
using FSH.Framework.Infrastructure.Persistence;
using FSH.Starter.WebApi.Zatca.Application.Features.OnBoard.v1;
using FSH.Starter.WebApi.Zatca.Application.Features.ClearInvoice.v1;
using FSH.Starter.WebApi.Zatca.Application.Interfaces;
using FSH.Starter.WebApi.Zatca.Infrastructure;
using FSH.Starter.WebApi.Zatca.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using FSH.Starter.WebApi.Zatca.Application.Features.ValidateInvoice.v1;
using FSH.Starter.WebApi.Zatca.Application.Features.ReportInvoice.v1;

namespace FSH.Starter.WebApi.Zatca;
public static class ZatcaModule
{

    public class Endpoints : CarterModule
    {
        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            //var zatcaGroup = app.MapGroup("zatca").WithTags("zatca");
            //zatcaGroup.MapOnboardEndpoint();
            //zatcaGroup.MapValidateInvoiceEndpoint();
            //zatcaGroup.MapClearInvoiceEndpoint();
            //zatcaGroup.MapReportInvoiceEndpoint();
        }
    }

    public static WebApplicationBuilder RegisterZatcaServices(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.Services.BindDbContext<ZatcaDbContext>();
        builder.Services.AddScoped<IDbInitializer, TodoDbInitializer>();
        builder.Services.AddScoped<IEgsService, EgsService>();
        builder.Services.AddScoped<IZatcaApiClient, ZatcaApiClient>();
        return builder;
    }

    public static WebApplication UseTodoModule(this WebApplication app)
    {
        return app;
    }
}
