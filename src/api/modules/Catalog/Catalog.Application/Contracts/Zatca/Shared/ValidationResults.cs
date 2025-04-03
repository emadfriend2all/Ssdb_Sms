namespace FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Shared;

public class ValidationResults
{
    public IReadOnlyList<InfoMessage>? InfoMessages { get; set; }
    public IReadOnlyList<WarningMessage>? WarningMessages { get; set; }
    public IReadOnlyList<ErrorMessage>? ErrorMessages { get; set; }
    public string? Status { get; set; }
}
