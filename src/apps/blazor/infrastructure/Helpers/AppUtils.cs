using System.Globalization;

namespace FSH.Starter.Blazor.Infrastructure.Helpers;
public static class AppUtils
{
    public static string ToCurrency(this object amount, string? currencyCode)
    {
        if (amount is null) return "N/A";

        var culture = CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                                 .Where(c => new RegionInfo(c.Name).ISOCurrencySymbol == currencyCode)
                                 .Select(c => new { Culture = c, Region = new RegionInfo(c.Name) })
                                 .FirstOrDefault()?.Culture;

        culture ??= new CultureInfo("ar-SA");

        if (!decimal.TryParse(amount.ToString(), NumberStyles.Currency, culture, out decimal numericAmount))
            return "Invalid Amount";

        return string.Format(culture, "{0:C}", numericAmount);
    }

    public static string ToFormattedCurrency(this object? amount, string? currencyCode)
    {
        if (amount is null) return "N/A";

        var culture = CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                                 .Where(c => new RegionInfo(c.Name).ISOCurrencySymbol == currencyCode)
                                 .Select(c => new { Culture = c, Region = new RegionInfo(c.Name) })
                                 .FirstOrDefault()?.Culture;

        culture ??= new CultureInfo("ar-SA");

        if (!decimal.TryParse(amount.ToString(), NumberStyles.Currency, culture, out decimal numericAmount))
            return "Invalid Amount";

        string formattedAmount = string.Format(culture, "{0:C}", numericAmount);
        var region = new RegionInfo(culture.Name);
        string currencySymbol = region.CurrencySymbol;

        if (!formattedAmount.Contains(currencySymbol + " ", StringComparison.Ordinal))
        {
            formattedAmount = formattedAmount.Replace(region.CurrencySymbol, region.CurrencySymbol + " ", StringComparison.Ordinal);
        }

        return formattedAmount;
    }

}
