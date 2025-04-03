using FSH.Framework.Core.Domain.Events;
using FSH.Starter.WebApi.Zatca.Application.Features.ClearInvoice.v1;
using Shared.Contracts.Zatca.Invoices;

namespace FSH.Starter.WebApi.Catalog.Domain.Events;
public sealed record InvoiceCleared : DomainEvent
{
    public StandardInvoice? Invoice { get; set; }
    public ClearInvoiceResponse? Response { get; set; }
}
