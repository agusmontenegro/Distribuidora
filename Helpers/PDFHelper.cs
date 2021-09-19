using Aspose.Pdf;

namespace Distribuidora.Helpers
{
    public static class PDFHelper
    {
        public static Document ImprimirDetalleDeVenta()
        {
            var document = new Document();
            ArmarEncabezado(document);

            // Add page
            Page page = document.Pages.Add();

            // Add text to new page
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Hello Worldgfdf!"));

            document.Save(@"Venta.pdf");

            return document;
        }

        private static void ArmarEncabezado(Document document)
        {

        }
    }
}
