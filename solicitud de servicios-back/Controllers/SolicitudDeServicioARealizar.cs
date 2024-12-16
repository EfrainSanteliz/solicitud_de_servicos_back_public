using iText.Kernel.Pdf;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg.OpenPgp;
using solicitud_de_servicios_back.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace solicitud_de_servicios_back.Controllers
{
    [Route("api/Solicitud_de_servicio")]
    [ApiController]
    public class SolicitudDeServicioARealizar : ControllerBase
    {
        private readonly UserContext _userContext;
        private readonly ILogger<SolicitudDeServicioARealizar> _logger;

        public SolicitudDeServicioARealizar(UserContext userContext, ILogger<SolicitudDeServicioARealizar> logger)
        {
            _userContext = userContext;
            _logger = logger;
        }

        // GET: api/Request metodo para listar a todas las solicituides de servicios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SS_Solicitud_de_servicio>>> GetRequest()
        {
            if (_userContext.SS_Solicitud_De_Servicios == null)
            {
                return NotFound();
            }

            return await _userContext.SS_Solicitud_De_Servicios.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<SS_Solicitud_de_servicio>> GetRequestById(int id)
        {
            var user = await _userContext.SS_Solicitud_De_Servicios
            .Include(s => s.SS_Solicitudes)                      
            .FirstOrDefaultAsync(u => u.SS_Solicitud_de_servicio_id == id);


            if (user == null)
            {
                return NotFound();
            }

            return user;
        }


        [HttpPost]

        public async Task<ActionResult<SS_Solicitud_de_servicio>> CreateRequest(SS_Solicitud_de_servicio solicitud)
        {
            if (_userContext.SS_Solicitud_De_Servicios == null)
            {
                return Problem("Entity set 'UserContext.Solicitud_De_Servicios' is null.");
            }

            _userContext.SS_Solicitud_De_Servicios.Add(solicitud);
            await _userContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRequestById), new { id = solicitud.SS_Solicitud_de_servicio_id }, solicitud);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSolicitud(int id, [FromBody] SS_Solicitud_de_servicio solicitudUpdate)
        {
            if (id <= 0 || solicitudUpdate == null)
            {
                return BadRequest("Invalid request data.");
            }

            // Find the existing solicitud record
            var existingSolicitud = await _userContext.SS_Solicitud_De_Servicios.FindAsync(id);
            if (existingSolicitud == null)
            {
                return NotFound("Solicitud not found.");
            }

            // Update fields if they are provided
            if (solicitudUpdate.DescripcionSolicitud_De_Servicio != null)
            {
                existingSolicitud.DescripcionSolicitud_De_Servicio = solicitudUpdate.DescripcionSolicitud_De_Servicio;
            }

            if (solicitudUpdate.Habilitado.HasValue)
            {
                existingSolicitud.Habilitado = solicitudUpdate.Habilitado;
            }

            // Save changes
            _userContext.Entry(existingSolicitud).State = EntityState.Modified;

            try
            {
                await _userContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Error updating solicitud");
            }

            return NoContent();
        }

        // DELETE: api/Solicitud_de_servicio/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            var request = await _userContext.SS_Solicitud_De_Servicios
           .Include(s => s.SS_Solicitudes)
           .FirstOrDefaultAsync(u => u.SS_Solicitud_de_servicio_id == id);



            if (request == null)
            {
                return NotFound();
            }

            if (request.SS_Solicitudes != null && request.SS_Solicitudes.Any())
            {
                var associatedRequestIds = request.SS_Solicitudes.Select(r => r.SS_SolicitudId).ToList();
                var idsString = string.Join(",", associatedRequestIds);


                return BadRequest($"No puedes eliminar este servicio por que tiene relacion con las solicitudes de id : {idsString}"); // Return a message with the IDs
            }


            _userContext.SS_Solicitud_De_Servicios.Remove(request);
            await _userContext.SaveChangesAsync();

            return NoContent();
        }

        private bool RequestExists(int id)
        {
            return _userContext.SS_Solicitud_De_Servicios.Any(e => e.SS_Solicitud_de_servicio_id == id);
        }

        public  class UpdateRequestDto {

            public string? Descripcion { get; set; }

            public bool? Habilitado { get; set; }
            
         }


    }


}