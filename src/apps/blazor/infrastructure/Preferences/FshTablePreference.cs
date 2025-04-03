using FSH.Starter.Blazor.Shared.Notifications;

namespace FSH.Starter.Blazor.Infrastructure.Preferences;

public class FshTablePreference : INotificationMessage
{
    public bool IsDense { get; set; } = true;
    public bool IsStriped { get; set; } = true;
    public bool HasBorder { get; set; }
    public bool IsHoverable { get; set; }
}
