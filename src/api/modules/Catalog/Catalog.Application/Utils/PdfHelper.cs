using System.Text;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Filespec;
using iText.Pdfa;
using iText.Pdfua;

namespace FSH.Starter.WebApi.Catalog.Application.Utils
{
    public static class PdfHelper
    {
        public static PDFA3Result ConvertToPDFA3(byte[] pdfFileBytes, Dictionary<string, string> attachments)
        {
            using var reader = new PdfReader(new MemoryStream(pdfFileBytes));
            using var ms = new MemoryStream();
            var writerProperties = new WriterProperties().SetPdfVersion(PdfVersion.PDF_1_7);
            using var writer = new PdfWriter(ms, writerProperties);
            using var doc = new PdfDocument(reader, writer);

            var outputIntent = new PdfOutputIntent("Custom", "", "http://www.color.org", "sRGB", null);
            var catalog = doc.GetCatalog();
            catalog.Put(PdfName.OutputIntent, outputIntent.GetPdfObject()); 

            doc.AttachToPdf(attachments);
            doc.Close();

            var pdfBytes = ms.ToArray();

            return new PDFA3Result
            {
                PDFA3FileName = $"{Guid.NewGuid()}.pdf",
                PDFA3ContentFile = pdfBytes
            };

        }

        private static void AttachToPdf(this PdfDocument pdfDocument, Dictionary<string, string> attachments)
        {
            var embeddedFiles = pdfDocument.GetCatalog().GetPdfObject().GetAsDictionary(PdfName.Names) ?? new PdfDictionary();
            var fileDict = new PdfDictionary();

            foreach (var attachment in attachments)
            {
                string attachmentName = attachment.Key;
                string xmlContent = attachment.Value;

                byte[] fileBytes = Encoding.UTF8.GetBytes(xmlContent);

                // Create the embedded file specification
                var fileSpec = PdfFileSpec.CreateEmbeddedFileSpec(
                    pdfDocument,
                    fileBytes,
                    $"{attachmentName} Description",
                    attachmentName,
                    PdfName.ApplicationPdf,
                    null,
                    new PdfName(attachmentName)
                );

                pdfDocument.AddFileAttachment("specificname", fileSpec);
                fileDict.Put(new PdfName(attachmentName), fileSpec.GetPdfObject());
            }

            // Add the file dictionary to the embedded files
            embeddedFiles.Put(PdfName.EmbeddedFiles, fileDict);

            // Update the catalog
            var catalog = pdfDocument.GetCatalog();
            catalog.Put(PdfName.Names, embeddedFiles);

            // Ensure the PDF is saved with the embedded file specifications
            pdfDocument.GetCatalog().Put(PdfName.Names, embeddedFiles);
        }
    }
}
