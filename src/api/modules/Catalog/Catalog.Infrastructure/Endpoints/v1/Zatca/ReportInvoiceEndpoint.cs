using FSH.Framework.Infrastructure.Auth.Policy;
using FSH.Starter.WebApi.Catalog.Application.Features.Zatca.ReportInvoice.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1.Zatca;
public static class ReportInvoiceEndpoint
{
    internal static RouteHandlerBuilder MapReportInvoiceEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPost("/reportInvoice", async (ReportInvoiceCommand input, ISender mediator) =>
            {
                var response = await mediator.Send(input);
                return Results.Ok(response);
            })
            .WithName(nameof(ReportInvoiceEndpoint))
            .WithSummary("Report invoice")
            .WithDescription("This will report B2C eInvoice, hash it and get QR code")
            .Produces(StatusCodes.Status204NoContent)
            .RequirePermission("Permissions.Zatca.Create")
            .MapToApiVersion(1);
    }
}
