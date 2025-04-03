using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Shared;
using FSH.Starter.WebApi.Catalog.Application.Features.Zatca.ClearInvoice.v1;
using FSH.Starter.WebApi.Catalog.Application.Features.Zatca.ReportInvoice.v1;
using FSH.Starter.WebApi.Catalog.Domain;

namespace FSH.Starter.WebApi.Catalog.Application.Utils;
public static class InvoiceHelper
{
    public static bool IsB2B(this Invoice invoice)
    {
        return invoice.InvoiceSeller is not null;
    }

    public static bool IsB2C(this Invoice invoice)
    {
        return invoice.InvoiceSeller is null;
    }

    public static Invoice Clear(this Invoice invoice, ClearInvoiceResponse response)
    {
        SetValidations(invoice, response.ValidationResults);

        invoice.IsValid = response.ValidationResults?.ErrorMessages?.Any() == false;
        invoice.IsCleared = response.Status == "CLEARED";
        return invoice;
    }

    public static Invoice Report(this Invoice invoice, ReportInvoiceResponse response)
    {
        SetValidations(invoice, response.ValidationResults);

        invoice.IsValid = response.ValidationResults?.ErrorMessages?.Any() == false;
        invoice.IsReported = response.Status == "REPORTED";
        return invoice;
    }
    
    public static string GetInvoiceTypeId (string invoiceType)
    {
        switch (invoiceType)
        {
            case "388":
                return "1100";
            case "383":
                return "0200";
            case "381":
                return "0300";
            default:
                return "Invalid invoice type";
        }
    }
    public static Invoice Calculate(this Invoice invoice)
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
        foreach (var invoiceLine in invoice.InvoiceLines)
        {
            lineExtensionAmount += invoiceLine.Quantity * invoiceLine.Item.Price;
            taxExclusiveAmount += invoiceLine.NetAmount;
            taxInclusiveAmount += invoiceLine.AmountInclusiveVAT;

            // Handle allowances in each InvoiceLine
            foreach (var allowance in invoiceLine.Allowances)
            {
                allowanceTotalAmount += allowance.Amount;
            }

            // Handle tax totals for each InvoiceLine
            var taxableAmount = invoiceLine.NetAmount; // This could vary based on how you calculate taxable amount
            var taxAmount = taxableAmount * invoiceLine.TaxCategory.Percent / 100;

            // Add to TaxSubtotals
            taxSubtotals.Add(new TaxSubtotal
            {
                TaxableAmount = taxableAmount,
                TaxAmount = taxAmount,
                TaxCategory = new TaxCategory { Id = invoiceLine.TaxCategory.Id }
            });
        }

        // Calculate allowanceTotalAmount for allowances not tied to any InvoiceLine
        foreach (var allowance in invoice.Allowances)
        {
            allowanceTotalAmount += allowance.Amount;
        }

        // Payable amount after applying allowances (you can define additional logic here)
        payableAmount = taxInclusiveAmount - allowanceTotalAmount - prepaidAmount;

        // Assign the calculated values to LegalMonetaryTotal
        invoice.LegalMonetaryTotal.LineExtensionAmount = lineExtensionAmount;
        invoice.LegalMonetaryTotal.TaxExclusiveAmount = taxExclusiveAmount;
        invoice.LegalMonetaryTotal.TaxInclusiveAmount = taxInclusiveAmount;
        invoice.LegalMonetaryTotal.AllowanceTotalAmount = allowanceTotalAmount;
        invoice.LegalMonetaryTotal.PrepaidAmount = prepaidAmount;
        invoice.LegalMonetaryTotal.PayableAmount = payableAmount;

        // Assign the calculated tax total values to TaxTotal
        invoice.TaxTotal ??= new();
        invoice.TaxTotal.RoundingAmount = totalTaxableAmount;
        invoice.TaxTotal.TaxAmount = totalTaxAmount;
        invoice.TaxTotal.TaxSubtotals = taxSubtotals;
    
        return invoice;
    }

    private static void SetValidations(Invoice invoice, ValidationResults? validationResult)
    {
        if (validationResult == null)
        {
            return;
        }

        if (validationResult.InfoMessages != null)
        {
            invoice.Info = string.Join("==>", validationResult.InfoMessages);
        }

        if (validationResult.WarningMessages != null)
        {
            invoice.Info = string.Join("==>", validationResult.WarningMessages);
        }

        if (validationResult.ErrorMessages != null)
        {
            invoice.Info = string.Join("==>", validationResult.ErrorMessages);
        }

        invoice.IsValid = validationResult.ErrorMessages?.Any() == false;
    }
}
