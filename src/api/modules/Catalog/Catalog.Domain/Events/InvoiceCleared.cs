using FSH.Framework.Core.Domain.Events;

namespace FSH.Starter.WebApi.Catalog.Domain.Events;
public sealed record InvoiceCleared : DomainEvent
{
    public Invoice? Invoice { get; set; }
}
