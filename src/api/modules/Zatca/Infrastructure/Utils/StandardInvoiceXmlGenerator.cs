using System.Xml;
using Shared.Contracts.Zatca.Invoices;
namespace FSH.Starter.WebApi.Zatca.Infrastructure.Utils;

public static partial class StandardInvoiceXmlGenerator
{
    private const string StandardInvoiceXmlFormat = "StandardInvoiceTemplate.xml";
    private const string DebitNoteXmlFormat = "DebitNote.xml";
    private const string CreditNoteXmlFormat = "CreditNote.xml";
    private static string? _currentHash;
    private static string _currency = "SAR";

    public static GenerateXmlResult GenerateForDebitNote(StandardInvoiceAdjustment standardInvoiceAdjustment)
    {
        return GenerateForAdjustment(standardInvoiceAdjustment, DebitNoteXmlFormat);
    }

    public static GenerateXmlResult GenerateForCreditNote(StandardInvoiceAdjustment standardInvoiceAdjustment)
    {
        return GenerateForAdjustment(standardInvoiceAdjustment, CreditNoteXmlFormat);
    }

    private static GenerateXmlResult GenerateForAdjustment(StandardInvoiceAdjustment standardInvoiceAdjustment,
        string debitNoteXmlFormat)
    {
        var standardInvoice = (StandardInvoice)standardInvoiceAdjustment;
        _currency = standardInvoice.DocumentCurrencyCode;
        var xmlDoc = standardInvoice.ToZatcaXml(debitNoteXmlFormat);

        xmlDoc.SetNodeValue(InvoiceReferenceXpath, standardInvoiceAdjustment.AdjsutmentInvoiceNumber);
        xmlDoc.SetNodeValue(AdjustmentReasonXpath, standardInvoiceAdjustment.AdjustmentReason);

        xmlDoc.PopulateInvoiceHash();


        var xmlBytes = System.Text.Encoding.UTF8.GetBytes(xmlDoc.OuterXml);

        var result = new GenerateXmlResult
        {
            InvoiceHash = _currentHash ?? string.Empty,
            UUID = standardInvoice.Uuid ?? string.Empty,
            Invoice = Convert.ToBase64String(xmlBytes),
            XmlInvoice = xmlDoc
        };

        return result;
    }

    public static GenerateXmlResult Generate(StandardInvoice standardInvoice)
    {
        var xmlDoc = standardInvoice.ToZatcaXml(StandardInvoiceXmlFormat);
        xmlDoc.PopulateInvoiceHash();

        var xmlBytes = System.Text.Encoding.UTF8.GetBytes(xmlDoc.OuterXml);

        var result = new GenerateXmlResult
        {
            InvoiceHash = _currentHash ?? string.Empty,
            UUID = standardInvoice.Uuid ?? string.Empty,
            Invoice = Convert.ToBase64String(xmlBytes),
            XmlInvoice = xmlDoc
        };

        return result;
    }

    public static XmlDocument ToZatcaXml(this StandardInvoice standardInvoice, string docXmlFormat)
    {
        var xmlDoc = new XmlDocument
        {
            PreserveWhitespace = true
        };
        var xmlString = typeof(StandardInvoiceXmlGenerator).GetFileContent(docXmlFormat);


        xmlDoc.LoadXml(xmlString!);
        PopulateInvoiceBasicInfo(xmlDoc, standardInvoice);
        PopulateSellerInfo(xmlDoc, standardInvoice);
        PopulateBuyerInfo(xmlDoc, standardInvoice);
        PopulateAllowances(xmlDoc, standardInvoice);
        PopulateTaxTotals(xmlDoc, standardInvoice);
        PopulateInvoiceLineItems(xmlDoc, standardInvoice);
        PopulateTotals(xmlDoc, standardInvoice);

        return xmlDoc;
    }

    private static void PopulateAllowances(XmlDocument xmlDoc, StandardInvoice standardInvoice)
    {
        XmlNode invoiceAllowanceTemplate = xmlDoc.SelectSingleNode(AllowanceXpath)!;
        xmlDoc.RemoveNode(AllowanceXpath);

        if (standardInvoice.Allowances is not null)
        {
            foreach (var allowance in standardInvoice.Allowances)
            {
                PopulateAllowances(xmlDoc, invoiceAllowanceTemplate, allowance);
            }
        }
    }

    private static void PopulateAllowances(XmlNode node, XmlNode allowancetemplate, Allowance allowance)
    {
        XmlNode newAllowance = allowancetemplate.CloneNode(true);
        newAllowance.SetNodeValue(LineItemAllowanceChargeIndicatorXpath, allowance.ChargeIndicator.ToString().ToLower());
        newAllowance.SetNodeValue(LineItemAllowanceChargeReasonXpath, allowance.AllowanceChargeReason);
        newAllowance.SetCurrencyNodeValue(LineItemAllowanceChargeAmountXpath, allowance.Amount.ToMoney());
        newAllowance.SetNodeValue(LineItemAllowanceChargeTaxCategoreIdXpath, allowance.TaxCategory.CategoryCode);
        newAllowance.SetNodeValue(LineItemAllowanceChargeTaxCategorePercentXpath, allowance.TaxCategory.Percent.ToString());
        newAllowance.SetNodeValue(LineItemAllowanceChargeTaxCategoreChemeIdXpath, allowance.TaxCategory.TaxScheme.Code);

        node.AppendChild(node);
    }

    private static void PopulateInvoiceHash(this XmlDocument xmlDoc)
    {
        _currentHash = InvoiceHashHelper.GenerateEInvoiceHashing(xmlDoc.OuterXml);
        xmlDoc.SetNodeValue(HashXpath, _currentHash);
    }

    private static void PopulateTotals(XmlDocument xmlDoc, StandardInvoice standardInvoice)
    {
        xmlDoc.SetCurrencyNodeValue(LegalMonetaryTotalLineExtensionAmountXpath, standardInvoice?.LegalMonetaryTotal?.LineExtensionAmount.ToMoney());
        xmlDoc.SetCurrencyNodeValue(LegalMonetaryTotalTaxExclusiveAmountXpath, standardInvoice?.LegalMonetaryTotal?.TaxExclusiveAmount.ToMoney());
        xmlDoc.SetCurrencyNodeValue(LegalMonetaryTotalTaxInclusiveAmountXpath, standardInvoice?.LegalMonetaryTotal?.TaxInclusiveAmount.ToMoney());
        xmlDoc.SetCurrencyNodeValue(LegalMonetaryTotalAllowanceTotalAmountXpath, standardInvoice?.LegalMonetaryTotal?.AllowanceTotalAmount.ToMoney());
        xmlDoc.SetCurrencyNodeValue(LegalMonetaryTotalAllowanceTotalAmountXpath, standardInvoice?.LegalMonetaryTotal?.AllowanceTotalAmount.ToMoney());
        xmlDoc.SetCurrencyNodeValue(LegalMonetaryTotalPrepaidAmountXpath, standardInvoice?.LegalMonetaryTotal?.PrepaidAmount.ToMoney());
        xmlDoc.SetCurrencyNodeValue(LegalMonetaryTotalPayableAmountXpath, standardInvoice?.LegalMonetaryTotal?.PayableAmount.ToMoney());
    }

    public static void PopulateInvoiceLineItems(XmlDocument xmlDoc, StandardInvoice standardInvoice)
    {
        XmlNode invoiceLineTemplate = xmlDoc.SelectSingleNode(LineItemXpath)!;
        XmlNode LineItemAllowanceTemplate = xmlDoc.SelectSingleNode(LineItemAllowanceXpath)!;
        if (invoiceLineTemplate == null)
            throw new InvalidOperationException("No InvoiceLine element found in the XML.");

        XmlNode parentNode = invoiceLineTemplate.ParentNode!;
        xmlDoc.RemoveNode(LineItemXpath);

        foreach (var item in standardInvoice.InvoiceLines)
        {
            XmlNode newInvoiceLine = invoiceLineTemplate.CloneNode(true);

            newInvoiceLine.SetNodeValue(LineItemIdXpath, item.Id);
            newInvoiceLine.SetNodeValue(LineItemInvoicedQuantityXpath, item.Quantity.ToString());
            newInvoiceLine.SetCurrencyNodeValue(LineItemLineExtensionAmountXpath, item.LineExtensionAmount.ToMoney());
            newInvoiceLine.SetCurrencyNodeValue(LineItemTaxAmountXpath, item.VatAmount.ToMoney());
            newInvoiceLine.SetCurrencyNodeValue(LineItemRoundingAmountXpath, item.NetAmount.ToMoney());
            newInvoiceLine.SetNodeValue(LineItemTaxClassificationIdXpath, item.TaxCategory.TaxScheme.Code);
            newInvoiceLine.SetNodeValue(LineItemTaxSchemeXpath, item.TaxCategory.TaxScheme.Name);
            newInvoiceLine.SetNodeValue(LineItemTaxPercentXpath, item.TaxCategory.Percent.ToString());
            newInvoiceLine.SetNodeValue(LineItemNameXpath, item.ItemName);
            newInvoiceLine.SetCurrencyNodeValue(LineItemPriceAmountXpath, item.Price.ToMoney());

            //Allowance charge 
            if (item.Allowances is not null)
            {
                xmlDoc.RemoveNode(LineItemXpath);
                foreach (var allowance in item.Allowances)
                {
                    PopulateAllowances(newInvoiceLine, LineItemAllowanceTemplate, allowance);
                }
            }
            else
            {
                newInvoiceLine.RemoveNode(LineItemAllowanceXpath);
            }

            parentNode.AppendChild(newInvoiceLine);
        }
    }

    private static Money ToMoney(this decimal amount)
    {
        return new Money(_currency, amount);
    }
    
    private static void PopulateTaxTotals(XmlDocument xmlDoc, StandardInvoice standardInvoice)
    {
        // TODO: Need to handle tax exemption
        xmlDoc.SetCurrencyNodeValue(TaxTotalTaxAmountXpath, standardInvoice.TaxTotal.TaxAmount.ToMoney());
        xmlDoc.SetCurrencyNodeValue(TaxTotalTaxAmountSecondTagXpath, standardInvoice.TaxTotal.TaxAmount.ToMoney());

        XmlNode subTotalTemplate = xmlDoc.SelectSingleNode(TaxTotalSubTotalXpath)!;
        XmlNode parentNode = subTotalTemplate.ParentNode!;
        xmlDoc.RemoveNode(TaxTotalSubTotalXpath);

        foreach (var subTotal in standardInvoice.TaxTotal.TaxSubtotals)
        {
            XmlNode newSubTotal = subTotalTemplate.CloneNode(true);
            newSubTotal.SetCurrencyNodeValue(TaxTotalSubTotalTaxableAmountXpath, subTotal.TaxableAmount.ToMoney());
            newSubTotal.SetCurrencyNodeValue(TaxTotalSubTotalTaxAmountXpath, subTotal.TaxAmount.ToMoney());
            newSubTotal.SetNodeValue(TaxTotalSubTotalTaxCategoryIdTaxAmountXpath, subTotal.TaxCategory.CategoryCode);
            newSubTotal.SetNodeValue(TaxTotalSubTotalTaxPercentTaxAmountXpath, subTotal.TaxCategory.Percent.ToString());
            newSubTotal.SetNodeValue(TaxTotalSubTotalTaxSchemeIDXpath, subTotal.TaxCategory.TaxScheme.Name);

            if (subTotal.TaxCategory.CategoryCode == "E")
            {
                newSubTotal.SetNodeValue(TaxTotalSubTotalTaxExemptionReasonCodeXpath, subTotal.TaxCategory.TaxExemptionReasonCode);
                newSubTotal.SetNodeValue(TaxTotalSubTotalTaxExemptionReasonXpath, subTotal.TaxCategory.TaxExemptionReason);
            }
            else
            {
                newSubTotal.RemoveNode(TaxTotalSubTotalTaxExemptionReasonCodeXpath);
                newSubTotal.RemoveNode(TaxTotalSubTotalTaxExemptionReasonXpath);
            }

            parentNode.AppendChild(newSubTotal);
        }
    }

    private static void PopulateBuyerInfo(XmlDocument xmlDoc, StandardInvoice standardInvoice)
    {
        xmlDoc.SetNodeValue(BuyerStreetNameXpath, standardInvoice.Buyer.PartyPostalAddress?.Street);
        xmlDoc.SetNodeValue(BuyerBuildingNumberXpath, standardInvoice.Buyer.PartyPostalAddress?.BuildingNumber);
        xmlDoc.SetNodeValue(BuyerPlotIdentificationXpath, standardInvoice.Buyer.PartyPostalAddress?.PlotIdentification);
        xmlDoc.SetNodeValue(BuyerCitySubdivisionNameXpath, standardInvoice.Buyer.PartyPostalAddress?.Suburb);
        xmlDoc.SetNodeValue(BuyerCityNameXpath, standardInvoice.Buyer.PartyPostalAddress?.City);
        xmlDoc.SetNodeValue(BuyerPostalZoneXpath, standardInvoice.Buyer.PartyPostalAddress?.PostalCode);
        xmlDoc.SetNodeValue(BuyerCountrySubEntityXpath, standardInvoice.Buyer.PartyPostalAddress?.CountrySubentity);
        xmlDoc.SetNodeValue(BuyerCountryXpath, standardInvoice.Buyer.PartyPostalAddress?.CountryCode);
        xmlDoc.SetNodeValue(BuyerRegistrationNameXpath, standardInvoice.Buyer.Name);
        xmlDoc.SetNodeValue(BuyerNatXpath, standardInvoice.Buyer.TaxNumber);
        xmlDoc.SetNodeValue(BuyerTaxSchemeXpath, "VAT");
    }

    private static void PopulateSellerInfo(XmlDocument xmlDoc, StandardInvoice standardInvoice)
    {
        xmlDoc.SetNodeValue(SellerStreetNameXpath, standardInvoice.Seller.Address?.Street);
        xmlDoc.SetNodeValue(SellerBuildingNumberXpath, standardInvoice.Seller.Address?.BuildingNumber);
        xmlDoc.SetNodeValue(SellerPlotIdentificationXpath, standardInvoice.Seller.Address?.PlotIdentification);
        xmlDoc.SetNodeValue(SellerCitySubdivisionNameXpath, standardInvoice.Seller.Address?.Suburb);
        xmlDoc.SetNodeValue(SellerCityNameXpath, standardInvoice.Seller.Address?.City);
        xmlDoc.SetNodeValue(SellerPostalZoneXpath, standardInvoice.Seller.Address?.PostalCode);
        xmlDoc.SetNodeValue(SellerCountrySubEntityXpath, standardInvoice.Seller.Address?.CountrySubentity);
        xmlDoc.SetNodeValue(SellerCountryXpath, standardInvoice.Seller.Address?.CountryCode);
        xmlDoc.SetNodeValue(SellerRegistrationNameXpath, standardInvoice.Seller.Name);
        xmlDoc.SetNodeValue(SellerTaxSchemeXpath, "VAT");
        xmlDoc.SetNodeValue(SellerCrnXpath, standardInvoice.Seller.Identifier);
    }

    private static void PopulateInvoiceBasicInfo(XmlDocument xmlDoc, StandardInvoice standardInvoice)
    {
        xmlDoc.SetNodeValue(InvoiceIdXpath, standardInvoice.Number);
        xmlDoc.SetNodeValue(InvoiceUuidXpath, standardInvoice.Uuid);
        xmlDoc.SetNodeValue(InvoiceActualDeliveryDateXpath, standardInvoice.ActualDeliveryDate?.ToString("yyyy-MM-dd"));
        xmlDoc.SetNodeValue(InvoiceDocumentCurrencyCodeXpath, standardInvoice.DocumentCurrencyCode);
        xmlDoc.SetNodeValue(InvoiceTaxCurrencyCodeXpath, standardInvoice.TaxCurrencyCode);
        xmlDoc.SetNodeValue(InvoiceIssueDateXpath, standardInvoice.IssueDateTime.ToString("yyyy-MM-dd"));
        xmlDoc.SetNodeValue(InvoiceIssueTimeXpath, standardInvoice.IssueDateTime.ToString("HH:mm:ss"));
        xmlDoc.SetNodeValue(InvoiceLatestDeliveryDateXpath, standardInvoice.LatestDeliveryDate?.ToString("yyyy-MM-dd"));
        xmlDoc.SetNodeValue(InvoicePreviousInvoiceHashXpath, standardInvoice.PreviousInvoiceHash);
    }
}
