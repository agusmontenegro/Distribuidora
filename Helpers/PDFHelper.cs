using Aspose.Pdf;

namespace Distribuidora.Helpers
{
    public class PDFHelper
    {
        public Document ImprimirDetalleDeVenta()
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

        private void ArmarEncabezado(Document document)
        {

        }
    }
}