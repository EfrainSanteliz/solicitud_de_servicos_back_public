
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaApi.Controllers;
using solicitud_de_servicios_back.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace solicitud_de_servicios_back.Controllers
{
    [Route("api/HistorialStatus")]
    [ApiController]
    public class HistorialStatusController : ControllerBase
    {
        private readonly UserContext _userContext;

        private readonly ILogger<RequestController> _logger;

        public HistorialStatusController(UserContext userContext, ILogger<RequestController> logger)
        {
            _userContext = userContext;
            _logger = logger;

        }

        // GET: api/Historial

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SS_HistorialStatus>>> GetUsers()
        {
            var HistorialStatus = await _userContext.SS_HistorialStatus
                .ToListAsync();

            return Ok(HistorialStatus);
        }

        // GET: api/Historial/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SS_HistorialStatus>> GetHistorialStatus(int id)
        {
            var request = await _userContext.SS_HistorialStatus
             .Where(r => r.SS_SolicitudId == id) // Retrieve the specific request by its ID
             .ToListAsync();
            if (request == null)
            {
                return NotFound();
            }

            return Ok(request); // Return the request with related entities
        }

        [HttpGet("statusYprioridad/{id}")]
        public async Task<IActionResult> GetHistorial(int id)
        {
            var statusRequest = await _userContext.SS_HistorialStatus
                 .Where(r => r.SS_SolicitudId == id)
                 .ToListAsync();

            var prioridadRequest = await _userContext.SS_HistorialPrioridads
                .Where(r => r.SS_SolicitudId == id)
                .ToListAsync();

            // Log data counts for debugging
            _logger.LogInformation("Status Request Count: {Count}", statusRequest.Count);
            _logger.LogInformation("Prioridad Request Count: {Count}", prioridadRequest.Count);

            if (statusRequest.Count == 0 && prioridadRequest.Count == 0)
            {
                return NotFound();
            }

            return Ok(new
            {
                Status = statusRequest,
                Prioridad = prioridadRequest
            });
        }

        // POST: api/Historial
        [HttpPost]
        public async Task<ActionResult<SS_HistorialStatus>> PostHistorialStatus(SS_HistorialStatus HistorialStatus)
        {
            var request = await _userContext.SS_Solicitudes.FindAsync(HistorialStatus.SS_SolicitudId);
            if (request == null)
            {
                return BadRequest("Invalid RequestID");
            }

            _userContext.SS_HistorialStatus.Add(HistorialStatus);
            await _userContext.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Historial
        

       

        public class UpdateStatusDto
        {
            public string? Quien { get; set; }
            public int? Status { get; set; }

            public DateTime? FechaStatus { get; set; }

        }

        private bool HistorialExists(int id)
        {
            return _userContext.SS_HistorialComentarios.Any(e => e.SS_HistorialComentariosID == id);
        }
    }
}