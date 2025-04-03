
using BrowserInterop.Geolocation;

namespace FSH.Starter.Blazor.Infrastructure.Services;
public interface IGeolocationService
{
    Task<bool> CheckGeolocationPermission();
    Task<GeolocationPosition?> GetCurrentLocation();
    Task InitAsync();
    Task<bool> RequestGeolocationPermission();
    Task StopWatch();
    Task<GeolocationPosition?> WatchCurrentLocation();
}
