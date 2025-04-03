using System.Text.Json.Serialization;
namespace FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Shared;

public class ErrorModel
{
    /// <summary>
    /// Gets or sets the category of the error.
    /// </summary>
    [JsonPropertyName("category")]
    public string? Category { get; set; }

    /// <summary>
    /// Gets or sets the code of the error.
    /// </summary>
    [JsonPropertyName("code")]
    public string? Code { get; set; }

    /// <summary>
    /// Gets or sets the message describing the error.
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; }
}
