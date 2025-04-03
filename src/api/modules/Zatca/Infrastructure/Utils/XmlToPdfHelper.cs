using System.Xml;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Filespec;
using iText.Kernel.Utils;
using iText.Layout;
using iText.Layout.Element;

namespace FSH.Starter.WebApi.Zatca.Infrastructure.Utils
{
    public static class XmlToPdfHelper
    {
        public static PDFA3Result ConvertToPDFA3(string xmlFileName, string pdfFileName)
        {
            return ConvertToPDFA3Internal(xmlFileName, pdfFileName, null);
        }

        private static PDFA3Result ConvertToPDFA3Internal(string xmlFileName, string? pdfFileName, byte[]? pdfContent, bool returnByteArray = false)
        {
            var pdfResult = new PDFA3Result { IsValid = false };

            try
            {
                var doc = LoadXmlDocument(xmlFileName, pdfResult);
                if (doc == null) return pdfResult;

                var invoiceId = XmlHelper.GetNodeValue(doc, StandardInvoiceXmlGenerator.InvoiceIdXpath);
                if (string.IsNullOrEmpty(invoiceId))
                {
                    pdfResult.ErrorMessage = "Invoice Id not found in XML.";
                    return pdfResult;
                }

                if (pdfContent == null && !File.Exists(pdfFileName))
                {
                    pdfResult.ErrorMessage = "PDF file doesn't exist.";
                    return pdfResult;
                }

                string outputPdfA3Name = returnByteArray ? string.Empty : GetOutputPdfPath(pdfFileName!, invoiceId);
                byte[]? fileContent = null;

                using (Stream outputStream = returnByteArray ? new MemoryStream() : new FileStream(outputPdfA3Name, FileMode.Create))
                {
                    var writerProperties = new WriterProperties();
                    writerProperties.SetPdfVersion(PdfVersion.PDF_1_7);

                    using PdfWriter pdfWriter = new PdfWriter(outputStream, writerProperties);
                    var pdfDocument = new PdfDocument(pdfWriter);

                    using var contentStram = new MemoryStream(pdfContent!);

                    // Load existing PDF
                    using var pdfReader = pdfContent != null ? new PdfReader(contentStram) : new PdfReader(pdfFileName!);
                    using PdfDocument existingPdf = new PdfDocument(pdfReader);
                    var pdfMerger = new PdfMerger(pdfDocument);
                    pdfMerger.Merge(existingPdf, 1, existingPdf.GetNumberOfPages());

                    existingPdf.Close();

                    var document = new Document(pdfDocument);
                    document.Add(new Paragraph("PDF/A-3 Conversion").SimulateBold());

                    // Attach XML file
                    var attachmentName = GetAttachmentName(xmlFileName, invoiceId);
                    AttachXmlToPdf(pdfDocument, xmlFileName, attachmentName);

                    // Close document
                    document.Close();
                    pdfDocument.Close();
                    pdfWriter.Close();

                    if (returnByteArray)
                    {
                        using MemoryStream memoryStream = new MemoryStream();
                        outputStream.CopyTo(memoryStream);
                        fileContent = memoryStream.ToArray();
                    }
                }

                pdfResult.IsValid = true;
                pdfResult.PDFA3FileNameFullPath = returnByteArray ? string.Empty : outputPdfA3Name;
                pdfResult.PDFA3FileName = returnByteArray ? string.Empty : $"{invoiceId}_PDFA3.pdf";
                pdfResult.PDFA3ContentFile = fileContent ?? Array.Empty<byte>();
            }
            catch (Exception ex)
            {
                pdfResult.ErrorMessage = ex.Message;
            }

            return pdfResult;
        }

        private static void AttachXmlToPdf(PdfDocument pdfDocument, string xmlFilePath, string attachmentName)
        {
            byte[] xmlBytes = File.ReadAllBytes(xmlFilePath);
            PdfFileSpec fileSpec = PdfFileSpec.CreateEmbeddedFileSpec(
                pdfDocument, xmlBytes, "Description", "filename.pdf", PdfName.ApplicationPdf, null, new PdfName(attachmentName));



            PdfDictionary embeddedFiles = pdfDocument.GetCatalog().GetPdfObject().GetAsDictionary(PdfName.Names)
                                          ?? new PdfDictionary();
            PdfDictionary fileDict = new PdfDictionary();
            fileDict.Put(new PdfName(attachmentName), fileSpec.GetPdfObject());

            embeddedFiles.Put(PdfName.EmbeddedFiles, fileDict);
            pdfDocument.GetCatalog().Put(PdfName.Names, embeddedFiles);
        }

        private static XmlDocument? LoadXmlDocument(string xmlFileName, PDFA3Result pdfResult)
        {
            var doc = new XmlDocument();
            try
            {
                doc.Load(xmlFileName);
                return doc;
            }
            catch
            {
                pdfResult.ErrorMessage = "Cannot load XML file.";
                return null;
            }
        }

        private static string GetOutputPdfPath(string filename, string invoiceId)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), filename + invoiceId);
        }

        private static string GetAttachmentName(string xmlFileName, string invoiceId)
        {
            var xmlFile = Path.GetFileName(xmlFileName);
            return $"{invoiceId}_{xmlFile}";
        }

    }
}
