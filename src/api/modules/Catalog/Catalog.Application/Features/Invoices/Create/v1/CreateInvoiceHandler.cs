using FSH.Framework.Core.Identity.Users.Abstractions;
using FSH.Framework.Core.Persistence;
using FSH.Framework.Core.Tenant.Abstractions;
using FSH.Starter.WebApi.Catalog.Application.Features.Zatca.ClearInvoice.v1;
using FSH.Starter.WebApi.Catalog.Application.Features.Zatca.SubmitInvoice.v1;
using FSH.Starter.WebApi.Catalog.Domain;
using FSH.Starter.WebApi.Catalog.Domain.Exceptions;
using Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Invoices.Create.v1;
public sealed class CreateInvoiceHandler(
    ILogger<CreateInvoiceHandler> logger, IMediator mediator,ITenantService tenantService,ICurrentUser currentUser,
    [FromKeyedServices("catalog:invoices")] IRepository<Invoice> repository
    )
    : IRequestHandler<CreateInvoiceCommand, CreateInvoiceResponse>
{
    public async Task<CreateInvoiceResponse> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        var tenantId = currentUser.GetTenant();
        if(tenantId is not null)
        { 
            var tenant = await tenantService.GetBasicTenantInfoAsync(tenantId); 
            if (tenant.Balance <= 0)
            {
                throw new AppBadRequestException("Insuffeciant balance, please refill.");
            }   
        }

        ArgumentNullException.ThrowIfNull(request);
        var invoice = request.Adapt<Invoice>();

        var response = await mediator.Send(request.Adapt<SubmitInvoiceCommand>(), cancellationToken);
        if (response.IsSuccessful)
        {
            invoice.IsValid = true;
            invoice.QrCode = response.QrCode;
            invoice.SubmitedInvoice = response.SubmitedInvoice;
            invoice.SingedXML = response.SingedXML;
            invoice.SubmitStatus = response.Status;
            invoice.IsCleared = response.Status == "CLEARED";
            invoice.IsReported = response.Status == "REPORTED";
            if (response?.ValidationResults?.WarningMessages?.Select(x => x.Message).ToList().Count > 0)
            {
                invoice.Warnings = string.Join("", response.ValidationResults.WarningMessages.Select(x => x.Message).ToList());
            }
            await repository.AddAsync(invoice, cancellationToken);

            await tenantService.Charge(tenantId!, 1);

            logger.LogInformation("Invoice created {InvoiceId}", invoice.Id);
            return response.Adapt<CreateInvoiceResponse>();
        }

        
        
        return response.Adapt<CreateInvoiceResponse>();
    }
}
