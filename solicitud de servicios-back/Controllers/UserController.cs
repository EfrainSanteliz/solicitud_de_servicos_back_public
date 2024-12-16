using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SocialMediaApi.Controllers;
using solicitud_de_servicios_back.Models;
using solicitud_de_servicios_back.Services;
using System.Globalization;




namespace solicitud_de_servicios_back.Controllers
{
    /*
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext _userContext;
        private readonly ILogger<RequestController> _logger;
        private readonly JwtTokenService _jwtTokenService;


        public UserController(JwtTokenService jwtTokenService,UserContext userContext , ILogger<RequestController> logger)
        {
            _userContext = userContext;
            _logger = logger;
            _jwtTokenService = jwtTokenService;

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NomEmpleados>>> GetUsers()
        {
            var users = await _userContext.NomEmpleados
                .Include(n => n.DireccionesICEES)                      // Incluye los requests
                .Include(n => n.Requests)                      // Incluye los requests
                    .ThenInclude(r => r.ConActivosFijos)       // Incluye los activos fijos relacionados a cada request
                .Include(n => n.Requests)                      // Incluye nuevamente los requests
                    .ThenInclude(r => r.Historials)            // Incluye los historiales relacionados a cada request
                .ToListAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NomEmpleados>> GetUser(int id)
        {
            var user = await _userContext.NomEmpleados
            .Include(n => n.DireccionesICEES)                      // Incluye los requests
            .Include(u => u.Requests)
            .ThenInclude(r => r.ConActivosFijos)       // Incluye los activos fijos relacionados a cada request
                .Include(n => n.Requests)                      // Incluye nuevamente los requests
                    .ThenInclude(r => r.Historials)
            .FirstOrDefaultAsync(u => u.EmpleadoID == id);

            if (user == null)
            {
                return NotFound();
            }

            return user;

        }

        [HttpPost("create")]
        public async Task<ActionResult<NomEmpleados>> PostUser(CreateUser createUser)
        {
            // Create a new instance of NomEmpleados with all necessary fields
            var user = new NomEmpleados
            {
                Email = createUser.Email,
                Password = createUser.Password,
                NomEmpClave = createUser.NomEmpClave,
                NomEmpNombre = createUser.NomEmpNombre,
                NomEmpPaterno = createUser.NomEmpPaterno,
                NomEmpMaterno = createUser.NomEmpMaterno,
                NomEmpSexo = createUser.NomEmpSexo,
                NomEmpEdoCivil = createUser.NomEmpEdoCivil,
                NomEmpTieneHijos = createUser.NomEmpTieneHijos,
                NomEmpFechaNacimiento = createUser.NomEmpFechaNacimiento,
                NomEmpRFC = createUser.NomEmpRFC,
                NomEmpCURP = createUser.NomEmpCURP,
                NomEmpDatosDireccionID = createUser.NomEmpDatosDireccionID,
                NomEmpTipoNomina = createUser.NomEmpTipoNomina,
                NomEmpFechaIngreso = createUser.NomEmpFechaIngreso,
                NomEmpFechaTerminacion = createUser.NomEmpFechaTerminacion,
                NomEmpAdscripcionID = createUser.NomEmpAdscripcionID,
                NomEmpPuestoID = createUser.NomEmpPuestoID,
                NomEmpTabuladorID = createUser.NomEmpTabuladorID,
                NomEmpTabuladorSubNivelClave = createUser.NomEmpTabuladorSubNivelClave,
                NomEmpNivelEstudioID = createUser.NomEmpNivelEstudioID,
                NomEmpFechaAltaIsssteson = createUser.NomEmpFechaAltaIsssteson,
                NomEmpNumeroPension = createUser.NomEmpNumeroPension,
                NomEmpNoCuentaBancaria = createUser.NomEmpNoCuentaBancaria,
                NomEmpFoto = createUser.NomEmpFoto,
                SucursalId = createUser.SucursalId,
                Carrera = createUser.Carrera,
                TipoSangre = createUser.TipoSangre,
                NumLicenciaConducir = createUser.NumLicenciaConducir,
                FechaVencimLicencia = createUser.FechaVencimLicencia,
                DeclaracionPatrimonial = createUser.DeclaracionPatrimonial,
                Antiguedad = createUser.Antiguedad,
                ClaveSindical = createUser.ClaveSindical,
                BancoId = createUser.BancoId,
                NumEmpCuentaClave = createUser.NumEmpCuentaClave,
                NomEmpEstatus = createUser.NomEmpEstatus,
                UserID = createUser.UserID,
                DepartamentoID = createUser.DepartamentoID,
                RecursoID = createUser.RecursoID,
                NomEmpGradoDominio = createUser.NomEmpGradoDominio,
                NomEmpTipoAnalisis = createUser.NomEmpTipoAnalisis,
                NomEmpMarco = createUser.NomEmpMarco,
                EstaActivo = createUser.EstaActivo,
                EnviarCorreoFalta = createUser.EnviarCorreoFalta,
                NomEmpClaveRHGE = createUser.NomEmpClaveRHGE,
                AfiliacionIsssteson = createUser.AfiliacionIsssteson,
                CatPuestoID = createUser.CatPuestoID,
                NomEmpIngresoGobEdo = createUser.NomEmpIngresoGobEdo,
                NSS = createUser.NSS,
                NumeroFonacot = createUser.NumeroFonacot,
                NomEmpClaveAntesRHGE = createUser.NomEmpClaveAntesRHGE,
                RequiereLicencia = createUser.RequiereLicencia,
                ActividadPresupuestal = createUser.ActividadPresupuestal,
                UserRole = createUser.UserRole
            };

            // Create and add the associated Request
            //var request = new Request
            //{
              //  ServicioSolicitado = "0",
                //FechaSolicitada = DateTime.Now,
               // Status =0,
               // Descripcion = "0",
              //  FirmaEmpleado = "0",
              //  FirmaJefeDepartamento = "0",
              //  FirmaJefe = "0",
              //  NomEmpleadosId = user.EmpleadoID
           // };

            // Add the request to the user's Requests collection
            //user.Requests = new List<Request> { request };

            // Add the new user to the context
            _userContext.NomEmpleados.Add(user);
            await _userContext.SaveChangesAsync();


            // Return the created user with associated requests
            return CreatedAtAction(nameof(GetUser), new { id = user.EmpleadoID }, user);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> LoginUser([FromBody] LoginUser loginUser)
        {
            var user = await _userContext.NomEmpleados
                .Include(n => n.DireccionesICEES)                  

                .Where(u => u.Email == loginUser.Email && u.Password == loginUser.Password)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            // Inject your token generation service (JwtTokenService) and generate a JWT token
            var token = _jwtTokenService.GenerateToken(user.EmpleadoID.ToString(), user.Email);

            // Return the token to the client
            return Ok(new
            {
                Token = token,
                User = user
            });
        }

        [HttpPut("{NomEmpRFC}")]
        public async Task<IActionResult> PutUser(string NomEmpRFC, [FromBody] CreateUser updateUser)
        {
            _logger.LogInformation("Received PUT request for NomEmpRFC: {NomEmpRFC} with data: {@UpdateUser}", NomEmpRFC, updateUser);
          
            if (string.IsNullOrEmpty(NomEmpRFC) || updateUser == null)
            {
                return BadRequest("Invalid request data.");
            }

            var existingUser = await _userContext.NomEmpleados.FirstOrDefaultAsync(u => u.NomEmpRFC == NomEmpRFC); if (existingUser == null)
            {
                return NotFound("User not found.");
            }

            // Update only non-null fields
            existingUser.NomEmpClave = string.IsNullOrEmpty(updateUser.NomEmpClave) ? existingUser.NomEmpClave : updateUser.NomEmpClave;
            existingUser.Email = string.IsNullOrEmpty(updateUser.Email) ? existingUser.Email : updateUser.Email;
            existingUser.Password = string.IsNullOrEmpty(updateUser.Password) ? existingUser.Password : updateUser.Password;

            existingUser.NomEmpNombre = string.IsNullOrEmpty(updateUser.NomEmpNombre) ? existingUser.NomEmpNombre : updateUser.NomEmpNombre;
            existingUser.NomEmpPaterno = string.IsNullOrEmpty(updateUser.NomEmpPaterno) ? existingUser.NomEmpPaterno : updateUser.NomEmpPaterno;
            existingUser.NomEmpMaterno = updateUser.NomEmpMaterno ?? existingUser.NomEmpMaterno;
            existingUser.NomEmpSexo = updateUser.NomEmpSexo == 0 ? existingUser.NomEmpSexo : updateUser.NomEmpSexo;
            existingUser.NomEmpEdoCivil = updateUser.NomEmpEdoCivil == 0 ? existingUser.NomEmpEdoCivil : updateUser.NomEmpEdoCivil;
            existingUser.NomEmpTieneHijos = updateUser.NomEmpTieneHijos;
            existingUser.NomEmpFechaNacimiento = updateUser.NomEmpFechaNacimiento != default ? updateUser.NomEmpFechaNacimiento : existingUser.NomEmpFechaNacimiento;
            existingUser.NomEmpRFC = string.IsNullOrEmpty(updateUser.NomEmpRFC) ? existingUser.NomEmpRFC : updateUser.NomEmpRFC;
            existingUser.NomEmpCURP = string.IsNullOrEmpty(updateUser.NomEmpCURP) ? existingUser.NomEmpCURP : updateUser.NomEmpCURP;
            existingUser.NomEmpDatosDireccionID = updateUser.NomEmpDatosDireccionID == 0 ? existingUser.NomEmpDatosDireccionID : updateUser.NomEmpDatosDireccionID;
            existingUser.NomEmpTipoNomina = updateUser.NomEmpTipoNomina == 0 ? existingUser.NomEmpTipoNomina : updateUser.NomEmpTipoNomina;
            existingUser.NomEmpFechaIngreso = updateUser.NomEmpFechaIngreso != default ? updateUser.NomEmpFechaIngreso : existingUser.NomEmpFechaIngreso;
            existingUser.NomEmpFechaTerminacion = updateUser.NomEmpFechaTerminacion ?? existingUser.NomEmpFechaTerminacion;
            existingUser.NomEmpAdscripcionID = updateUser.NomEmpAdscripcionID == 0 ? existingUser.NomEmpAdscripcionID : updateUser.NomEmpAdscripcionID;
            existingUser.NomEmpPuestoID = updateUser.NomEmpPuestoID == 0 ? existingUser.NomEmpPuestoID : updateUser.NomEmpPuestoID;
            existingUser.NomEmpTabuladorID = updateUser.NomEmpTabuladorID == 0 ? existingUser.NomEmpTabuladorID : updateUser.NomEmpTabuladorID;
            existingUser.NomEmpTabuladorSubNivelClave = updateUser.NomEmpTabuladorSubNivelClave == '\0' ? existingUser.NomEmpTabuladorSubNivelClave : updateUser.NomEmpTabuladorSubNivelClave;
            existingUser.NomEmpNivelEstudioID = updateUser.NomEmpNivelEstudioID == 0 ? existingUser.NomEmpNivelEstudioID : updateUser.NomEmpNivelEstudioID;
            existingUser.NomEmpFechaAltaIsssteson = updateUser.NomEmpFechaAltaIsssteson != default ? updateUser.NomEmpFechaAltaIsssteson : existingUser.NomEmpFechaAltaIsssteson;
            existingUser.NomEmpNumeroPension = string.IsNullOrEmpty(updateUser.NomEmpNumeroPension) ? existingUser.NomEmpNumeroPension : updateUser.NomEmpNumeroPension;
            existingUser.NomEmpNoCuentaBancaria = string.IsNullOrEmpty(updateUser.NomEmpNoCuentaBancaria) ? existingUser.NomEmpNoCuentaBancaria : updateUser.NomEmpNoCuentaBancaria;
            existingUser.NomEmpFoto = updateUser.NomEmpFoto ?? existingUser.NomEmpFoto;
            existingUser.SucursalId = updateUser.SucursalId == 0 ? existingUser.SucursalId : updateUser.SucursalId;
            existingUser.Carrera = updateUser.Carrera == 0 ? existingUser.Carrera : updateUser.Carrera;
            existingUser.TipoSangre = string.IsNullOrEmpty(updateUser.TipoSangre) ? existingUser.TipoSangre : updateUser.TipoSangre;
            existingUser.NumLicenciaConducir = string.IsNullOrEmpty(updateUser.NumLicenciaConducir) ? existingUser.NumLicenciaConducir : updateUser.NumLicenciaConducir;
            existingUser.FechaVencimLicencia = updateUser.FechaVencimLicencia ?? existingUser.FechaVencimLicencia;
            existingUser.DeclaracionPatrimonial = updateUser.DeclaracionPatrimonial ?? existingUser.DeclaracionPatrimonial;
            existingUser.Antiguedad = string.IsNullOrEmpty(updateUser.Antiguedad) ? existingUser.Antiguedad : updateUser.Antiguedad;
            existingUser.ClaveSindical = string.IsNullOrEmpty(updateUser.ClaveSindical) ? existingUser.ClaveSindical : updateUser.ClaveSindical;
            existingUser.BancoId = updateUser.BancoId == 0 ? existingUser.BancoId : updateUser.BancoId;
            existingUser.NumEmpCuentaClave = string.IsNullOrEmpty(updateUser.NumEmpCuentaClave) ? existingUser.NumEmpCuentaClave : updateUser.NumEmpCuentaClave;
            existingUser.NomEmpEstatus = updateUser.NomEmpEstatus ?? existingUser.NomEmpEstatus;
            existingUser.UserID = updateUser.UserID ?? existingUser.UserID;
            existingUser.DepartamentoID = updateUser.DepartamentoID == 0 ? existingUser.DepartamentoID : updateUser.DepartamentoID;
            existingUser.RecursoID = updateUser.RecursoID ?? existingUser.RecursoID;
            existingUser.NomEmpGradoDominio = updateUser.NomEmpGradoDominio ?? existingUser.NomEmpGradoDominio;
            existingUser.NomEmpTipoAnalisis = updateUser.NomEmpTipoAnalisis ?? existingUser.NomEmpTipoAnalisis;
            existingUser.NomEmpMarco = updateUser.NomEmpMarco ?? existingUser.NomEmpMarco;
            existingUser.EstaActivo = updateUser.EstaActivo ?? existingUser.EstaActivo;
            existingUser.EnviarCorreoFalta = updateUser.EnviarCorreoFalta ?? existingUser.EnviarCorreoFalta;
            existingUser.NomEmpClaveRHGE = string.IsNullOrEmpty(updateUser.NomEmpClaveRHGE) ? existingUser.NomEmpClaveRHGE : updateUser.NomEmpClaveRHGE;
            existingUser.AfiliacionIsssteson = updateUser.AfiliacionIsssteson ?? existingUser.AfiliacionIsssteson;
            existingUser.CatPuestoID = updateUser.CatPuestoID ?? existingUser.CatPuestoID;
            existingUser.NomEmpIngresoGobEdo = updateUser.NomEmpIngresoGobEdo ?? existingUser.NomEmpIngresoGobEdo;
            existingUser.NSS = updateUser.NSS ?? existingUser.NSS;
            existingUser.NumeroFonacot = updateUser.NumeroFonacot ?? existingUser.NumeroFonacot;
            existingUser.NomEmpClaveAntesRHGE = string.IsNullOrEmpty(updateUser.NomEmpClaveAntesRHGE) ? existingUser.NomEmpClaveAntesRHGE : updateUser.NomEmpClaveAntesRHGE;
            existingUser.RequiereLicencia = updateUser.RequiereLicencia ?? existingUser.RequiereLicencia;
            existingUser.ActividadPresupuestal = updateUser.ActividadPresupuestal ?? existingUser.ActividadPresupuestal;

            try
            {
                await _userContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                
            }

            return NoContent();
        }





    }

    public class LoginUser
    {
        public string? Email { get; set; }
        public string? Password { get; set; }

    }

    public class UpdateUser
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string NomEmpClave { get; set; } = string.Empty;
        public string NomEmpNombre { get; set; } = string.Empty;
        public string NomEmpPaterno { get; set; } = string.Empty;
        public string? NomEmpMaterno { get; set; }
        public byte NomEmpSexo { get; set; }
        public byte NomEmpEdoCivil { get; set; }
        public bool NomEmpTieneHijos { get; set; }
        public DateTime NomEmpFechaNacimiento { get; set; } // Changed to DateTime
        public string? NomEmpRFC { get; set; }
        public string? NomEmpCURP { get; set; }
        public long NomEmpDatosDireccionID { get; set; }
        public byte NomEmpTipoNomina { get; set; }
        public DateTime NomEmpFechaIngreso { get; set; } // Changed to DateTime
        public DateTime? NomEmpFechaTerminacion { get; set; } // Changed to DateTime?
        public int NomEmpAdscripcionID { get; set; }
        public int NomEmpPuestoID { get; set; }
        public int NomEmpTabuladorID { get; set; }
        public char NomEmpTabuladorSubNivelClave { get; set; }
        public int NomEmpNivelEstudioID { get; set; }
        public DateTime NomEmpFechaAltaIsssteson { get; set; } // Changed to DateTime
        public string? NomEmpNumeroPension { get; set; }
        public string? NomEmpNoCuentaBancaria { get; set; }
        public byte[]? NomEmpFoto { get; set; } // Optional
        public int SucursalId { get; set; }
        public byte Carrera { get; set; }
        public string? TipoSangre { get; set; }
        public string? NumLicenciaConducir { get; set; }
        public DateTime? FechaVencimLicencia { get; set; }
        public bool? DeclaracionPatrimonial { get; set; }
        public string? Antiguedad { get; set; }
        public string? ClaveSindical { get; set; }
        public int BancoId { get; set; }
        public string? NumEmpCuentaClave { get; set; }
        public bool? NomEmpEstatus { get; set; }
        public int? UserID { get; set; }
        public int DepartamentoID { get; set; }
        public int? RecursoID { get; set; }
        public byte? NomEmpGradoDominio { get; set; }
        public byte? NomEmpTipoAnalisis { get; set; }
        public byte? NomEmpMarco { get; set; }
        public bool? EstaActivo { get; set; }
        public bool? EnviarCorreoFalta { get; set; }
        public string? NomEmpClaveRHGE { get; set; }
        public long? AfiliacionIsssteson { get; set; }
        public int? CatPuestoID { get; set; }
        public DateTime? NomEmpIngresoGobEdo { get; set; } // Changed to DateTime?
        public long? NSS { get; set; }
        public long? NumeroFonacot { get; set; }
        public string? NomEmpClaveAntesRHGE { get; set; }
        public bool? RequiereLicencia { get; set; }
        public int? ActividadPresupuestal { get; set; }
    }

    public class CreateUser
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public  string UserRole { get; set; }
        public string NomEmpClave { get; set; } = string.Empty;
        public string NomEmpNombre { get; set; } = string.Empty;
        public string NomEmpPaterno { get; set; } = string.Empty;
        public string? NomEmpMaterno { get; set; }
        public byte NomEmpSexo { get; set; }
        public byte NomEmpEdoCivil { get; set; }
        public bool NomEmpTieneHijos { get; set; }
        public DateTime NomEmpFechaNacimiento { get; set; } // Changed to DateTime
        public string? NomEmpRFC { get; set; }
        public string? NomEmpCURP { get; set; }
        public long NomEmpDatosDireccionID { get; set; }
        public byte NomEmpTipoNomina { get; set; }
        public DateTime NomEmpFechaIngreso { get; set; } // Changed to DateTime
        public DateTime? NomEmpFechaTerminacion { get; set; } // Changed to DateTime?
        public int NomEmpAdscripcionID { get; set; }
        public int NomEmpPuestoID { get; set; }
        public int NomEmpTabuladorID { get; set; }
        public char NomEmpTabuladorSubNivelClave { get; set; }
        public int NomEmpNivelEstudioID { get; set; }
        public DateTime NomEmpFechaAltaIsssteson { get; set; } // Changed to DateTime
        public string? NomEmpNumeroPension { get; set; }
        public string? NomEmpNoCuentaBancaria { get; set; }
        public byte[]? NomEmpFoto { get; set; } // Optional
        public int SucursalId { get; set; }
        public byte Carrera { get; set; }
        public string? TipoSangre { get; set; }
        public string? NumLicenciaConducir { get; set; }
        public DateTime? FechaVencimLicencia { get; set; }
        public bool? DeclaracionPatrimonial { get; set; }
        public string? Antiguedad { get; set; }
        public string? ClaveSindical { get; set; }
        public int BancoId { get; set; }
        public string? NumEmpCuentaClave { get; set; }
        public bool? NomEmpEstatus { get; set; }
        public int? UserID { get; set; }
        public int DepartamentoID { get; set; }
        public int? RecursoID { get; set; }
        public byte? NomEmpGradoDominio { get; set; }
        public byte? NomEmpTipoAnalisis { get; set; }
        public byte? NomEmpMarco { get; set; }
        public bool? EstaActivo { get; set; }
        public bool? EnviarCorreoFalta { get; set; }
        public string? NomEmpClaveRHGE { get; set; }
        public long? AfiliacionIsssteson { get; set; }
        public int? CatPuestoID { get; set; }
        public DateTime? NomEmpIngresoGobEdo { get; set; } // Changed to DateTime?
        public long? NSS { get; set; }
        public long? NumeroFonacot { get; set; }
        public string? NomEmpClaveAntesRHGE { get; set; }
        public bool? RequiereLicencia { get; set; }
        public int? ActividadPresupuestal { get; set; }
    }

*/}
