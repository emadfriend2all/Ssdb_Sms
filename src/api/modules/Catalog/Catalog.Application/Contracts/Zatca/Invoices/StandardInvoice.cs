using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Invoices;
public class StandardInvoice
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
    public int InvoiceSellerId { get; set; } = default!;
    public int InvoiceBuyerId { get; set; } = default!;
    public SellerInfo InvoiceSeller { get; set; } = default!;
    public BuyerInfo InvoiceBuyer { get; set; } = default!;
    public List<Allowance> Allowances { get; set; } = [];
    public List<InvoiceLineItem> InvoiceLines { get; set; } = default!;
    public LegalMonetaryTotal LegalMonetaryTotal { get; set; } = default!;
    public TaxTotal TaxTotal { get; set; } = default!;

    public void CalculateLegalMonetaryTotal()
    {
        decimal lineExtensionAmount = 0;
        decimal taxExclusiveAmount = 0;
        decimal taxInclusiveAmount = 0;
        decimal allowanceTotalAmount = 0;
        decimal prepaidAmount = 0; // If applicable, can be set based on specific logic
        decimal payableAmount = 0;

        // Initialize tax values
        decimal totalTaxableAmount = 0;
        decimal totalTaxAmount = 0;
        List<TaxSubtotal> taxSubtotals = new List<TaxSubtotal>();

        // Calculate LineExtensionAmount, TaxExclusiveAmount, and TaxInclusiveAmount for InvoiceLines
        foreach (var invoiceLine in InvoiceLines)
        {
            lineExtensionAmount += invoiceLine.Quantity * invoiceLine.ItemPrice;
            taxExclusiveAmount += invoiceLine.NetAmount;
            taxInclusiveAmount += invoiceLine.AmountInclusiveVAT;

            // Handle allowances in each InvoiceLine
            foreach (var allowance in invoiceLine.Allowances)
            {
                allowanceTotalAmount += allowance.Amount;
            }

            // Handle tax totals for each InvoiceLine
            var taxableAmount = invoiceLine.NetAmount; // This could vary based on how you calculate taxable amount
            var taxAmount = taxableAmount * invoiceLine.TaxCategoryPercent / 100;

            // Add to TaxSubtotals
            taxSubtotals.Add(new TaxSubtotal
            {
                TaxableAmount = taxableAmount,
                TaxAmount = taxAmount ?? default,
                Category = new TaxCategory { Id = invoiceLine.TaxCategoryId }
            });
        }

        // Calculate allowanceTotalAmount for allowances not tied to any InvoiceLine
        foreach (var allowance in Allowances)
        {
            allowanceTotalAmount += allowance.Amount;
        }

        // Payable amount after applying allowances (you can define additional logic here)
        payableAmount = taxInclusiveAmount - allowanceTotalAmount - prepaidAmount;

        // Assign the calculated values to LegalMonetaryTotal
        LegalMonetaryTotal.LineExtensionAmount = lineExtensionAmount;
        LegalMonetaryTotal.TaxExclusiveAmount = taxExclusiveAmount;
        LegalMonetaryTotal.TaxInclusiveAmount = taxInclusiveAmount;
        LegalMonetaryTotal.AllowanceTotalAmount = allowanceTotalAmount;
        LegalMonetaryTotal.PrepaidAmount = prepaidAmount;
        LegalMonetaryTotal.PayableAmount = payableAmount;

        // Assign the calculated tax total values to TaxTotal
        TaxTotal.RoundingAmount = totalTaxableAmount + lineExtensionAmount;
        TaxTotal.TaxAmount = totalTaxAmount;
        TaxTotal.TaxSubtotals = taxSubtotals;
    }
}
