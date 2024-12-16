using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using solicitud_de_servicios_back.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
//using iTextSharp.text;
//using iTextSharp.text.pdf;

using System.IO;
using iText.IO.Image;


namespace solicitud_de_servicios_back.Controllers
{
    [Route("api/PDF")]
    [ApiController]
    public class PDFCONTROLLER : ControllerBase
    {
        private readonly UserContext _userContext;
        private readonly ILogger<PDFCONTROLLER> _logger;

        public PDFCONTROLLER(UserContext userContext, ILogger<PDFCONTROLLER> logger)
        {
            _userContext = userContext;
            _logger = logger;
        }

        // GET: api/Request
      /**  [HttpGet]
        public async Task<ActionResult<IEnumerable<SS_Solicitud>>> GetRequest()
        {
            var requests = await _userContext.SS_Solicitudes
           .Include(r => r.SS_Solicitud_de_servicios)
           .Include(r => r.SS_Servicio_Solicitados)
           .Include(r => r.NomEmpleados)
           .ThenInclude(n => n.DireccionesICEES)
           .Include(r => r.ConActivosFijos)
           .Include(r => r.HistorialComentarios)
           .ToListAsync();

            return Ok(requests);
        }

         [HttpGet("download-pdf2/{id}")]
         public async Task<IActionResult> GetRequestAsPdf2(int id)
         {
             // Retrieve the request data from the database
             var request = await _userContext.Requests
                 .Include(r => r.Solicitud_de_servicio)
                 .Include(r => r.Servicio_Solicitado)
                 .Include(r => r.Usuarios)
                     .ThenInclude(u => u.NomEmpleados)
                         .ThenInclude(n => n.DireccionesICEES)
                 .Include(r => r.ConActivosFijos)
                 .Include(r => r.Historials)
                 .FirstOrDefaultAsync(r => r.Id == id);

             if (request == null)
             {
                 return NotFound();
             }

             var descripcion = request.Descripcion;

             try
             {
                 using (var memoryStream = new MemoryStream())
                 {
                     // Create a PDF document with appropriate margins
                     var document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 36, 36, 36, 36);

                     // Set up PDF writer instance
                     iTextSharp.text.pdf.PdfWriter.GetInstance(document, memoryStream);

                     document.Open();

                     // Add the description with potential adjustments for layout
                     var paragraph = new iTextSharp.text.Paragraph(descripcion);
                     paragraph.Alignment = Element.ALIGN_LEFT; // Adjust alignment as needed

                     // Calculate paragraph height and line count (consider adjustments)
                     float lineHeight = paragraph.Font.Size * paragraph.MultipliedLeading; // More accurate height
                     int charsPerLine = GetEstimatedCharactersPerLine(paragraph.Font.Size, document.PageSize.Width - document.LeftMargin - document.RightMargin);

                     int numberOfLines = (int)Math.Ceiling((double)descripcion.Length / charsPerLine);
                     float descriptionHeight = lineHeight * numberOfLines;

                     // Check for exceeding 70% of page height, adjust logic as needed
                     if (descriptionHeight > document.PageSize.Height * 0.7)
                     {
                         var warningParagraph = new iTextSharp.text.Paragraph("El texto ha superado el 70% de la página.");
                         document.Add(warningParagraph);
                     }
                     else
                     {
                         document.Add(paragraph);
                     }

                     document.Close();

                     // Return the PDF file
                     return File(memoryStream.ToArray(), "application/pdf", "request.pdf");
                 }
             }
             catch (Exception ex)
             {
                 // Handle exceptions gracefully, log errors, return appropriate responses
                 Console.WriteLine($"Error generating PDF: {ex.Message}");
                 return StatusCode(500, "Error generando el PDF"); // Or other appropriate status code and message
             }
         }

         // Helper method to estimate characters per line based on font size and page width
         private int GetEstimatedCharactersPerLine(float fontSize, float availableWidth)
         {
             // Adjust this calculation based on your specific font and layout needs
             const float averageCharacterWidth = 6; // Adjust based on font characteristics
             return (int)Math.Floor(availableWidth / averageCharacterWidth);
         }


         [HttpGet("PDF/{id}")]
         public async Task<IActionResult> GetRequestAsPdf(int id)
         {
             // Recuperar los datos de la solicitud de la base de datos
             var request = await _userContext.Requests
                 .Include(r => r.Solicitud_de_servicio)
                 .Include(r => r.Servicio_Solicitado)
                 .Include(r => r.Usuarios)
                     .ThenInclude(u => u.NomEmpleados)
                     .ThenInclude(n => n.DireccionesICEES)
                 .Include(r => r.ConActivosFijos)
                 .Include(r => r.Historials) // Incluye los activos fijos
                 .FirstOrDefaultAsync(r => r.Id == id);

             if (request == null)
             {
                 return NotFound();
             }

             var Descripcion = request.Descripcion;
             var caracterCount = Descripcion.Length; // Contar caracteres

             // Crear el PDF
             using (var memoryStream = new MemoryStream())
             {
                 // Inicializar el documento
                 Document document = new Document();
                 PdfWriter.GetInstance(document, memoryStream);
                 document.Open();

                 // Añadir contenido al PDF
                 document.Add(new Paragraph("Descripción de la Solicitud"));
                 document.Add(new Paragraph(Descripcion)); // Añadir la descripción
                 document.Add(new Paragraph($"Cantidad de caracteres: {caracterCount}")); // Añadir conteo de caracteres

                 // Cerrar el documento
                 document.Close();

                 var pdfBytes = memoryStream.ToArray();
                 return File(pdfBytes, "application/pdf", "Solicitud.pdf"); // Devolver el PDF como archivo
             }
         }

        [HttpGet("PDF/{id}")]
        public async Task<IActionResult> GetRequestAsPdf(int id)
        {
            // Recuperar el request de la base de datos
            var request = await _userContext.SS_Solicitudes

                 .Include(u => u.NomEmpleados)
                    .ThenInclude(n => n.DireccionesICEES)
                .Include(r => r.SS_Solicitud_de_servicios)
                .Include(r => r.SS_Servicio_Solicitados)
                   
                .Include(r => r.ConActivosFijos)
                .Include(r => r.HistorialComentarios) // Incluye los activos fijos
                .FirstOrDefaultAsync(r => r.SS_SolicitudId == id);

            if (request == null)
            {
                return NotFound();
            }

            var descripcion = request.Descripcion ?? ""; // Asegúrate de que no sea null
            int charCount = descripcion.Length; // Cuenta los caracteres


            // Obtener la ruta de la imagen desde wwwroot y cargar la imagen
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", request.File.TrimStart('/'));
            var img = new Image(ImageDataFactory.Create(imagePath));

            // Crear un MemoryStream para el PDF
            using (var stream = new MemoryStream())
            {
                // Inicializar el PDF writer y document
                using (var writer = new PdfWriter(stream))
                {
                    using (var pdf = new PdfDocument(writer))
                    {
                        var document = new Document(pdf);

                        // Añadir el texto de la descripción
                        document.Add(new Paragraph("Descripción:"));
                        document.Add(new Paragraph(descripcion));

                        // Añadir texto con la cantidad de caracteres
                        document.Add(new Paragraph($"La descripción tiene {charCount} caracteres."));

                        document.Add(new Paragraph($"La imagen tiene una altura de {img.GetImageHeight()} píxeles."));

                    }
                }



                // Preparar la respuesta HTTP
                var pdfBytes = stream.ToArray();
                return File(pdfBytes, "application/pdf", $"Request_{id}_CaracteresDescripcion.pdf");
            }
        }*/

    }
}