using MudBlazor;

namespace FSH.Starter.Blazor.Client.Components.Common;

public class FSHTextField<T> : MudTextField<T>
{
    protected override void OnInitialized()
    {
        Margin = Margin.Dense;
        Variant = Variant.Outlined;
    }
}
