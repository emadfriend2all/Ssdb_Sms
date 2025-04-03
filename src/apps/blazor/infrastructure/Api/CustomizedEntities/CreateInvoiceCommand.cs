namespace FSH.Starter.Blazor.Infrastructure.Api.CustomizedEntities;
public static class InvoiceExensions
{
    public static CreateInvoiceCommand Calculate(this CreateInvoiceCommand invoice)
    {
        double lineExtensionAmount = 0;
        double taxExclusiveAmount = 0;
        double taxInclusiveAmount = 0;
        double allowanceTotalAmount = 0;
        double prepaidAmount = 0;
        double payableAmount = 0;

        double totalRoundingAmount = 0;
        double totalTaxAmount = 0;
        List<TaxSubtotal> taxSubtotals = new List<TaxSubtotal>();


        foreach (var invoiceLine in invoice?.InvoiceLines ?? [])
        {
            CalculateLine(invoiceLine);
            double vatRate = invoiceLine.TaxCategoryPercent.GetValueOrDefault(0) / 100;

            double vatAmount = Math.Round(invoiceLine.NetAmount * vatRate, 2);

            double calculatedNetAmount = invoiceLine.LineExtensionAmount - (invoiceLine.Allowances?.Sum(a => a.Amount) ?? 0);

            if (Math.Abs(calculatedNetAmount - invoiceLine.NetAmount) > 0.01)
            {
                throw new Exception("Mismatch in calculated NetAmount and expected NetAmount.");
            }

            lineExtensionAmount += invoiceLine.LineExtensionAmount;
            taxExclusiveAmount += invoiceLine.NetAmount;
            taxInclusiveAmount += invoiceLine.NetAmount + vatAmount;

            totalRoundingAmount += invoiceLine.LineExtensionAmount + invoiceLine.VatAmount;
            totalTaxAmount += vatAmount;

            taxSubtotals.Add(new TaxSubtotal
            {
                TaxableAmount = invoiceLine.NetAmount,
                TaxAmount = vatAmount,
                TaxCategoryId = invoiceLine.TaxCategoryId,
                TaxCategoryPercent = invoiceLine.TaxCategoryPercent.GetValueOrDefault(0),
                Category = invoiceLine.LineTaxCategory
            });
        }

        // Process document-level allowances
        foreach (var allowance in invoice!.Allowances ?? [])
        {
            allowanceTotalAmount += allowance.Amount;
        }

        // Correcting the final payable amount
        payableAmount = taxInclusiveAmount - allowanceTotalAmount - prepaidAmount;

        // Assign calculated values to LegalMonetaryTotal
        invoice.LegalMonetaryTotal ??= new();
        invoice.LegalMonetaryTotal.LineExtensionAmount = lineExtensionAmount;
        invoice.LegalMonetaryTotal.TaxExclusiveAmount = taxExclusiveAmount;
        invoice.LegalMonetaryTotal.TaxInclusiveAmount = taxInclusiveAmount;
        invoice.LegalMonetaryTotal.AllowanceTotalAmount = allowanceTotalAmount;
        invoice.LegalMonetaryTotal.PrepaidAmount = prepaidAmount;
        invoice.LegalMonetaryTotal.PayableAmount = payableAmount;

        // Assign calculated tax values to TaxTotal
        invoice.TaxTotal ??= new();
        invoice.TaxTotal.RoundingAmount = totalRoundingAmount;
        invoice.TaxTotal.TaxAmount = totalTaxAmount;
        invoice.TaxTotal.TaxSubtotals = taxSubtotals;

        return invoice;
    }

    private static void CalculateLine(InvoiceLineItem line)
    {
        var vat = line.TaxCategoryPercent ?? 15;
        line.LineExtensionAmount = line.ItemPrice * line.Quantity;
        line.NetAmount = line.LineExtensionAmount - (line.Allowances?.Sum(a => a.Amount) ?? 0);
        line.VatAmount = line.NetAmount * (vat / 100);
        line.AmountInclusiveVAT = line.NetAmount + line.VatAmount;

    }
}
