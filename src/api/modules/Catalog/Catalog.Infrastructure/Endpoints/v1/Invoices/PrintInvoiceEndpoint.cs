using FSH.Framework.Infrastructure.Auth.Policy;
using FSH.Starter.WebApi.Catalog.Application.Features.Invoices.Get.v1;
using FSH.Starter.WebApi.Catalog.Application.Features.Invoices.Print.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1.Invoices;
public static class PrintInvoiceEndpoint
{
    internal static RouteHandlerBuilder MapPrintInvoiceEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet("PrintInvoice/{id:int}", async (int id, ISender mediator) =>
            {
                var response = await mediator.Send(new PrintInvoiceRequest(id));

                if (response?.File == null || response.File.Length == 0)
                {
                    return Results.NotFound("Invoice not found or empty.");
                }

                return Results.File(response.File, "application/pdf", $"Invoice_{id}.pdf");
            })
            .WithName(nameof(PrintInvoiceEndpoint))
            .WithSummary("Download invoice PDF by ID")
            .WithDescription("Returns the invoice PDF as a downloadable file.")
            .Produces<FileContentResult>(StatusCodes.Status200OK, "application/pdf")
            .Produces(StatusCodes.Status404NotFound)
            .RequirePermission("Permissions.Invoices.View")
            .MapToApiVersion(1);
    }
}
