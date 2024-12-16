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
using iText.Layout.Properties;
using iText.Bouncycastleconnector;


using System.IO;
using iText.IO.Image;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using solicitud_de_servicios_back.Controllers;
using solicitud_de_servicios_back.Services;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;
using System.ComponentModel.DataAnnotations;


namespace SocialMediaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly UserContext _userContext;
        private readonly UserContextPlasalcees _userContextPlasaibcess;

        private readonly ILogger<RequestController> _logger;

        public RequestController(UserContext userContext, UserContextPlasalcees userContextPlasaibcess, ILogger<RequestController> logger)
        {
            _userContext = userContext;
            _logger = logger;
            _userContextPlasaibcess = userContextPlasaibcess;
        }
        
        // GET: api/Request
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SS_Solicitud>>> GetRequest()
        {
            //var requests = await _userContext.SS_Solicitudes
               // .Include(r => r.NomEmpleados)
           //.ThenInclude(n => n.DireccionesICEES)
           //.Include(r => r.SS_Solicitud_de_servicios)
           //.Include(r => r.SS_Servicio_Solicitados)


            //.Include(r => r.ConActivosFijos)
            //.Include(r => r.Historials)
           //.ToListAsync();

            // Use FromSqlInterpolated to execute the stored procedure
            var solicitudDetails = await _userContext.SS_solicitudDetails
            .FromSqlInterpolated($"EXEC sp_ListSolicitudes")
            .ToListAsync();

            if (solicitudDetails == null || solicitudDetails.Count == 0)
            {
                return NotFound(new { message = "User not found or invalid credentials" });
            }

            var requests = solicitudDetails;

            return Ok(new
            {
                Request = requests,
              // Rques= request
            });


        }


        // GET: api/Request/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<SS_Solicitud>> GetRequest(int id)
        {
            var HistorialComentarios = await _userContext.SS_HistorialComentarios

             //.Include(r => r.SS_Solicitud_de_servicios)

             //.Include(s => s.NomEmpleados)
             //.ThenInclude(n => n.DireccionesICEES)
             //.Include(s => s.NomEmpleados)
             //.ThenInclude(n => n.Usuario)
             //.Include(r => r.ConActivosFijos)
             //.Include(r => r.HistorialComentarios)
             .Where(r => r.SS_SolicitudId == id)
             .ToListAsync();

            // Use FromSqlInterpolated to execute the stored procedure
            var showSolicitudes = await _userContext.SS_showSolicitudes
            .FromSqlInterpolated($"EXEC sp_showSolicitudes @SS_SolicitudId = {id}")
            .ToListAsync();

            if (showSolicitudes == null || showSolicitudes.Count == 0)
            {
                return NotFound(new { message = "solicitud not found" });
            }

            var request = showSolicitudes.First();

            //if (request == null)
           // {
             //   return NotFound();
           // }
            return Ok(new
            {
             //   Request = request,
               Request = request,
                historialComentarios = HistorialComentarios
            });
            
        }

        [HttpGet("download-pdf/{id}")]
        public async Task<IActionResult> GetRequestAsPdf(int id)
        {
            // Retrieve the request data from the database
            //var request = await _userContext.SS_Solicitudes

            //.include(r => r.SS_Solicitud_de_servicios)
             //.include(r => r.SS_Servicio_Solicitados)

             //.Include(r => r.NomEmpleados)
            // .ThenInclude(n => n.DireccionesICEES)

             //.Include(r => r.ConActivosFijos)
             //.Include(r => r.HistorialComentarios)

             //.FirstOrDefaultAsync(r => r.SS_SolicitudId == id);

            // Use FromSqlInterpolated to execute the stored procedure
            var showSolicitudes = await _userContext.SS_showSolicitudes
            .FromSqlInterpolated($"EXEC sp_showSolicitudes @SS_SolicitudId = {id}")
            .ToListAsync();

            if (showSolicitudes == null || showSolicitudes.Count == 0)
            {
                return NotFound(new { message = "User not found or invalid credentials" });
            }

            var request = showSolicitudes.First();

            if (request == null)
            {
                return NotFound();
            }

            // Validate the incoming model
            if (!ModelState.IsValid)
            {
                foreach (var modelError in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(modelError.ErrorMessage);  // Logs validation errors
                }
                return BadRequest(ModelState); // Return validation errors
            }

            // Create a PDF document in memory
            using (var memoryStream = new MemoryStream())
            {
                var writer = new PdfWriter(memoryStream);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);

                // Add the logo to the head of the document
                var logoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "LogoInstitutoDeBecasYCreditoEducativo.png");
                if (System.IO.File.Exists(logoPath))
                {
                    var logo = new iText.Layout.Element.Image(iText.IO.Image.ImageDataFactory.Create(logoPath));
                    logo.SetHorizontalAlignment(HorizontalAlignment.LEFT); // Align the logo at the center
                    logo.ScaleToFit(100, 100); // Adjust the size of the logo if necessary
                    document.Add(logo);
                }
                float lineSpacingFactor = 1f; // This is the multiplication factor for line spacing
                float lineSpacingFactor2 = 1f; // This is the multiplication factor for line spacing



                // Add content to the PDF
                document.Add(new Paragraph("SOLICITUD DE SERVICIOS SUBDIRECCION DE INFRAESTRUCTURA Y TECNOLOGIAS DE LA INFORMACION").SetFontSize(10).SetMultipliedLeading(lineSpacingFactor)); // Set multiplied leading;

                document.Add(new Paragraph($"Servicio solicitado: {request.DescripcionServicio_Solicitado}").SetFontSize(11).SetMultipliedLeading(lineSpacingFactor));
                document.Add(new Paragraph($"Fecha solicitada: {request.FechaSolicitada.ToString("dd/MM/yyyy")}").SetFontSize(11).SetMultipliedLeading(lineSpacingFactor));
                if (request.DescripcionSolicitud_De_Servicio != null)
                {
                    document.Add(new Paragraph($"Solicitud de servicio a realizar: {request.DescripcionSolicitud_De_Servicio}").SetFontSize(11).SetMultipliedLeading(lineSpacingFactor));
                }
                if (request.AFDescripcion != null)
                {
                    var conActivosFijosText = $"{request.AFClave ?? "N/A"} {request.AFDescripcion ?? "N/A"}";
                    document.Add(new Paragraph($"Recurso que presenta el problema: {conActivosFijosText}").SetFontSize(11).SetMultipliedLeading(lineSpacingFactor));
                }

                document.Add(new Paragraph($"Área administrativa requirente: {request.DireccionesDescripcion}").SetFontSize(11).SetMultipliedLeading(lineSpacingFactor));
                document.Add(new Paragraph($"Solicitante: {request.FirmaEmpleado}").SetFontSize(11).SetMultipliedLeading(lineSpacingFactor));

                document.Add(new Paragraph("Descripción:").SetFontSize(11).SetMultipliedLeading(lineSpacingFactor));
                document.Add(new Paragraph(request.Descripcion).SetFontSize(11).SetMultipliedLeading(lineSpacingFactor2));

                var descripcion = request.Descripcion ?? "";
                int charCount = descripcion.Length;


                if (charCount > 4000)
                {
                    // Agregar nueva página
                    document.Add(new AreaBreak()); // Add a new page


                }





                // If there is an image, add it to the PDF
                if (!string.IsNullOrEmpty(request.Archivo))
                {

                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", request.Archivo.TrimStart('/'));

                    var img = new Image(ImageDataFactory.Create(imagePath));

                    img.ScaleToFit(400, 500); // Adjust size as needed

                    if (System.IO.File.Exists(imagePath) )
                    {
                        img.SetHorizontalAlignment(HorizontalAlignment.CENTER);

                        var validation = false;


                        if (charCount < 400 && img.GetImageHeight() >= 500)
                        {
                            // Agregar nueva página
                            validation = true;
                            document.Add(img); // Add image to the document

                             // document.Add(new Paragraph("1"));

                        }

                        if (charCount < 3000 && img.GetImageHeight() < 280 && validation == false)
                        {
                            // Agregar nueva página
                           // document.Add(new AreaBreak()); // Add a new page
                            validation = true;
                            document.Add(img); // Add image to the document

                           // document.Add(new Paragraph("1"));
                           //hola

                        }

                        if (charCount > 3700 && validation == false )
                        {
                            // Agregar nueva página
                            document.Add(new AreaBreak()); // Add a new page
                            validation = true;
                            document.Add(img); // Add image to the document
                           // document.Add(new Paragraph("1"));


                        }

                        if (charCount < 3600 && img.GetImageHeight() < 280 && validation == false)
                        {
                            document.Add(img); // Add image to the document

                            // Agregar nueva página
                            document.Add(new AreaBreak()); // Add a new page
                            validation = true;

                            document.Add(new Paragraph("2"));


                        }


                        if (charCount > 1200 && charCount < 2000 && img.GetImageHeight() < 300 && validation == false)
                        {

                            document.Add(img); // Add image to the document

                            // Agregar nueva página
                            document.Add(new AreaBreak()); // Add a new page
                            validation = true;
                           document.Add(new Paragraph("3"));


                        }

                        if  (charCount < 1200 && charCount > 200 && img.GetImageHeight() > 500 && validation == false)
                        {
                            // Agregar nueva página
                            document.Add(img); // Add image to the document

                            document.Add(new AreaBreak()); // Add a new page
                            validation = true;
                            document.Add(new Paragraph("4"));


                        }
                        if (charCount < 1200 && img.GetImageHeight() < 500 && validation == false)
                        {
                            // Agregar nueva página
                            document.Add(img); // Add image to the document

                            //document.Add(new AreaBreak()); // Add a new page
                            validation = true;

                           // document.Add(new Paragraph("5"));

                        }

                        if (charCount < 200  && validation == false)
                        {
                            // Agregar nueva página
                            document.Add(img); // Add image to the document

                            validation = true;

                            document.Add(new Paragraph("6"));


                        }
                    }
                    else
                    {
                    }
                }
                else
                {
                }

                // Add signatures table
                Table table = new Table(3).UseAllAvailableWidth();

                // Add headers
                table.AddCell(new Cell().Add(new Paragraph("Solicitante")).SetFontSize(10));
                table.AddCell(new Cell().Add(new Paragraph("Autorizo unidad adm solicitante")).SetFontSize(10));
                table.AddCell(new Cell().Add(new Paragraph("Acepta infraestructura y tecnologias de la informacion")).SetFontSize(10));

                // Add first row with signature names
                table.AddCell(new Cell().Add(new Paragraph(request.FirmaEmpleado)).SetFontSize(10));
                table.AddCell(new Cell().Add(new Paragraph(request.FirmaJefeDepartamento ?? "N/A")).SetFontSize(10));
                table.AddCell(new Cell().Add(new Paragraph(request.FirmaJefe ?? "N/A")).SetFontSize(10));

                // Add second row with additional names or titles (if applicable)
                table.AddCell(new Cell().Add(new Paragraph("Nombre")).SetFontSize(10));
                table.AddCell(new Cell().Add(new Paragraph("Nombre")).SetFontSize(10));
                table.AddCell(new Cell().Add(new Paragraph("JOEL ADRIAN ACUÑA ALCARAZ")).SetFontSize(10));

                // Add the table to the document
                // Calculate the position for the table at the bottom
                float bottomPosition = 40; // Set a desired margin from the bottom (adjust as necessary)
                float tableHeight = 100;   // Set approximate height of the table (this can be calculated dynamically if necessary)


                // Draw the table at the specified bottom position
                table.SetFixedPosition(20, bottomPosition, 550); // Adjust X, Y, and width values as needed

                var totalPages = pdf.GetNumberOfPages();
                for (int i = 1; i <= totalPages; i++)
                {
                    // Set up the canvas for adding page numbers
                    var page = pdf.GetPage(i);
                    var canvas = new iText.Kernel.Pdf.Canvas.PdfCanvas(page);
                    var pageSize = page.GetPageSize();
                    canvas.BeginText()
                        .SetFontAndSize(iText.Kernel.Font.PdfFontFactory.CreateFont(), 12)
                        .MoveText(pageSize.GetWidth() - 100, 20) // Position the page number at the bottom right
                        .ShowText($"Pagina {i} de {totalPages}")
                        .EndText();
                }

                // Add the table to the document
                document.Add(table);
                // Close the document
                document.Close();

                // Return the PDF as a file
                var pdfBytes = memoryStream.ToArray();
                return File(pdfBytes, "application/pdf", $"Solicitud_{id}.pdf");
            }
        }

        // GET: api/Request/{id}
        // GET: api/Request/{id}
        [HttpGet("listbyNomEmpleadoIdWelcome/{EmpleadoID}")]

        public async Task<ActionResult<SS_Solicitud>> GetRequestByNomEmpleadosWelcome(int EmpleadoID)
        {
            var request = await _userContext.SS_Solicitudes
             .Include(r => r.SS_Servicio_Solicitados)
             .Where(r => r.EmpleadoID == EmpleadoID) // Retrieve the specific request by its ID
             .ToListAsync();
            if (request == null)
            {
                return NotFound();
            }

            return Ok(request); // Return the request with related entities
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(int id, [FromBody] UpdateRequestDto updateRequestDto)
        {
            _logger.LogInformation("Received PUT request for id: {Id} with data: {@UpdateRequestDto}", id, updateRequestDto);

            if (id <= 0 || updateRequestDto == null)
            {
                return BadRequest("Datos de la solicitud inválidos.");
            }

            // Buscar la solicitud existente
            var existingRequest = await _userContext.SS_Solicitudes.FindAsync(id);
            if (existingRequest == null)
            {
                return NotFound("Solicitud no encontrada.");
            }

            // Solo actualizar los campos que el usuario envía (que no son nulos)
            if (updateRequestDto.ServicioSolicitado != null)
            {
                existingRequest.ServicioSolicitado = updateRequestDto.ServicioSolicitado;
            }
            if (updateRequestDto.Solicitud_de_servicio_id != null)
            {
                existingRequest.SS_Solicitud_de_servicio_id = updateRequestDto.Solicitud_de_servicio_id;
            }
            if (updateRequestDto.FechaSolicitada.HasValue)
            {
                existingRequest.FechaSolicitada = updateRequestDto.FechaSolicitada.Value;
            }


            if (updateRequestDto.Descripcion != null)
            {
                existingRequest.Descripcion = updateRequestDto.Descripcion;
            }

            if (updateRequestDto.Estatus != null)
            {
                existingRequest.Estatus = updateRequestDto.Estatus;
            }
            if (updateRequestDto.FirmaJefeDepartamento != null)
            {
                existingRequest.FirmaJefeDepartamento = updateRequestDto.FirmaJefeDepartamento;
            }
            if (updateRequestDto.FirmaEmpleado != null)
            {
                existingRequest.FirmaEmpleado = updateRequestDto.FirmaEmpleado;
            }
            if (updateRequestDto.FirmaJefe != null)
            {
                existingRequest.FirmaJefe = updateRequestDto.FirmaJefe;
            }

            if (updateRequestDto.Prioridad != null)
            {
                existingRequest.Prioridad = updateRequestDto.Prioridad;
            }

            if (updateRequestDto.RevisadoSub.HasValue)
            {
                existingRequest.RevisadoSub = updateRequestDto.RevisadoSub.Value;
            }
            if (updateRequestDto.UltimoStatus.HasValue)
            {
                existingRequest.UltimoStatus = updateRequestDto.UltimoStatus.Value;
            }


            // Guardar los cambios en la base de datos
            try
            {
                await _userContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
                {
                    return NotFound("Solicitud no encontrada.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        
       /* [HttpPut("{userId}/requests/{requestId}")]
        public async Task<IActionResult> UpdateRequest(int userId, int requestId, [FromBody] UpdateRequestDto updateRequestDto)
        {
            // Buscar al usuario por su userId
            var user = await _userContextPlasaibcess.NomEmpleados
                                         .Include(u => u.SS_Solicitudes)
                                         .FirstOrDefaultAsync(u => u.EmpleadoID == userId);

            if (user == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            // Buscar la solicitud específica por su requestId dentro de la lista de requests del usuario
            var request = user.SS_Solicitudes.FirstOrDefault(r => r.SS_SolicitudId == requestId);

            if (request == null)
            {
                return NotFound("Solicitud no encontrada.");
            }

            // Actualizar los campos de la solicitud
            if (updateRequestDto.ServicioSolicitado.HasValue)
            {
                request.ServicioSolicitado = updateRequestDto.ServicioSolicitado.Value;
            }

            if (updateRequestDto.FechaSolicitada.HasValue)
            {
                request.FechaSolicitada = updateRequestDto.FechaSolicitada.Value;
            }



            if (!string.IsNullOrEmpty(updateRequestDto.FirmaEmpleado))
            {
                request.FirmaEmpleado = updateRequestDto.FirmaEmpleado;
            }


            if (updateRequestDto.RevisadoSub.HasValue)
            {
                request.RevisadoSub = updateRequestDto.RevisadoSub.Value;
            }

            if (updateRequestDto.UltimoStatus.HasValue)
            {
                request.UltimoStatus = updateRequestDto.UltimoStatus.Value;
            }

            // Guardar los cambios en la base de datos
            try
            {
                await _userContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(requestId))
                {
                    return NotFound("Solicitud no encontrada.");
                }
                else
                {
                    throw;//hola q tal 
                }
            }

            return Ok("Solicitud actualizada exitosamente.");
        }
       */
        [HttpPost]
        public async Task<ActionResult<SS_Solicitud>> PostRequest([FromForm] CreateRequestDto createRequestDto, IFormFile? archivo)
        {
            if (_userContext.SS_Solicitudes == null)
            {
                return Problem("Entity set 'UserContext.Requests' is null.");
            }

            // Validate the incoming model
            if (!ModelState.IsValid)
            {
                foreach (var modelError in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(modelError.ErrorMessage);  // Logs validation errors
                }
                return BadRequest(ModelState); // Return validation errors
            }

            // Ensure the employee exists
            // Ensure the user exists and load NomEmpleados with eager loading
            //  var user = await _userContextPlasaibcess.NomEmpleados
            //    .FirstOrDefaultAsync(n => n.EmpleadoID == createRequestDto.EmpleadoID); 

            // if (user == null)
            // {
            //   return BadRequest("Invalid UserId.");
            // }

            // Verify if ConActivosFijosId has a value
            //  ConActivosFijos? conActivosFijos = null;
            //  if (createRequestDto.ActivoFijoID.HasValue)
            //  {
            //    conActivosFijos = await _userContextPlasaibcess.ConActivosFijos.FindAsync(createRequestDto.ActivoFijoID);
            //    if (conActivosFijos == null)
            //  {
            //    return BadRequest("Invalid ActivosFijosId.");
            //}
            // }


            var nomEmpleados = await _userContext.SS_ListEmpleados
              .FromSqlInterpolated($"EXEC sp_ShowEmpleado @empleadoID = {createRequestDto.EmpleadoID}")
              .ToListAsync();

            var empleado = nomEmpleados.First();


            if (nomEmpleados == null)
            {
                return NotFound("No empleados found");
            }

            // Handle file upload if file is provided
            string? filePath = null;
            if (archivo != null && archivo.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(archivo.FileName);
                filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await archivo.CopyToAsync(fileStream);
                }

                filePath = "/uploads/" + uniqueFileName;

            }

            // Map the DTO to the Request entity
            var request = new SS_Solicitud
            {
                EmpleadoID = createRequestDto.EmpleadoID,
                ServicioSolicitado = createRequestDto.ServicioSolicitado,
                SS_Solicitud_de_servicio_id = createRequestDto.SS_Solicitud_de_servicio_id,
                FechaSolicitada = createRequestDto.FechaSolicitada,
                Descripcion = createRequestDto.Descripcion,
                Estatus = createRequestDto.Estatus,
                FirmaEmpleado = $"{empleado.NomEmpNombre} {empleado.NomEmpPaterno} {empleado.NomEmpMaterno}",
                FirmaJefeDepartamento = createRequestDto.FirmaJefeDepartamento,
                FirmaJefe = createRequestDto.FirmaJefe,
                ActivoFijoID = createRequestDto.ActivoFijoID,
                Prioridad = createRequestDto.Prioridad,
                Archivo = filePath, // Can be null if no file is uploaded
                SS_Servicio_solicitado_Id = createRequestDto.SS_Servicio_solicitado_Id,
                RevisadoSub= createRequestDto.RevisadoSub,
            };


            // Create and add the associated Request
            var HistorialStatus = new SS_HistorialStatus
            {
                Status = 1,
                FechaStatus = DateTime.Now,
                Quien = "Solicitante",
                SS_SolicitudId = request.SS_SolicitudId
             };

            // Add the request to the user's Requests collection
           request.HistorialStatus = new List<SS_HistorialStatus> { HistorialStatus};

            // Add the new user to the context

            _userContext.SS_Solicitudes.Add(request);
            await _userContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRequest), new { id = request.SS_SolicitudId }, request);
        }

       

        [HttpDelete("{userId}/requests/{requestId}")]
        public async Task<IActionResult> DeleteRequest(int userId, int UserIdReceiver)
        {
            // Buscar al usuario por su userId
            var user = await _userContextPlasaibcess.NomEmpleados
                                         //.Include(u => u.SS_Solicitudes)
                                         .FirstOrDefaultAsync(u => u.EmpleadoID == userId);

            if (user == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            // Buscar la solicitud específica por su useridreceiver dentro de la lista de requests del usuario
           // var request = user.SS_Solicitudes.FirstOrDefault(r => r.SS_SolicitudId == UserIdReceiver);

           // if (request == null)
           // {
                return NotFound("Solicitud no encontrada.");
          //  }

            // Remover la solicitud de la lista de requests del usuario
            //user.SS_Solicitudes.Remove(request);

            // Guardar los cambios en la base de datos
            try
            {
                await _userContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Ocurrió un error al intentar eliminar la solicitud.");
            }

            return Ok("Solicitud eliminada exitosamente.");
        }

        private bool RequestExists(int id)
        {
            return _userContext.SS_Solicitudes.Any(e => e.SS_SolicitudId == id);
        }

    }

    public class SS_solicitudDetails
    {
        public int SS_SolicitudId { get; set; }
        public string FirmaEmpleado { get; set; }
        public string? FirmaJefeDepartamento { get; set; }
        public string? FirmaJefe { get; set; }

        public string DescripcionServicio_Solicitado { get; set; }
        public string Descripcion { get; set; }
        //public string? DescripcionSolicitud_De_Servicio { get; set; }
        public DateTime FechaSolicitada { get; set; }
        public bool? RevisadoSub { get; set; }
        public int? Estatus { get; set; }
        public int? Prioridad { get; set; }
        public int? UltimoStatus { get; set; }
        public string? Archivo { get; set; }
        public string NomEmpNombre { get; set; }
        public string NomEmpPaterno { get; set; }
        public string NomEmpMaterno { get; set; }
        public string DireccionesDescripcion { get; set; }
    }

    public class SS_ConActivosFijosSP
    {
        [Key]
        public int ActivoFijoID { get; set; }

        [MaxLength(10)]
        public string AFClave { get; set; }

        [MaxLength(50)]
        public string AFNombre { get; set; }

        [MaxLength(100)]
        public string AFDescripcion { get; set; }
    }

    public class SS_showSolicitudes
    {
        public int SS_SolicitudId { get; set; }
        public string FirmaEmpleado { get; set; }
        public string DescripcionServicio_Solicitado { get; set; }
        public string Descripcion { get; set; }
        public string? DescripcionSolicitud_De_Servicio { get; set; }
        public int SS_Servicio_solicitado_Id { get; set; }
        public DateTime FechaSolicitada { get; set; }
        public bool? RevisadoSub { get; set; }
        public int? Estatus { get; set; }
        public int? Prioridad { get; set; }
        public int? UltimoStatus { get; set; }
        public string? Archivo { get; set; }
        public string? FirmaJefeDepartamento { get; set; }
        public string? FirmaJefe { get; set; }
        public string NomEmpNombre { get; set; }
        public string NomEmpPaterno { get; set; }
        public string NomEmpMaterno { get; set; }
        public string? AFClave { get; set; }
        public string? AFDescripcion { get; set; }
        public string DireccionesDescripcion { get; set; }
        public string? Email { get; set; }


    }



    public class CreateRequestDto
    {

        public int EmpleadoID { get; set; }
        public int? ActivoFijoID { get; set; }
        public int? SS_Solicitud_de_servicio_id { get; set; }
        public int? ServicioSolicitado { get; set; }
        public DateTime? FechaSolicitada { get; set; }
        public string? AreaAdministrativaRequirente { get; set; }
        public string? PersonaQueSolicitaElServicio { get; set; }
        public string? Descripcion { get; set; }
        public string? PuestoSolicitante { get; set; }
        public string? PuestoAutorizante { get; set; }
        public string? FirmaEmpleado { get; set; }
        public string? FirmaJefeDepartamento { get; set; }
        public string? FirmaJefe { get; set; }
        public int? Estatus { get; set; }
        public string? Comentarios { get; set; }

        public int? Prioridad { get; set; }

        public string? Archivo { get; set; }

        public bool? RevisadoSub { get; set; }

        public int? SS_Servicio_solicitado_Id { get; set; }
    }

    public class UpdateRequestDto
    {
        public int UserId { get; set; }
        public int? ServicioSolicitado { get; set; }
        public int? Solicitud_de_servicio_id { get; set; }
        public DateTime? FechaSolicitada { get; set; }
        public string? AreaAdministrativaRequirente { get; set; }
        public string? PersonaQueSolicitaElServicio { get; set; }
        public string? Descripcion { get; set; }
        public string? PuestoSolicitante { get; set; }
        public string? PuestoAutorizante { get; set; }
        public int? Estatus {get; set; }
        public string? FirmaJefeDepartamento { get; set; }
        public string? FirmaEmpleado { get; set; }
        public int? Prioridad { get; set; }

        public int? UltimoStatus { get; set; }

        public bool? RevisadoSub { get; set; }

        public string? FirmaJefe { get; set; }


    }

    public class RequestUpdateDto
    {
        public int? UserIdReceiver { get; set; }
        public int? UserIdSend { get; set; }
        public bool? Match { get; set; }//no c q
    }
}