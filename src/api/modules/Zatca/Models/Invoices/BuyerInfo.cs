namespace Shared.Contracts.Zatca.Invoices;

public class BuyerInfo
{
    public string? Name { get; set; }
    public string? TaxNumber { get; set; }
    public PartyAddress? PartyPostalAddress { get; set; }
}
