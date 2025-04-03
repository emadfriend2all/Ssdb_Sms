using System.Globalization;
using FSH.Starter.Blazor.Client;
using FSH.Starter.Blazor.Infrastructure;
using FSH.Starter.Blazor.Infrastructure.Preferences;
using FSH.Starter.Shared.Constants;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddClientServices(builder.Configuration);

var host = builder.Build();

var storageService = host.Services.GetRequiredService<IClientPreferenceManager>();
if (storageService != null)
{
    CultureInfo culture = await storageService.GetPreference() is ClientPreference preference
        ? new CultureInfo(preference.LanguageCode)
        : new CultureInfo(LocalizationConstants.SupportedLanguages.FirstOrDefault()?.Code ?? "ar-SA");

    CultureInfo.DefaultThreadCurrentCulture = culture;
    CultureInfo.DefaultThreadCurrentUICulture = culture;
}

await builder.Build().RunAsync();
