using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using solicitud_de_servicios_back.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace solicitud_de_servicios_back.Controllers
{
    [Route("api/Historial")]
    [ApiController]
    public class HistorialController : ControllerBase
    {
        private readonly UserContext _userContext;

        public HistorialController(UserContext userContext)
        {
            _userContext = userContext;
        }

        // GET: api/Historial
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SS_HistorialComentarios>>> GetHistorials()
        {
            return await _userContext.SS_HistorialComentarios
                .Include(h => h.SS_Solicitudes)  // Include related Request
                .ToListAsync();
        }

        // GET: api/Historial/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SS_HistorialComentarios>> GetHistorial(int id)
        {
            var request = await _userContext.SS_HistorialComentarios
              .Where(r => r.SS_SolicitudId == id) // Retrieve the specific request by its ID
              .ToListAsync();
            if (request == null)
            {
                return NotFound();
            }

            return Ok(request); // Return the request with related entities
        }

      


        // POST: api/Historial
        [HttpPost] //metodo para registrar un comentario en la tabla de historialComentarios
        public async Task<ActionResult<SS_HistorialComentarios>> PostHistorial(SS_HistorialComentarios HistorialComentarios)
        {
            var request = await _userContext.SS_Solicitudes.FindAsync(HistorialComentarios.SS_SolicitudId);
            if (request == null)
            {
                return BadRequest("Invalid RequestID");
            }

            _userContext.SS_HistorialComentarios.Add(HistorialComentarios);
            await _userContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHistorial), new { id = HistorialComentarios.SS_HistorialComentariosID }, HistorialComentarios);
        }

        // PUT: api/Historial/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistorial(int id, Historial historial)
        {
            if (id != historial.HistorialID)
            {
                return BadRequest();
            }

            _userContext.Entry(historial).State = EntityState.Modified;

            try
            {
                await _userContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistorialExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Historial/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistorial(int id)
        {
            var historial = await _userContext.SS_HistorialComentarios.FindAsync(id);
            if (historial == null)
            {
                return NotFound();
            }

            _userContext.SS_HistorialComentarios.Remove(historial);
            await _userContext.SaveChangesAsync();

            return NoContent();
        }

        private bool HistorialExists(int id)
        {
            return _userContext.SS_HistorialComentarios.Any(e => e.SS_HistorialComentariosID == id);
        }
    }
}