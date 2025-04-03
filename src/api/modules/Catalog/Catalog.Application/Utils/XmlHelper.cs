using System.Xml;
using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Invoices;

namespace FSH.Starter.WebApi.Catalog.Application.Utils;

public static class XmlHelper
{
    public static void SetNodeValue(this XmlDocument xmlDocument, string xPath, string? value)
    {
        var xmlNode = xmlDocument.SelectSingleNode(xPath);
        if (xmlNode == null)
            return;
        if (value != null) xmlNode.InnerText = value;
    }

    public static void SetNodeValue(this XmlNode node, string absoluteXpath, string? value)
    {
        string relativeXpath = node.GetRelativeXpath(absoluteXpath);

        var xmlNode = node.SelectSingleNode(relativeXpath, GetXmlNamespaceManager(node));
        if (xmlNode == null) return;

        if (value == null) xmlNode.RemoveNode(absoluteXpath);
        if (value != null) xmlNode.InnerText = value;
    }

    public static void RemoveNode(this XmlDocument xmlDocument, string xPath)
    {
        var xmlNode = xmlDocument.SelectSingleNode(xPath);
        xmlNode?.ParentNode?.RemoveChild(xmlNode);
    }

    public static void RemoveNode(this XmlNode xmlNode, string xPath)
    {
        string relativeXpath = xmlNode.GetRelativeXpath(xPath); // TODO: maks sure this never retuns null or empty

        if (string.IsNullOrEmpty(relativeXpath)) return; 

        var nodeToRemove = xmlNode.SelectSingleNode(relativeXpath);
        nodeToRemove?.ParentNode?.RemoveChild(nodeToRemove);
    }

    public static void SetAttributeValue(this XmlDocument xmlDocument, string xPath, string attributeName,
        string? value)
    {
        var xmlNode = xmlDocument.SelectSingleNode(xPath);
        if (xmlNode?.Attributes == null) return;
        foreach (XmlAttribute attribute in xmlNode.Attributes)
        {
            if (attribute.Name == attributeName)
                attribute.Value = value;
        }
    }

    public static void SetCurrencyNodeValue(this XmlDocument xmlDocument, string xPath, string? value, string? attributeValue)
    {
        const string currencyIdAttributeName = "currencyID";

        var xmlNode = xmlDocument.SelectSingleNode(xPath);
        if (xmlNode?.Attributes == null) return;
        foreach (XmlAttribute attribute in xmlNode.Attributes)
        {
            if (attribute.Name == currencyIdAttributeName)
                attribute.Value = attributeValue;
        }

        if (value != null) xmlNode.InnerText = value;
    }

    public static void SetCurrencyNodeValue(this XmlDocument xmlDocument, string xPath, Money? money)
    {
        const string currencyIdAttributeName = "currencyID";

        var xmlNode = xmlDocument.SelectSingleNode(xPath);
        if (xmlNode?.Attributes == null) return;
        if (money is null)
        {
            xmlNode.RemoveNode(xPath);
            return;
        }

        foreach (XmlAttribute attribute in xmlNode.Attributes)
        {
            if (attribute.Name == currencyIdAttributeName)
                attribute.Value = money.CurrencyCode;
        }

        xmlNode.InnerText = money.AmountString;
    }

    public static void SetCurrencyNodeValue(this XmlNode node, string absoluteXpath, Money money)
    {
        string relativeXpath = node.GetRelativeXpath(absoluteXpath);

        var xmlNode = node.SelectSingleNode(relativeXpath, GetXmlNamespaceManager(node));
        if (xmlNode?.Attributes == null) return;
        if (money == null) xmlNode.RemoveNode(absoluteXpath);

        const string currencyIdAttributeName = "currencyID";
        foreach (XmlAttribute attribute in xmlNode.Attributes)
        {
            if (attribute.Name == currencyIdAttributeName)
                attribute.Value = money?.CurrencyCode;
        }

        xmlNode.InnerText = money?.AmountString ?? "0";
    }

    public static string? GetNodeValue(this XmlDocument xmlDocument, string xPath)
    {
        var xmlNode = xmlDocument.SelectSingleNode(xPath);
        return xmlNode?.InnerText;
    }

    private static string GetRelativeXpath(this XmlNode node, string absoluteXpath)
    {
        string pattern = $"/*[local-name() = '{node.LocalName}']";
        int index = absoluteXpath.IndexOf(pattern, StringComparison.OrdinalIgnoreCase);

        return index != -1 ? absoluteXpath.Substring(index + pattern.Length) : "./";
    }

    private static XmlNamespaceManager GetXmlNamespaceManager(XmlNode node)
    {
        var nsmgr = new XmlNamespaceManager(node.OwnerDocument!.NameTable);
        nsmgr.AddNamespace("ns", node.OwnerDocument.DocumentElement!.NamespaceURI);
        return nsmgr;
    }

}
