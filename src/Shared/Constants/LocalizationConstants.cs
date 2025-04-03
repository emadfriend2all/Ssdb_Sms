namespace FSH.Starter.Shared.Constants;

public static class LocalizationConstants
{
    public static readonly LanguageCode[] SupportedLanguages = [
            new() {
                Code = "en-US",
                DisplayName= "English"
            },
            new() {
                Code = "ar-SA",
                DisplayName = "العربية",
                IsRTL = true
            }
        ];
}
