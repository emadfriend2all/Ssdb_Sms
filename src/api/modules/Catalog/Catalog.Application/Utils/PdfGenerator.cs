using System.Globalization;
using FSH.Starter.WebApi.Catalog.Domain;
using iText.Kernel.Pdf;
using QuestPDF;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace FSH.Starter.WebApi.Catalog.Application.Utils;
public class StandardInvoiceDocument : IDocument
{
    private readonly Invoice _invoice;
    public Color PrimaryColor { get; set; }
    public const int HeaderFontSize = 20;
    public const int TitleFontSize = 14;
    public const int BodyFontSize = 12;

    public StandardInvoiceDocument(Invoice invoice)
    {
        Settings.License = LicenseType.Community;
        _invoice = invoice;
        PrimaryColor = Colors.Blue.Medium;
    }

    public DocumentMetadata GetMetadata() => new DocumentMetadata { Title = $"Invoice {_invoice.Number}" };

    public void Compose(IDocumentContainer container)
    {
        RegisterFonts();

        container.Page(page =>
        {
            page.DefaultTextStyle(TextStyle.Default.FontFamily("Somar"));
            page.Margin(50);

            page.Header().Element(ComposeHeader);

            page.Content().PaddingVertical(10).Column(column =>
            {
                column.Spacing(5);

                // Seller and Buyer Information
                column.Item().Row(row =>
                {
                    row.RelativeItem().Layers(layers => ComposeAddressBox(layers, "معلومات البائع", _invoice.InvoiceSeller));
                    row.ConstantItem(40); // Spacing
                    row.RelativeItem().Layers(layers => ComposeAddressBox(layers, "معلومات المشتري", _invoice.InvoiceBuyer));
                });

                // Invoice Details
                column.Item().PaddingVertical(10).Element(ComposeInvoiceTable);
                column.Item().PaddingVertical(10).Element(ComposeAllowances);
                column.Item().PaddingVertical(10).Element(ComposeTotals);
            });

            page.Footer().AlignCenter().Text(x =>
            {
                x.CurrentPageNumber();
                x.Span(" / ");
                x.TotalPages();
            });
        });
    }

    private static void RegisterFonts()
    {
        var fontPath = $"{typeof(StandardInvoiceDocument).Namespace}.fonts.";
        FontManager.RegisterFontFromEmbeddedResource($"{fontPath}Qlines-Regular.ttf");
        FontManager.RegisterFontFromEmbeddedResource($"{fontPath}Somar-Regular.otf");
    }

    private void ComposeHeader(IContainer container)
    {
        container.AlignRight().Row(row =>
        {
            row.ConstantItem(100).Height(50).Width(100).Image("assets/defaults/logo.png");

            row.RelativeItem().AlignRight().Column(column =>
            {
                column.Item().AlignRight().Text("فاتورة ضريبية").FontSize(HeaderFontSize).SemiBold().FontColor(PrimaryColor);

                column.Item().AlignRight().Row(innerRow =>
                {
                    innerRow.RelativeItem().AlignRight().Text($"{_invoice.IssueDateTime:d}").FontSize(BodyFontSize);
                    innerRow.ConstantItem(60).AlignRight().Text("تاريخ الإصدار").FontSize(BodyFontSize).SemiBold();
                });

                column.Item().AlignRight().Row(innerRow =>
                {
                    innerRow.RelativeItem().AlignRight().Text($"{_invoice.Number}").FontSize(BodyFontSize);
                    innerRow.ConstantItem(60).AlignRight().Text("الرقم التسلسلي").FontSize(BodyFontSize).SemiBold();
                });
            });
        });
    }

    private void ComposeAddressBox(LayersDescriptor layers, string title, object addressInfo)
    {
        layers.Layer().Svg(size => CreateGradientBox(size));
        layers.PrimaryLayer()
            .PaddingVertical(10)
            .PaddingHorizontal(20)
            .Column(column =>
            {
                column.Item().Text(title).AlignRight().FontSize(TitleFontSize).Bold().FontColor(PrimaryColor);

                if (addressInfo is InvoiceSeller seller)
                {
                    AddAddressRow(column, ": الإسم", seller.Name);
                    AddAddressRow(column, ": العنوان", seller.Address?.City);
                    AddAddressRow(column, ": الرقم الضريبي", seller.Identifier);
                    AddAddressRow(column, ": رقم السجل التجاري", seller.Identifier);
                }
                else if (addressInfo is InvoiceBuyer buyer)
                {
                    AddAddressRow(column, ": الإسم", buyer.Name);
                    AddAddressRow(column, ": العنوان", buyer.Address?.City);
                    AddAddressRow(column, ": الرقم الضريبي", buyer.TaxNumber);
                    AddAddressRow(column, ": رقم السجل التجاري", buyer.TaxNumber);
                }
            });
    }

    private static void AddAddressRow(ColumnDescriptor column, string label, string? value)
    {
        column.Item().Row(row =>
        {
            row.RelativeItem().Text(value ?? "- - - - - - - - - - -").AlignRight();
            row.ConstantItem(70).Text(label).AlignRight();
        });
    }

    private void ComposeInvoiceTable(IContainer container)
    {
        container.Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.RelativeColumn(1);
                columns.RelativeColumn(1);
                columns.RelativeColumn(1);
                columns.RelativeColumn(1);
                columns.RelativeColumn(1);
                columns.RelativeColumn(1);
                columns.RelativeColumn(4);
            });

            table.Header(header =>
            {
                header.Cell().Element(CellStyle).AlignRight().Text("المجموع VatInc").FontColor(PrimaryColor);
                header.Cell().Element(CellStyle).AlignRight().Text("قيمة الضريبة").FontColor(PrimaryColor);
                header.Cell().Element(CellStyle).AlignCenter().Text("نسبة الضريبة").FontColor(PrimaryColor);
                header.Cell().Element(CellStyle).AlignRight().Text("المجموع VatEx").FontColor(PrimaryColor);
                header.Cell().Element(CellStyle).AlignRight().Text("الكمية").FontColor(PrimaryColor);
                header.Cell().Element(CellStyle).AlignRight().Text("سعر الوحدة").FontColor(PrimaryColor);
                header.Cell().Element(CellStyle).AlignRight().Text("المنتج").FontColor(PrimaryColor);

                static IContainer CellStyle(IContainer container)
                {
                    return container.DefaultTextStyle(x => x.SemiBold()).Background(Colors.Grey.Lighten3).PaddingHorizontal(5).PaddingVertical(3).BorderColor(Colors.Black);
                }
            });

            foreach (var item in _invoice.InvoiceLines)
            {
                table.Cell().Element(CellStyle).AlignRight().Text((item.Quantity * item.ItemPrice).ToString("N2"));
                table.Cell().Element(CellStyle).AlignRight().Text(item.VatAmount.ToString());
                table.Cell().Element(CellStyle).AlignCenter().Text($"{item.TaxCategory.Percent:N0}%");
                table.Cell().Element(CellStyle).AlignRight().Text(item.NetAmount.ToString());
                table.Cell().Element(CellStyle).AlignRight().Text(item.Quantity.ToString("N1"));
                table.Cell().Element(CellStyle).AlignRight().Text(item.ItemPrice.ToString());
                table.Cell().Element(CellStyle).AlignRight().Text(item.ItemName);

                static IContainer CellStyle(IContainer container)
                {
                    return container.BorderBottom(0.2f).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                }
            }
        });
    }

    private void ComposeAllowances(IContainer container)
    {
        if (_invoice.Allowances?.Count != 0)
        {
            container.Column(column =>
            {
                column.Item().Text("Allowances").SemiBold();
                foreach (var allowance in _invoice.Allowances ?? [])
                {
                    column.Item().Text($"{allowance.AllowanceChargeReason}: {allowance.Amount:C2}");
                }
            });
        }
    }

    private void ComposeTotals(IContainer container)
    {
        container.AlignRight().Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                AddTotalRow(column, _invoice.LegalMonetaryTotal.TaxExclusiveAmount.ToString("C2", new CultureInfo("ar-SA")), "المجموع (بدون الضريبة)");
                AddTotalRow(column, _invoice.LegalMonetaryTotal.LineExtensionAmount.ToString("C2", new CultureInfo("ar-SA")), "الضريبة");
                AddTotalRow(column, _invoice.LegalMonetaryTotal.PayableAmount.ToString("C2", new CultureInfo("ar-SA")), "المجموع", true);
            });


            row.ConstantItem(100).Height(100).Width(100).Layers(layers =>
            {
                layers.Layer().ScaleToFit().Svg(size => CreateGradientBox(size));
                if (_invoice.QrCode is not null)
                {
                    layers.PrimaryLayer().ScaleToFit()
                    .Column(column =>
                    {
                        column.Item().Image(GenerateQrCode(_invoice.QrCode)).FitArea();
                    });
                }
                else
                {
                    layers.PrimaryLayer().PaddingVertical(10).PaddingHorizontal(20);
                }
            });


        });
    }

    private void AddTotalRow(ColumnDescriptor column, string value, string label, bool isBold = false)
    {
        column.Item().Row(row =>
        {
            if (isBold)
            {
                row.ConstantItem(100).AlignLeft().Text(value).Bold().FontColor(PrimaryColor).FontSize(TitleFontSize);
                row.RelativeItem().AlignLeft().Text(label).Bold().FontColor(PrimaryColor).FontSize(TitleFontSize);
            }
            else
            {
                row.ConstantItem(100).AlignLeft().Text(value).FontColor(Colors.Black);
                row.RelativeItem().AlignLeft().Text(label).FontColor(Colors.Black);
            }
        });
    }

    private static string CreateGradientBox(Size size)
    {
        return $"""
            <svg width="{size.Width}" height="{size.Height}" xmlns="http://www.w3.org/2000/svg">
                <defs>
                  <linearGradient id="backgroundGradient" x1="0%" y1="0%" x2="100%" y2="100%">
                    <stop stop-color="#e8e8e8" offset="0%"/>
                    <stop stop-color="#e8e8e8" offset="100%"/>
                  </linearGradient>
                </defs>
                <rect x="0" y="0" width="{size.Width}" height="{size.Height}" rx="{size.Height / 9}" ry="{size.Height / 9}" fill="url(#backgroundGradient)" />
            </svg>
            """;
    }

    public static byte[] GenerateQrCode(string content)
    {
        using QRCoder.QRCodeGenerator qrGenerator = new QRCoder.QRCodeGenerator();
        QRCoder.QRCodeData qrCodeData = qrGenerator.CreateQrCode(content, QRCoder.QRCodeGenerator.ECCLevel.Q);
        using QRCoder.PngByteQRCode qrCode = new QRCoder.PngByteQRCode(qrCodeData);
        return qrCode.GetGraphic(20);
    }
}
