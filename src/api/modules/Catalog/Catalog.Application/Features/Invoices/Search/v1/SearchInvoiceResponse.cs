
namespace FSH.Starter.WebApi.Catalog.Application.Features.Invoices.Get.v1;
public class SearchInvoiceResponse
{
    public int Id { get; set; }
    public string? InvoiceType { get; init; }
    public string? Number { get; init; }
    public string? Uuid { get; init; }
    public DateTimeOffset? IssueDateTime { get; init; }
    public DateTimeOffset? ActualDeliveryDate { get; init; }
    public DateTimeOffset? LatestDeliveryDate { get; init; }
    public string? InvoiceTypeCode { get; init; }
    public string? DocumentCurrencyCode { get; init; }
    public string? TaxCurrencyCode { get; init; }
    public string? PreviousInvoiceHash { get; init; }
    public int InvoiceSellerId { get; set; } = default!;
    public int InvoiceBuyerId { get; set; } = default!;
    public SearchInvoiceSeller? InvoiceSeller { get; init; }
    public SearchInvoiceBuyer? InvoiceBuyer { get; init; }
    public SearchInvoiceLegalMonetaryTotal? LegalMonetaryTotal { get; init; }
}

public class SearchInvoiceSeller
{
    public int Id { get; set; }
    public string? Identifier { get; init; }
    public string? IdentifierType { get; init; }
    public string? Name { get; init; }
}

public class SearchInvoiceBuyer
{
    public int Id { get; set; }
    public string? Name { get; init; }
    public string? TaxNumber { get; init; }
}

public class SearchInvoiceLegalMonetaryTotal
{
    public decimal? LineExtensionAmount { get; init; }
    public decimal? TaxExclusiveAmount { get; init; }
    public decimal? TaxInclusiveAmount { get; init; }
    public decimal? AllowanceTotalAmount { get; init; }
    public decimal? PrepaidAmount { get; init; }
    public decimal? PayableAmount { get; init; }
}


