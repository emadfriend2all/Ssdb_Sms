namespace FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Invoices;

public class SellerInfo
{
    public string? Identifier { get; set; }
    public string? IdentifierType { get; set; }
    public string? Name { get; set; }
    public PartyAddress? Address { get; set; }
}
