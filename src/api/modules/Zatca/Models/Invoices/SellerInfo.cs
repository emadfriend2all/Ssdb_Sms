namespace Shared.Contracts.Zatca.Invoices;

public class SellerInfo
{
    public string? Identifier { get; set; }
    public string? IdentifierType { get; set; }
    public string? Name { get; set; }
    public PartyAddress? Address { get; set; }
}
