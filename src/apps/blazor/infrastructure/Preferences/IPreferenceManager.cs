namespace FSH.Starter.Blazor.Infrastructure.Preferences;

public interface IPreferenceManager
{
    Task SetPreference(IPreference preference);

    Task<IPreference> GetPreference();
}
