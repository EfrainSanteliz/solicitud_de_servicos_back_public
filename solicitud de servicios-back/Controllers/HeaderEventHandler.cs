using iText.Kernel.Events;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Geom; // This is needed for Rectangle



namespace solicitud_de_servicios_back.Controllers
{
    public class HeaderEventHandler : IEventHandler
    {
        private Document _document;

        public HeaderEventHandler(Document document)
        {
            _document = document;
        }

        public void HandleEvent(Event currentEvent)
        {
            PdfDocumentEvent docEvent = (PdfDocumentEvent)currentEvent;
            PdfPage page = docEvent.GetPage();
            PdfCanvas pdfCanvas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), docEvent.GetDocument());

            // Add page number
            int pageNumber = docEvent.GetDocument().GetPageNumber(page);
            Rectangle pageSize = page.GetPageSize();

            Canvas canvas = new Canvas(pdfCanvas, pageSize);
            canvas.SetFontSize(10);

            // Add the header text (left-aligned)
            canvas.ShowTextAligned("Solicitud de Servicios", pageSize.GetLeft() + 30, pageSize.GetTop() - 20, TextAlignment.LEFT);

            // Add page number (right-aligned)
            canvas.ShowTextAligned($"Page {pageNumber}", pageSize.GetRight() - 30, pageSize.GetTop() - 20, TextAlignment.RIGHT);

          
        }
    }
}
