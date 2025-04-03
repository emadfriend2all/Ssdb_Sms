using System.Xml.Serialization;

namespace Shared.Contracts.Zatca.Invoices;
public class InvoiceLineItem
{
    public string Id { get; set; }
    public decimal Quantity { get; set; }
    public decimal LineExtensionAmount { get; set; }
    public decimal VatAmount { get; set; }
    public decimal NetAmount { get; set; }
    public decimal AmountInclusiveVAT { get; init; }
    public TaxCategory TaxCategory { get; set; }
    public List<Allowance> Allowances { get; set; }
    public string ItemName { get; set; }

    //rate of item
    public decimal Price { get; set; }

}


public class Allowance
{
    public bool ChargeIndicator { get; set; }
    public string AllowanceChargeReason { get; set; }
    public decimal Amount { get; set; } = 0;
    public TaxCategory TaxCategory { get; set; }
}

