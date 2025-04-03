using Newtonsoft.Json;

namespace Shared.Contracts.Zatca.Reporting;


public class ReportingRequest
{
    /// <summary>
    /// The respnse language
    /// </summary>
    public string? Lang { get; set; }

    /// <summary>
    /// The submitted document hash
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
