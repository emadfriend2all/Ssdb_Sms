namespace FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Invoices;

public class StandardInvoiceAdjustment : StandardInvoice
{
    public string? AdjustmentReason { get; set; }
    public string? AdjsutmentInvoiceNumber { get; set; }
}
