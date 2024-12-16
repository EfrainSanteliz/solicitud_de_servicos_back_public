using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SocialMediaApi.Controllers;
using solicitud_de_servicios_back.Models;
using System.Globalization;




namespace solicitud_de_servicios_back.Controllers
{

    [Route("api/direccionesICESS")]
    [ApiController]
    public class DireccionesICESSController : ControllerBase
    {
        private readonly UserContextPlasalcees _userContextPlasaibcess;
        private readonly ILogger<RequestController> _logger;

        public DireccionesICESSController(UserContextPlasalcees userContextPlasaibcess, ILogger<RequestController> logger)
        {
            _userContextPlasaibcess = userContextPlasaibcess;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DireccionesICEES>>> GetDireccionesICEES()
        {
            var users = await _userContextPlasaibcess.DireccionesICEES
                .ToListAsync();

            return Ok(users);
        }

    }
    

}
