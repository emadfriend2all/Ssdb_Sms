using FSH.Framework.Core.Domain.Contracts;
using FSH.Framework.Core.Domain;

namespace FSH.Starter.WebApi.Catalog.Domain;

public class Invoice : AuditableEntity<int>, IAggregateRoot
{
    public string Number { get; set; } = default!;
    public string InvoiceType { get; init; } = default!;
    public string? Uuid { get; set; }
    public DateTime IssueDateTime { get; set; }
    public DateTime? ActualDeliveryDate { get; set; }
    public DateTime? LatestDeliveryDate { get; set; }
    public string? InvoiceTypeCode { get; set; }
    public string DocumentCurrencyCode { get; set; } = default!;
    public string? TaxCurrencyCode { get; set; }
    public string? PreviousInvoiceHash { get; set; }
    public string? AdjustmentReason { get; set; }
    public string? AdjsutmentInvoiceNumber { get; set; }
    public string? SubmitStatus { get; set; }
    public bool? IsValid { get; set; }
    public bool? IsCleared { get; set; }
    public bool? IsReported { get; set; }
    public string? Warnings { get; set; }
    public string? Info { get; set; }
    public string? Errors { get; set; }
    public string? QrCode { get; set; }
    public string? SubmitedInvoice { get; set; }
    public string? SingedXML { get; set; }
    public int InvoiceSellerId { get; set; } = default!;
    public InvoiceSeller InvoiceSeller { get; set; } = default!;
    public int InvoiceBuyerId { get; set; } = default!;
    public InvoiceBuyer InvoiceBuyer { get; set; } = default!;
    public List<InvoiceAllowance>? Allowances { get; set; }
    public List<InvoiceLine> InvoiceLines { get; set; } = default!;
    public LegalMonetaryTotal LegalMonetaryTotal { get; set; } = default!;
    public TaxTotal? TaxTotal { get; set; } 
}
