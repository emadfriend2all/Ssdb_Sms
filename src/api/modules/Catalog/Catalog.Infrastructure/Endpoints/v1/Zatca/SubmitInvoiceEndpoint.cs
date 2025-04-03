﻿using FSH.Framework.Infrastructure.Auth.Policy;
using FSH.Starter.WebApi.Catalog.Application.Features.Zatca.SubmitInvoice.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1.Zatca;
public static class SubmitInvoiceEndpoint
{
    internal static RouteHandlerBuilder MapSubmitInvoiceEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPost("/submitInvoice", async (HttpContext context, SubmitInvoiceCommand input, ISender mediator) =>
            {
                if (context.Request.Headers.TryGetValue("Accept-Language", out var acceptLanguage))
                {
                    input.Lang = acceptLanguage.ToString();
                }

                var response = await mediator.Send(input);
                return Results.Ok(response);
            })
            .WithName(nameof(SubmitInvoiceEndpoint))
            .WithSummary("Submit invoice")
            .WithDescription("This will clear the B2B eInvoice, hash it and get QR code")
            .Produces(StatusCodes.Status204NoContent)
            .RequirePermission("Permissions.Zatca.Create")
            .MapToApiVersion(1);
    }
}
