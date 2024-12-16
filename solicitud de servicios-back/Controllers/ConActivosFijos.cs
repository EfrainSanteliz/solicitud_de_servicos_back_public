using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using solicitud_de_servicios_back.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace solicitud_de_servicios_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConActivosFijosController : ControllerBase
    {
        //private readonly UserContext _userContext;
        private readonly UserContextPlasalcees _userContextPlasaibcess;

        private readonly UserContext _userContext;

        private readonly ILogger<ConActivosFijosController> _logger;

        public ConActivosFijosController(UserContextPlasalcees userContextPlasaibcess, UserContext userContext , ILogger<ConActivosFijosController> logger)
        {
            //_userContext = userContext;
            _userContextPlasaibcess = userContextPlasaibcess;
            _logger = logger;
            _userContext = userContext;
        }

        // GET: api/Request  metodo para listar todo el intentario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConActivosFijos>>> GetRequest()
        {
         
            var activosFijos = await _userContext.SS_ConActivosFijosSP
          .FromSqlInterpolated($"EXEC sp_listConActivosFijos")
          .ToListAsync();

            if (activosFijos == null || activosFijos.Count == 0)
            {
                return NotFound(new { message = "solicitud not found" });
            }

            return Ok(new
            {
                activosFijos
            });

        }


    }

    public class CreateInvetarioDto
    {
        public int NomEmpleadosId { get; set; }
        public string? ServicioSolicitado { get; set; }
        public DateTime? FechaSolicitada { get; set; }
        public string? AreaAdministrativaRequirente { get; set; }
        public string? PersonaQueSolicitaElServicio { get; set; }
        public string? Descripcion { get; set; }
        public string? PuestoSolicitante { get; set; }
        public string? PuestoAutorizante { get; set; }
        public string? FirmaEmpleado { get; set; }
        public string? FirmaJefeDepartamento { get; set; }
        public string? FirmaJefe { get; set; }
    }

    public class UpdateConActivosFijosDto
    {
        public int UserId { get; set; }
        public string? ServicioSolicitado { get; set; }
        public DateTime? FechaSolicitada { get; set; }
        public string? AreaAdministrativaRequirente { get; set; }
        public string? PersonaQueSolicitaElServicio { get; set; }
        public string? Descripcion { get; set; }
        public string? PuestoSolicitante { get; set; }
        public string? PuestoAutorizante { get; set; }
        public string? Firma { get; set; }


    }

    public class InvetarioUpdateDto
    {
        public int? UserIdReceiver { get; set; }
        public int? UserIdSend { get; set; }
        public bool? Match { get; set; }
    }
}