namespace Shared.Contracts.Zatca.Shared;

public class ValidationResults
{
    public IReadOnlyList<InfoMessage>? InfoMessages { get; set; }
    public IReadOnlyList<WarningMessage>? WarningMessages { get; set; }
    public IReadOnlyList<string>? ErrorMessages { get; set; }
    public string? Status { get; set; }
}
