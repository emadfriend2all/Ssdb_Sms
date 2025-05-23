﻿using System.Text.RegularExpressions;
using Showmatics.Blazor.Client.Infrastructure.Theme;
using MudBlazor;
using Showmatics.Shared.Multitenancy;

namespace Showmatics.Blazor.Client.Infrastructure.Preferences;

public class ClientPreferenceManager : IClientPreferenceManager
{
    private readonly ILocalStorageService _localStorageService;

    public ClientPreferenceManager(
        ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public async Task<bool> ToggleDarkModeAsync()
    {
        if (await GetPreference() is ClientPreference preference)
        {
            preference.IsDarkMode = !preference.IsDarkMode;
            await SetPreference(preference);
            return !preference.IsDarkMode;
        }

        return false;
    }

    public async Task<bool> ToggleDrawerAsync()
    {
        if (await GetPreference() is ClientPreference preference)
        {
            preference.IsDrawerOpen = !preference.IsDrawerOpen;
            await SetPreference(preference);
            return preference.IsDrawerOpen;
        }

        return false;
    }

    public async Task<bool> ToggleLayoutDirectionAsync()
    {
        if (await GetPreference() is ClientPreference preference)
        {
            preference.IsRTL = !preference.IsRTL;
            await SetPreference(preference);
            return preference.IsRTL;
        }

        return false;
    }

    public async Task<bool> ChangeLanguageAsync(string languageCode)
    {
        if (await GetPreference() is ClientPreference preference)
        {
            var language = Array.Find(LocalizationConstants.SupportedLanguages, a => a.Code == languageCode);
            if (language?.Code is not null)
            {
                preference.LanguageCode = language.Code;
                preference.IsRTL = language.IsRTL;
            }
            else
            {
                preference.LanguageCode = "ar-SA";
                preference.IsRTL = false;
            }

            await SetPreference(preference);
            return true;
        }

        return false;
    }

    public async Task<MudTheme> GetCurrentThemeAsync()
    {
        if (await GetPreference() is ClientPreference preference)
        {
            if (preference.IsDarkMode) return new DarkTheme();
        }

        return new LightTheme();
    }

    public async Task<string> GetPrimaryColorAsync()
    {
        if (await GetPreference() is ClientPreference preference)
        {
            string colorCode = preference.PrimaryColor;
            if (Regex.Match(colorCode, "^#(?:[0-9a-fA-F]{3,4}){1,2}$").Success)
            {
                return colorCode;
            }
            else
            {
                preference.PrimaryColor = CustomColors.Light.Primary;
                await SetPreference(preference);
                return preference.PrimaryColor;
            }
        }

        return CustomColors.Light.Primary;
    }

    public async Task SetTenantColorsAsync(TenantBasicInfoDto? tenantInfo)
    {
        if(tenantInfo is null) return;

        if (await GetPreference() is ClientPreference preference)
        {
            if (Regex.Match(tenantInfo.PrimaryColor ?? string.Empty, "^#(?:[0-9a-fA-F]{3,4}){1,2}$").Success &&
                Regex.Match(tenantInfo.SecondaryColor ?? string.Empty, "^#(?:[0-9a-fA-F]{3,4}){1,2}$").Success)
            {
                preference.PrimaryColor = tenantInfo.PrimaryColor!;
                preference.SecondaryColor = tenantInfo.SecondaryColor!;

            }
            else
            {
                preference.PrimaryColor = CustomColors.Light.Primary;
                preference.SecondaryColor = CustomColors.Light.Secondary;
            }

            preference.LogoUrl = tenantInfo.LogoUrl ?? "Files/showmatics-logo.png";
            await SetPreference(preference);
        }
    }

    public async Task<bool> IsRTL()
    {
        if (await GetPreference() is ClientPreference preference)
        {
            return preference.IsRTL;
        }

        return false;
    }

    public async Task<bool> IsDrawerOpen()
    {
        if (await GetPreference() is ClientPreference preference)
        {
            return preference.IsDrawerOpen;
        }

        return false;
    }

    public static string Preference = "clientPreference";

    public async Task<IPreference> GetPreference()
    {
        return await _localStorageService.GetItemAsync<ClientPreference>(Preference) ?? new ClientPreference();
    }

    public async Task SetPreference(IPreference preference)
    {
        await _localStorageService.SetItemAsync(Preference, preference as ClientPreference);
    }
}