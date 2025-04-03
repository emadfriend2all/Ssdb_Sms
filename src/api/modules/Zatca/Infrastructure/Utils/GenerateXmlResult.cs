using System.Xml;

namespace FSH.Starter.WebApi.Zatca.Infrastructure.Utils;

public class GenerateXmlResult
{
    public string InvoiceHash { get; set; }
    public string UUID { get; set; }
    public string Invoice { get; set; }
    public XmlDocument XmlInvoice { get; set; }
}
