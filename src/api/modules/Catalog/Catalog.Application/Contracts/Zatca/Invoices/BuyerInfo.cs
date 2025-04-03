namespace FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Invoices;

public class BuyerInfo
{
    public string? Name { get; set; }
    public string? TaxNumber { get; set; }
    public PartyAddress? Address { get; set; }
}
