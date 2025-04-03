namespace FSH.Starter.WebApi.Zatca.Infrastructure.Utils;

public static partial class StandardInvoiceXmlGenerator
{
    public const string QrCodeXpath = "/*[local-name() = 'Invoice']/*[local-name() = 'AdditionalDocumentReference' and *[local-name()='ID' and .='QR']]/*[local-name() = 'Attachment']/*[local-name() = 'EmbeddedDocumentBinaryObject']";

    public const string InvoicePreviousInvoiceHashXpath = "/*[local-name() = 'Invoice']/*[local-name() = 'AdditionalDocumentReference' and *[local-name()='ID' and .='PIH']]/*[local-name() = 'Attachment']/*[local-name() = 'EmbeddedDocumentBinaryObject']";

    public const string HashXpath = "/*[local-name() = 'Invoice']/*[local-name() = 'UBLExtensions']/*[local-name() = 'UBLExtension']/*[local-name() = 'ExtensionContent']/*[local-name() = 'UBLDocumentSignatures']/*[local-name() = 'SignatureInformation']/*[local-name() = 'Signature']/*[local-name() = 'SignedInfo']/*[local-name() = 'Reference' and @Id='invoiceSignedData']/*[local-name() = 'DigestValue']";

    public const string InvoiceIdXpath = "/*[local-name() = 'Invoice']/*[local-name() = 'ID']";
    public const string InvoiceUuidXpath = "/*[local-name() = 'Invoice']/*[local-name() = 'UUID']";

    public const string InvoiceActualDeliveryDateXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'ActualDeliveryDate']";

    public const string InvoiceDocumentCurrencyCodeXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'DocumentCurrencyCode']";

    public const string InvoiceTaxCurrencyCodeXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'TaxCurrencyCode']";

    public const string InvoiceIssueDateXpath = "/*[local-name() = 'Invoice']/*[local-name() = 'IssueDate']";
    public const string InvoiceIssueTimeXpath = "/*[local-name() = 'Invoice']/*[local-name() = 'IssueTime']";

    public const string InvoiceLatestDeliveryDateXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'Delivery']/*[local-name() = 'LatestDeliveryDate']";

    public const string SellerStreetNameXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'AccountingSellerParty']/*[local-name() = 'Party']/*[local-name() = 'PostalAddress']/*[local-name() = 'StreetName']";

    public const string SellerBuildingNumberXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'AccountingSellerParty']/*[local-name() = 'Party']/*[local-name() = 'PostalAddress']/*[local-name() = 'BuildingNumber']";

    public const string SellerPlotIdentificationXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'AccountingSellerParty']/*[local-name() = 'Party']/*[local-name() = 'PostalAddress']/*[local-name() = 'PlotIdentification']";

    public const string SellerCitySubdivisionNameXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'AccountingSellerParty']/*[local-name() = 'Party']/*[local-name() = 'PostalAddress']/*[local-name() = 'CitySubdivisionName']";

    public const string SellerCityNameXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'AccountingSellerParty']/*[local-name() = 'Party']/*[local-name() = 'PostalAddress']/*[local-name() = 'CityName']";

    public const string SellerPostalZoneXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'AccountingSellerParty']/*[local-name() = 'Party']/*[local-name() = 'PostalAddress']/*[local-name() = 'PostalZone']";

    public const string SellerCountrySubEntityXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'AccountingSellerParty']/*[local-name() = 'Party']/*[local-name() = 'PostalAddress']/*[local-name() = 'CountrySubentity']";

    public const string SellerCountryXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'AccountingSellerParty']/*[local-name() = 'Party']/*[local-name() = 'PostalAddress']/*[local-name() = 'Country']/*[local-name() = 'Country']";

    public const string SellerRegistrationNameXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'AccountingSellerParty']/*[local-name() = 'Party']/*[local-name() = 'PartyLegalEntity']/*[local-name() = 'RegistrationName']";

    public const string SellerCrnXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'AccountingSellerParty']/*[local-name() = 'Party']/*[local-name() = 'PartyIdentification']/*[local-name() = 'ID']";

    public const string SellerTaxSchemeXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'AccountingSellerParty']/*[local-name() = 'Party']/*[local-name() = 'PartyTaxScheme']/*[local-name() = 'TaxScheme']/*[local-name() = 'ID']";

    public const string BuyerStreetNameXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'AccountingBuyerParty']/*[local-name() = 'Party']/*[local-name() = 'PostalAddress']/*[local-name() = 'StreetName']";

    public const string BuyerBuildingNumberXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'AccountingBuyerParty']/*[local-name() = 'Party']/*[local-name() = 'PostalAddress']/*[local-name() = 'BuildingNumber']";

    public const string BuyerPlotIdentificationXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'AccountingBuyerParty']/*[local-name() = 'Party']/*[local-name() = 'PostalAddress']/*[local-name() = 'PlotIdentification']";

    public const string BuyerCitySubdivisionNameXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'AccountingBuyerParty']/*[local-name() = 'Party']/*[local-name() = 'PostalAddress']/*[local-name() = 'CitySubdivisionName']";

    public const string BuyerCityNameXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'AccountingBuyerParty']/*[local-name() = 'Party']/*[local-name() = 'PostalAddress']/*[local-name() = 'CityName']";

    public const string BuyerPostalZoneXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'AccountingBuyerParty']/*[local-name() = 'Party']/*[local-name() = 'PostalAddress']/*[local-name() = 'PostalZone']";

    public const string BuyerCountrySubEntityXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'AccountingBuyerParty']/*[local-name() = 'Party']/*[local-name() = 'PostalAddress']/*[local-name() = 'CountrySubentity']";

    public const string BuyerCountryXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'AccountingBuyerParty']/*[local-name() = 'Party']/*[local-name() = 'PostalAddress']/*[local-name() = 'Country']/*[local-name() = 'Country']";

    public const string BuyerRegistrationNameXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'AccountingBuyerParty']/*[local-name() = 'Party']/*[local-name() = 'PartyLegalEntity']/*[local-name() = 'RegistrationName']";

    public const string BuyerNatXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'AccountingBuyerParty']/*[local-name() = 'Party']/*[local-name() = 'PartyIdentification']/*[local-name() = 'ID']";

    public const string BuyerTaxSchemeXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'AccountingBuyerParty']/*[local-name() = 'Party']/*[local-name() = 'PartyTaxScheme']/*[local-name() = 'TaxScheme']/*[local-name() = 'ID']";

    public const string AllowanceXpath = "/*[local-name() = 'Invoice']/*[local-name() = 'AllowanceCharge']";

    public const string TaxTotalTaxAmountXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'TaxTotal']/*[local-name() = 'TaxAmount']";

    public const string TaxTotalTaxAmountSecondTagXpath =
        "(/*[local-name() = 'Invoice']/*[local-name() = 'TaxTotal']/*[local-name() = 'TaxAmount'])[2]";

    public const string TaxTotalSubTotalXpath = "/*[local-name() = 'Invoice']/*[local-name() = 'TaxTotal']/*[local-name() = 'TaxSubtotal']";

    public const string TaxTotalSubTotalTaxableAmountXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'TaxTotal']/*[local-name() = 'TaxSubtotal']/*[local-name() = 'TaxableAmount']";

    public const string TaxTotalSubTotalTaxAmountXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'TaxTotal']/*[local-name() = 'TaxSubtotal']/*[local-name() = 'TaxAmount']";

    public const string TaxTotalSubTotalTaxCategoryIdTaxAmountXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'TaxTotal']/*[local-name() = 'TaxSubtotal']/*[local-name() = 'TaxCategory']/*[local-name() = 'ID']";

    public const string TaxTotalSubTotalTaxPercentTaxAmountXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'TaxTotal']/*[local-name() = 'TaxSubtotal']/*[local-name() = 'TaxCategory']/*[local-name() = 'Percent']";
    public const string TaxTotalSubTotalTaxSchemeIDXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'TaxTotal']/*[local-name() = 'TaxSubtotal']/*[local-name() = 'TaxCategory']/*[local-name() = 'TaxScheme']/*[local-name() = 'ID']";

    public const string TaxTotalSubTotalTaxExemptionReasonCodeXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'TaxTotal']/*[local-name() = 'TaxSubtotal']/*[local-name() = 'TaxCategory']/*[local-name() = 'TaxExemptionReasonCode']";
    public const string TaxTotalSubTotalTaxExemptionReasonXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'TaxTotal']/*[local-name() = 'TaxSubtotal']/*[local-name() = 'TaxCategory']/*[local-name() = 'TaxExemptionReason']";

    public const string CurrencyIdAttributeName = "currencyID";

    public const string LineItemXpath = "/*[local-name() = 'Invoice']/*[local-name() = 'InvoiceLine']";

    public const string LineItemIdXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'InvoiceLine']/*[local-name() = 'ID']";

    public const string LineItemInvoicedQuantityXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'InvoiceLine']/*[local-name() = 'InvoicedQuantity']";

    public const string LineItemLineExtensionAmountXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'InvoiceLine']/*[local-name() = 'LineExtensionAmount']";

    public const string LineItemTaxAmountXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'InvoiceLine']/*[local-name() = 'TaxTotal']/*[local-name() = 'TaxAmount']";

    public const string LineItemRoundingAmountXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'InvoiceLine']/*[local-name() = 'TaxTotal']/*[local-name() = 'RoundingAmount']";

    public const string LineItemNameXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'InvoiceLine']/*[local-name() = 'Item']/*[local-name() = 'Name']";

    public const string LineItemTaxClassificationIdXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'InvoiceLine']/*[local-name() = 'Item']/*[local-name() = 'ClassifiedTaxCategory']/*[local-name() = 'ID']";

    public const string LineItemTaxPercentXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'InvoiceLine']/*[local-name() = 'Item']/*[local-name() = 'ClassifiedTaxCategory']/*[local-name() = 'Percent']";

    public const string LineItemTaxSchemeXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'InvoiceLine']/*[local-name() = 'Item']/*[local-name() = 'ClassifiedTaxCategory']/*[local-name() = 'TaxScheme']/*[local-name() = 'ID']";

    public const string LineItemPriceAmountXpath = "/*[local-name() = 'Invoice']/*[local-name() = 'InvoiceLine']/*[local-name() = 'Price']/*[local-name() = 'PriceAmount']";

    public const string LineItemAllowanceXpath = "/*[local-name() = 'Invoice']/*[local-name() = 'InvoiceLine']/*[local-name() = 'Price']/*[local-name() = 'AllowanceCharge']";
    public const string LineItemAllowanceChargeIndicatorXpath = "/*[local-name() = 'Invoice']/*[local-name() = 'InvoiceLine']/*[local-name() = 'Price']/*[local-name() = 'AllowanceCharge']/*[local-name() = 'ChargeIndicator']";
    public const string LineItemAllowanceChargeReasonXpath = "/*[local-name() = 'Invoice']/*[local-name() = 'InvoiceLine']/*[local-name() = 'Price']/*[local-name() = 'AllowanceCharge']/*[local-name() = 'AllowanceChargeReason']";
    public const string LineItemAllowanceChargeAmountXpath = "/*[local-name() = 'Invoice']/*[local-name() = 'InvoiceLine']/*[local-name() = 'Price']/*[local-name() = 'AllowanceCharge']/*[local-name() = 'Amount']";
    public const string LineItemAllowanceChargeTaxCategoreIdXpath = "/*[local-name() = 'Invoice']/*[local-name() = 'InvoiceLine']/*[local-name() = 'Price']/*[local-name() = 'AllowanceCharge']/*[local-name() = 'TaxCategory']/*[local-name() = 'ID']";
    public const string LineItemAllowanceChargeTaxCategorePercentXpath = "/*[local-name() = 'Invoice']/*[local-name() = 'InvoiceLine']/*[local-name() = 'Price']/*[local-name() = 'AllowanceCharge']/*[local-name() = 'TaxCategory']/*[local-name() = 'Percent']";
    public const string LineItemAllowanceChargeTaxCategoreChemeIdXpath = "/*[local-name() = 'Invoice']/*[local-name() = 'InvoiceLine']/*[local-name() = 'Price']/*[local-name() = 'AllowanceCharge']/*[local-name() = 'TaxCategory']/*[local-name() = 'TaxScheme']/*[local-name() = 'ID']";

    public const string LegalMonetaryTotalLineExtensionAmountXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'LegalMonetaryTotal']/*[local-name() = 'LineExtensionAmount']";

    public const string LegalMonetaryTotalTaxExclusiveAmountXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'LegalMonetaryTotal']/*[local-name() = 'TaxExclusiveAmount']";

    public const string LegalMonetaryTotalTaxInclusiveAmountXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'LegalMonetaryTotal']/*[local-name() = 'TaxInclusiveAmount']";

    public const string LegalMonetaryTotalAllowanceTotalAmountXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'LegalMonetaryTotal']/*[local-name() = 'AllowanceTotalAmount']";

    public const string LegalMonetaryTotalPrepaidAmountXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'LegalMonetaryTotal']/*[local-name() = 'PrepaidAmount']";

    public const string LegalMonetaryTotalPayableAmountXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'LegalMonetaryTotal']/*[local-name() = 'PayableAmount']";

    public const string InvoiceReferenceXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'BillingReference']/*[local-name() = 'InvoiceDocumentReference']/*[local-name() = 'ID']";

    public const string AdjustmentReasonXpath =
        "/*[local-name() = 'Invoice']/*[local-name() = 'PaymentMeans']/*[local-name() = 'InstructionNote']";
    internal static string PihXpath = "/*[local-name() = 'Invoice']/*[local-name() = 'AdditionalDocumentReference' and *[local-name()='ID' and .='PIH']]/*[local-name() = 'Attachment']/*[local-name() = 'EmbeddedDocumentBinaryObject']";

}

