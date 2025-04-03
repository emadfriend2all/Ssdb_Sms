namespace Shared.Contracts.Zatca.Invoices;
public class Money
{
    public Money(string currencyCode, decimal amount)
    {
        CurrencyCode = currencyCode;
        Amount = amount;
    }

    public Money()
    {

    }

    public string CurrencyCode { get; set; } = default!;
    public decimal Amount { get; set; } = default!;

    public string AmountString => $"{Amount:0.00}";
}
