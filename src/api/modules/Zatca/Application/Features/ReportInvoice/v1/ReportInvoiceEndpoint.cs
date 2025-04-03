using Asp.Versioning;
using FSH.Framework.Infrastructure.Auth.Policy;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Zatca.Application.Features.ReportInvoice.v1;
public static class ReportInvoiceEndpoint
{
    internal static RouteHandlerBuilder MapReportInvoiceEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPost("/ReportInvoice", async (ReportInvoiceCommand input, ISender mediator) =>
            {
                var response = await mediator.Send(input);
                return Results.Ok(response);
            })
            .WithName(nameof(ReportInvoiceEndpoint))
            .WithSummary("Report invoice")
            .WithDescription("This will report B2C eInvoice, hash it and get QR code")
            .Produces(StatusCodes.Status204NoContent)
            .RequirePermission("Permissions.Products.View")
            .MapToApiVersion(1);
    }
}
