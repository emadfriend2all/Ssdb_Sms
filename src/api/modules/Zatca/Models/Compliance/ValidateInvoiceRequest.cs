using Newtonsoft.Json;

namespace Shared.Contracts.Zatca.Compliance;

public class ValidateInvoiceRequest
{
    /// <summary>
    /// The locally generated invoice xml string
    /// </summary>
    [JsonProperty("invoice")]
    public string? Invoice { get; set; }

    [JsonProperty("invoiceHash")]
    public string? InvoiceHash { get; set; }

    [JsonProperty("uuid")]
    public string? UUID { get; set; }
}
