using FSH.Starter.WebApi.Catalog.Domain;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Invoices.Get.v1;
public class GetInvoiceResponse
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
    public string? QrCode { get; init; }
    public int InvoiceSellerId { get; set; } = default!;
    public int InvoiceBuyerId { get; set; } = default!;
    public GetInvoiceSeller? InvoiceSeller { get; init; }
    public GetInvoiceBuyer? InvoiceBuyer { get; init; }
    public List<GetInvoiceAllowance>? Allowances { get; init; }
    public List<GetInvoiceInvoiceLine>? InvoiceLines { get; init; }
    public GetInvoiceLegalMonetaryTotal? LegalMonetaryTotal { get; init; }
}

public class GetInvoiceSeller
{
    public int Id { get; set; }
    public string? Identifier { get; init; }
    public string? IdentifierType { get; init; }
    public string? Name { get; init; }
    public Address? Address { get; init; }
}

public class GetInvoiceBuyer
{
    public int Id { get; set; }
    public string? Name { get; init; }
    public string? TaxNumber { get; init; }
    public Address? Address { get; init; }
}

public class GetInvoiceAddress
{
    public string? Street { get; init; }
    public string? BuildingNumber { get; init; }
    public string? PlotIdentification { get; init; }
    public string? Suburb { get; init; }
    public string? City { get; init; }
    public string? PostalCode { get; init; }
    public string? CountrySubentity { get; init; }
    public string? CountryCode { get; init; }
}

public class GetInvoiceAllowance
{
    public bool ChargeIndicator { get; init; }
    public string? AllowanceChargeReason { get; init; }
    public decimal? Amount { get; init; }
    public GetInvoiceTaxCategory? TaxCategory { get; init; }
}

public class GetInvoiceInvoiceLine
{
    public string? Id { get; init; }
    public decimal? Quantity { get; init; }
    public decimal? LineExtensionAmount { get; init; }
    public decimal? VatAmount { get; init; }
    public decimal? NetAmount { get; init; }
    public decimal? AmountInclusiveVAT { get; init; }
    public string? TaxCategoryId { get; set; }
    public decimal TaxCategoryPercent { get; set; } = 0;
    public GetInvoiceTaxCategory? TaxCategory { get; init; }
    public List<GetInvoiceAllowance>? Allowances { get; init; }
    public string? ItemName { get; init; }
    public decimal? Price { get; init; }
}

public class GetInvoiceLegalMonetaryTotal
{
    public decimal? LineExtensionAmount { get; init; }
    public decimal? TaxExclusiveAmount { get; init; }
    public decimal? TaxInclusiveAmount { get; init; }
    public decimal? AllowanceTotalAmount { get; init; }
    public decimal? PrepaidAmount { get; init; }
    public decimal? PayableAmount { get; init; }
}


public class GetInvoiceTaxTotal
{
    public decimal? TaxAmount { get; init; }
    public decimal? TaxableAmount { get; init; }
    public GetInvoiceTaxCategory? TaxCategory { get; init; }
    public List<GetInvoiceTaxSubtotal>? TaxSubtotals { get; init; }
}

public class GetInvoiceTaxSubtotal
{
    public decimal? TaxableAmount { get; init; }
    public decimal? TaxAmount { get; init; }
    public GetInvoiceTaxCategory? TaxCategory { get; init; }
}

public class GetInvoiceTaxCategory
{
    public string? CategoryCode { get; init; }
    public decimal? Percent { get; init; }
    public string? TaxExemptionReason { get; init; }
    public string? TaxExemptionReasonCode { get; init; }
    public string? TaxSchemeId { get; set; }
    public string? TaxSchemeName { get; set; }
}
