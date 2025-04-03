using Newtonsoft.Json;

namespace Shared.Contracts.Zatca.Clearance;


public class ClearanceRequest
{
    /// <summary>
    /// // Specifies the clearance status, while "0" when clearance is disabled and "1" when clearance is enabled
    /// </summary>
    public int ClearanceStatus { get; set; }

    /// <summary>
    /// Specifies the langauge in which the response will be returned. Currently supported languages are English (en) and Arabic (ar) and it defaults to English.
    /// </summary>
    public string Lang { get; set; } = "ar";

    /// <summary>
    /// The the submitted document hash
    /// </summary>
    [JsonProperty("invoiceHash")]
    public string? InvoiceHash { get; set; }

    /// <summary>
    /// the base64 representation of the invoice
    /// </summary>
    [JsonProperty("invoice")]
    public string? Invoice { get; set; }

    /// <summary>
    /// Universal Unique Identifier (GUID)
    /// </summary>
    [JsonProperty("uuid")]
    public string? UUID { get; set; }

}
