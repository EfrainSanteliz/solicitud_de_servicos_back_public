using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SocialMediaApi.Controllers;
using solicitud_de_servicios_back.Models;
using solicitud_de_servicios_back.Services;
using System;
using System.Globalization;
using System.Text;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;




namespace solicitud_de_servicios_back.Controllers
{

    [Route("api/User")]
    [ApiController]
    public class UserController2 : ControllerBase
    {
        private readonly UserContext _userContext;
        private readonly UserContextPlasalcees _userContextPlasaibcess;
        private readonly IEmailService _emailService;


        private readonly ILogger<RequestController> _logger;
        private readonly JwtTokenService _jwtTokenService;


        public UserController2(JwtTokenService jwtTokenService, UserContext userContext, UserContextPlasalcees userContextPlasaibcess, ILogger<RequestController> logger, IEmailService emailService)
        {
            _userContext = userContext;
            _userContextPlasaibcess = userContextPlasaibcess;
            _logger = logger;
            _jwtTokenService = jwtTokenService;
            _emailService = emailService;
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult> LoginUser([FromBody] LoginUser loginUser)
        {
            // Validate input
            if (loginUser == null || string.IsNullOrEmpty(loginUser.Email) || string.IsNullOrEmpty(loginUser.Password))
            {
                return BadRequest(new { message = "Invalid login data" });
            }

            // Use FromSqlInterpolated to execute the stored procedure
            var userDetails = await _userContext.UserDetailsResult
                .FromSqlInterpolated($"EXEC sp_LoginUser @Email = {loginUser.Email}")
                .ToListAsync();

            if (userDetails == null || userDetails.Count == 0)
            {
                return NotFound(new { message = "User not found or invalid credentials" });
            }

           

            var user = userDetails.First();

            // Verify the password using BCrypt
            if (!BCrypt.Net.BCrypt.Verify(loginUser.Password, user.Password))
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }

            // Generate JWT token
            var token = _jwtTokenService.GenerateToken(user.SS_UsuarioId.ToString(), user.UserRole);

            return Ok(new
            {
                Token = token,
                User = user
            });
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SS_Usuarios>>> GetUsers()
        {
            var user = await _userContextPlasaibcess.NomEmpleados

            .Include(n => n.DireccionesICEES)
                //.Include(n => n.SS_Solicitudes)
                // .ThenInclude(r => r.ConActivosFijos)      
                //.Include(n => n.SS_Solicitudes)                      
                //.ThenInclude(r => r.HistorialComentarios)        
                .ToListAsync();

            return Ok(user);
        }

        [HttpGet("SS_ListEmpleadoAndUsers")]
        public async Task<ActionResult<IEnumerable<SS_Usuarios>>> GetNomEmpleadosAndUsers()
        {

            var nomEmpleadosAndUsers = await _userContext.SS_ListEmpleadoAndUsers
            .FromSqlInterpolated($"EXEC sp_listEmpleadoAndUser")
            .ToListAsync();

            if (nomEmpleadosAndUsers == null || nomEmpleadosAndUsers.Count == 0)
            {
                return NotFound(new { message = "solicitud not found" });
            }

            return Ok(new
            {
                nomEmpleadosAndUsers
            });

        }



        /* [HttpGet("GetEmailSuperAdministrador")]
         public async Task<ActionResult<IEnumerable<SS_Usuarios>>> GetEmailSuperAdministrador()
         {
             var users = await _userContext.SS_Usuarios
                 .Include(u => u.NomEmpleados)
                 .Where(u => u.UserRole == 4) // Filter users with UserRole == 4
                 .Select(u => u.Email)        // Select only the Email field
                 .ToListAsync();

             return Ok(users);
         }

         [HttpGet("{id}")]
         public async Task<ActionResult<NomEmpleados>> GetUser(int id)
         {
             var user = await _userContextPlasaibcess.NomEmpleados

              .Include(n => n.DireccionesICEES)  
              .Include(n => n.SS_Solicitudes)// Incluye los requests                                                                    // Incluye los requests
              .ThenInclude(r => r.ConActivosFijos)       // Incluye los activos fijos relacionados a cada request
                 .Include(n => n.SS_Solicitudes)                      // Incluye nuevamente los requests
                     .ThenInclude(r => r.HistorialComentarios)
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


             // Create a new NomEmpleados instance
             var NomEmpleado = new NomEmpleados
             {
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
             };

             // Add the new user to the context
            // _userContext.NomEmpleados.Add(NomEmpleado);
             await _userContext.SaveChangesAsync();

             var Usuario = new SS_Usuarios
             {
                 Email = createUser.Email,
                 Password = createUser.Password,
                 UserRole = createUser.UserRole,
                 EmpleadoID = NomEmpleado.EmpleadoID, // Foreign Key
             };


             _userContext.SS_Usuarios.Add(Usuario);
             await _userContext.SaveChangesAsync();

             // Return the created user with associated requests
             return CreatedAtAction(nameof(GetUser), new { id = NomEmpleado.EmpleadoID }, NomEmpleado);
         }


         [HttpPost("createOnlyNomEmpleado")]
         public async Task<ActionResult<NomEmpleados>> PostNomEmpleado(CreateUser2 createUser2)
         {
             // Create a new instance of NomEmpleados with all necessary fields
             var NomEmpleados = new NomEmpleados
             {



                     NomEmpClave = createUser2.NomEmpClave,
                     NomEmpNombre = createUser2.NomEmpNombre,
                     NomEmpPaterno = createUser2.NomEmpPaterno,
                     NomEmpMaterno = createUser2.NomEmpMaterno,
                     NomEmpSexo = createUser2.NomEmpSexo,
                     NomEmpEdoCivil = createUser2.NomEmpEdoCivil,
                     NomEmpTieneHijos = createUser2.NomEmpTieneHijos,
                     NomEmpFechaNacimiento = createUser2.NomEmpFechaNacimiento,
                     NomEmpRFC = createUser2.NomEmpRFC,
                     NomEmpCURP = createUser2.NomEmpCURP,
                     NomEmpDatosDireccionID = createUser2.NomEmpDatosDireccionID,
                     NomEmpTipoNomina = createUser2.NomEmpTipoNomina,
                     NomEmpFechaIngreso = createUser2.NomEmpFechaIngreso,
                     NomEmpFechaTerminacion = createUser2.NomEmpFechaTerminacion,
                     NomEmpAdscripcionID = createUser2.NomEmpAdscripcionID,
                     NomEmpPuestoID = createUser2.NomEmpPuestoID,
                     NomEmpTabuladorID = createUser2.NomEmpTabuladorID,
                     NomEmpTabuladorSubNivelClave = createUser2.NomEmpTabuladorSubNivelClave,
                     NomEmpNivelEstudioID = createUser2.NomEmpNivelEstudioID,
                     NomEmpFechaAltaIsssteson = createUser2.NomEmpFechaAltaIsssteson,
                     NomEmpNumeroPension = createUser2.NomEmpNumeroPension,
                     NomEmpNoCuentaBancaria = createUser2.NomEmpNoCuentaBancaria,
                     NomEmpFoto = createUser2.NomEmpFoto,
                     SucursalId = createUser2.SucursalId,
                     Carrera = createUser2.Carrera,
                     TipoSangre = createUser2.TipoSangre,
                     NumLicenciaConducir = createUser2.NumLicenciaConducir,
                     FechaVencimLicencia = createUser2.FechaVencimLicencia,
                     DeclaracionPatrimonial = createUser2.DeclaracionPatrimonial,
                     Antiguedad = createUser2.Antiguedad,
                     ClaveSindical = createUser2.ClaveSindical,
                     BancoId = createUser2.BancoId,
                     NumEmpCuentaClave = createUser2.NumEmpCuentaClave,
                     NomEmpEstatus = createUser2.NomEmpEstatus,
                     UserID = createUser2.UserID,
                     DepartamentoID = createUser2.DepartamentoID,
                     RecursoID = createUser2.RecursoID,
                     NomEmpGradoDominio = createUser2.NomEmpGradoDominio,
                     NomEmpTipoAnalisis = createUser2.NomEmpTipoAnalisis,
                     NomEmpMarco = createUser2.NomEmpMarco,
                     EstaActivo = createUser2.EstaActivo,
                     EnviarCorreoFalta = createUser2.EnviarCorreoFalta,
                     NomEmpClaveRHGE = createUser2.NomEmpClaveRHGE,
                     AfiliacionIsssteson = createUser2.AfiliacionIsssteson,
                     CatPuestoID = createUser2.CatPuestoID,
                     NomEmpIngresoGobEdo = createUser2.NomEmpIngresoGobEdo,
                     NSS = createUser2.NSS,
                     NumeroFonacot = createUser2.NumeroFonacot,
                     NomEmpClaveAntesRHGE = createUser2.NomEmpClaveAntesRHGE,
                     RequiereLicencia = createUser2.RequiereLicencia,
                     ActividadPresupuestal = createUser2.ActividadPresupuestal,

             };


             // Add the new user to the context
             _userContext.NomEmpleados.Add(NomEmpleados);
             await _userContext.SaveChangesAsync();


             // Return the created user with associated requests
             return CreatedAtAction(nameof(PostNomEmpleado), new { id = NomEmpleados.EmpleadoID }, NomEmpleados);
         } */

        [HttpPost("createAllUsers")]
        public async Task<ActionResult<SS_Usuarios>> PostOnlyUser()
        {
            // Ensure you are working with a single user or logic to find the target user
            // var nomEmpleados = await _userContextPlasaibcess.NomEmpleados
            //   .ToListAsync();

            //if (nomEmpleados == null || !nomEmpleados.Any())
            // {
            //   return NotFound("No empleados found");
            //  }

            //var nomEmpleados = await _userContext.SS_ListEmpleadoAndUsers
              //.FromSqlInterpolated($"EXEC sp_listEmpleadoAndUser")
             // .ToListAsync();

            var nomEmpleados = await _userContext.SS_ListEmpleados
               .FromSqlInterpolated($"EXEC sp_listEmpleado")
               .ToListAsync();

            if (nomEmpleados == null || nomEmpleados.Count == 0)
            {
                return NotFound(new { message = "solicitud not found" });
            }

            var existingEmails = await _userContext.SS_Usuarios
                .Select(u => u.Email)
                .ToListAsync();

            var usuarios = new List<SS_Usuarios>();
            var random = new Random();

            foreach ( var empleado in nomEmpleados)
            {   
                if(empleado.EstaActivo == false)
                {
                    continue;
                }

                var nomEmpNombre = empleado.NomEmpNombre?.Split(' ').FirstOrDefault() ?? string.Empty;

                var email = $"{empleado.NomEmpPaterno}.{empleado.NomEmpMaterno}.{nomEmpNombre}@sonora.edu.mx";

                if(existingEmails.Contains(email))
                {
                    continue;
                }

                // Generate a random 4-digit password
                var password = random.Next(1000, 9999).ToString();

                // Create the SS_Usuarios object
                var usuario = new SS_Usuarios
                {

                    Email = email,
                    Password = password,
                    UserRole = 1,
                    EmpleadoID = empleado.EmpleadoID,
                };

                usuarios.Add(usuario);

            }
            if (usuarios.Any())
            {
                // Add and save the new user
                _userContext.SS_Usuarios.AddRange(usuarios);
                await _userContext.SaveChangesAsync();
            }
            

            // Return the created user
            return CreatedAtAction(nameof(PostOnlyUser), new { count = usuarios.Count }, usuarios);
        }


        [HttpPost("createUser/{id}")]
        public async Task<ActionResult<SS_Usuarios>> PostUser(int id)
        {
            // Buscar empleado en NomEmpleados
            // var nomEmpleados = await _userContextPlasaibcess.NomEmpleados
            //   .FirstOrDefaultAsync(r => r.EmpleadoID == id);\

            var nomEmpleados = await _userContext.SS_ListEmpleados
              .FromSqlInterpolated($"EXEC sp_ShowEmpleado @empleadoID = {id}")
              .ToListAsync();

            var empleado = nomEmpleados.First();


            if (nomEmpleados == null)
            {
                return NotFound("No empleados found");
            }

            // Método para eliminar acentos y reemplazar caracteres especiales
            string NormalizeString(string input)
            {
                if (string.IsNullOrEmpty(input)) return string.Empty;

                // Eliminar acentos
                var normalized = input.Normalize(NormalizationForm.FormD);
                var stringBuilder = new StringBuilder();

                foreach (var c in normalized)
                {
                    var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                    if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                    {
                        stringBuilder.Append(c == 'ñ' ? 'n' : c == 'Ñ' ? 'N' : c); // Reemplazar 'ñ' y 'Ñ'
                    }
                }

                return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
            }

            // Crear correo normalizando nombres
            var nomEmpNombre = NormalizeString(empleado.NomEmpNombre?.Split(' ').FirstOrDefault());
            var nomEmpPaterno = NormalizeString(empleado.NomEmpPaterno);
            var nomEmpMaterno = NormalizeString(empleado.NomEmpMaterno);

            var email = $"{nomEmpPaterno.ToUpper()}{nomEmpMaterno.ToUpper()}{nomEmpNombre.ToUpper()}@gmail.com";

            // Generar una contraseña aleatoria de 6 dígitos
            var random = new Random();
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            const string numbers = "0123456789";

            // Garantizar al menos 2 letras y 2 números
            string mandatoryLetters = new string(Enumerable.Range(0, 2).Select(_ => letters[random.Next(letters.Length)]).ToArray());
            string mandatoryNumbers = new string(Enumerable.Range(0, 2).Select(_ => numbers[random.Next(numbers.Length)]).ToArray());

            // Llenar el resto con letras o números aleatorios
            string allCharacters = letters + numbers;
            string rest = new string(Enumerable.Range(0, 4).Select(_ => allCharacters[random.Next(allCharacters.Length)]).ToArray());

            // Mezclar la contraseña
            string password = mandatoryLetters + mandatoryNumbers + rest;


            // Crear el objeto SS_Usuarios
            var usuario = new SS_Usuarios
            {
                Email = email,
                Password = password,
                UserRole = 1,
                EmpleadoID = empleado.EmpleadoID,
            };

            var encabezado = "Se le ha creado una cuenta en el sistema de 'Solicitudes de Servicios' del Instituto de Becas y Crédito Educativo del Estado de Sonora";

            var cuerpo = "Inicie sesión con su correo institucional. Su contraseña es: " + password + ". Acceda al enlace para iniciar sesión: http://solicituddeservicios.becasycredito.gob.mx/";

            await _emailService.SendEmailAsync(email, encabezado, cuerpo);


            usuario.SetPassword(password);


            // Guardar el nuevo usuario en la base de datos
            _userContext.SS_Usuarios.Add(usuario);
            await _userContext.SaveChangesAsync();

            // Devolver el usuario creado
            return CreatedAtAction(nameof(PostUser), new { id = usuario.EmpleadoID }, usuario);
        }

        [HttpPost("showUser/{id}")]
        public async Task<ActionResult<SS_Usuarios>> showUser(int id)
        {
            // Buscar empleado en NomEmpleados
            // var nomEmpleados = await _userContextPlasaibcess.NomEmpleados
            //   .FirstOrDefaultAsync(r => r.EmpleadoID == id);\

            var nomEmpleados = await _userContext.SS_ListEmpleados
              .FromSqlInterpolated($"EXEC sp_ShowEmpleado @empleadoID = {id}")
              .ToListAsync();

            var empleado = nomEmpleados.First();


            if (!nomEmpleados.Any())
            {
                return NotFound($"No employee found with ID: {id}");
            }
            // Devolver el usuario creado
            return Ok(new
            {
                //   Request = request,
                Empleado = empleado
                
            });
        }

        [HttpPut("{SS_UsuarioId}")]
        public async Task<IActionResult> PutRequest(int SS_UsuarioId, [FromBody] UpdateUser updateUser)
        {
            _logger.LogInformation("Received PUT request for id: {Id} with data: {@UpdateRequestDto}", SS_UsuarioId, updateUser);

            if (SS_UsuarioId <= 0 || updateUser == null)
            {
                return BadRequest("Datos de la solicitud inválidos.");
            }

            // Buscar la solicitud existente
            var existingUser = await _userContext.SS_Usuarios.FindAsync(SS_UsuarioId);
            if (existingUser == null)
            {
                return NotFound("Solicitud no encontrada.");
            }

            // Solo actualizar los campos que el usuario envía (que no son nulos)
            if (updateUser.Password != null)
            {
                existingUser.Password = updateUser.Password;
            }
         


            // Guardar los cambios en la base de datos
            try
            {
                await _userContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(SS_UsuarioId))
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
        [HttpPut("changeByEmailCode/{email}")]
        public async Task<IActionResult> PutUserByEmailCode(string email)
        {
            _logger.LogInformation("Received PUT request for email: {email}", email);

            // Search for the existing user
            var existingUser = await _userContext.SS_Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (existingUser == null)
            {
                return NotFound("Solicitud no encontrada.");
            }

            // Generate a random 4-digit integer code for password reset
            var random = new Random();
            var codigoParaRestablecerContraseña = random.Next(10000000, 99999999).ToString(); // Generate an integer


            // Update the existing user's reset code
            existingUser.SetCodigoParaRestablecerContraseña(codigoParaRestablecerContraseña);

            var encabezado = "Codigo de verificacion";

            var cuerpo = "su codigo de verificacion es " + codigoParaRestablecerContraseña;
           
            await _emailService.SendEmailAsync(email, encabezado, cuerpo);

            // Save changes to the database
            try
            {
                await _userContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExist(email))
                {
                    return NotFound("Usuario no encontrado.");
                }
                else
                {
                    throw;
                }
            }

            return Ok(codigoParaRestablecerContraseña);

        }

        [HttpPut("dropEmailCode/{email}")]
        public async Task<IActionResult> DropEmailCode(string email)
        {
            _logger.LogInformation("Received PUT request for email: {email}", email);

            // Search for the existing user
            var existingUser = await _userContext.SS_Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (existingUser == null)
            {
                return NotFound("Solicitud no encontrada.");
            }



            // Generate a random 4-digit integer code for password reset
            var random = new Random();
            

            // Update the existing user's reset code
            existingUser.CodigoParaRestablecerContraseña = null;

            // Save changes to the database
            try
            {
                await _userContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExist(email))
                {
                    return NotFound("Usuario no encontrado.");
                }
                else
                {
                    throw;
                }
            }

            return Ok();

        }




        [HttpPut("changeByEmail/{email}")]
        public async Task<IActionResult> PutUserByEmail(string email, [FromBody] UpdateUser updateUser)
        {
            _logger.LogInformation("Received PUT request for email: {email} with data: {@updateUser}", email, updateUser);

        

            // Buscar la solicitud existente
            var existingUser = await _userContext.SS_Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (existingUser == null)
            {
                return NotFound("Solicitud no encontrada.");
            }

            // Solo actualizar los campos que el usuario envía (que no son nulos)
            // Update fields that are not null
            if (!string.IsNullOrEmpty(updateUser.Password))
            {
                existingUser.SetPassword(updateUser.Password);
            }
            // Guardar los cambios en la base de datos


            try
            {
                await _userContext.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExist(email))
                {
                    return NotFound("UsuarioNoEncontrado.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        private bool RequestExists(int SS_UsuarioId)
        {
            return _userContext.SS_Usuarios.Any(e => e.SS_UsuarioId == SS_UsuarioId);
        }

        private bool UserExist(string email)
        {
            return _userContext.SS_Usuarios.Any(e => e.Email == email);
        }



        /*  [HttpPut("{NomEmpRFC}")]
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
          }*/





    }

    public class LoginUser
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }

    public class SS_ListEmpleados
    {
        public int EmpleadoID { get; set; }
        public string? NomEmpNombre { get; set; }
        public string? NomEmpPaterno { get; set; }
        public string? NomEmpMaterno { get; set; }
        public bool? EstaActivo { get; set; }
    }

    public class SS_listEmpleadoAndUsers
    {

        public int? SS_UsuarioId { get; set; }

        public int? EmpleadoID { get; set; }

        public string? Email { get; set; }

        public string? NomEmpNombre { get; set; }

        public string? NomEmpPaterno { get; set; }

        public string? NomEmpMaterno { get; set; }

        public string? DireccionesDescripcion {get; set; }


    }
    public class SS_UserDetailsResult
    {
        public int SS_UsuarioId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int UserRole { get; set; }
        public int EmpleadoID { get; set; }
        public string NomEmpNombre { get; set; }
        public string NomEmpPaterno { get; set; }
        public string NomEmpMaterno { get; set; }
        public string DireccionesDescripcion { get; set; }
    }

    public class ListEmpleadoAndUser
    {
        public string Email { get; set; }

        public string NomEmpNombre { get; set; }

        public string NomEmpPaterno { get; set; }

        public string NomEmpMaterno { get; set; }

        public string DireccionesDescripcion {  get; set; } 

    }

    public class UpdateUser
    {
       // public int? EmpleadoID { get; set; }
        //public string Email { get; set; }
        public string Password { get; set; }
       // public int? CodigoParaRestablecerContraseña { get; set; }


        /* public string NomEmpClave { get; set; } = string.Empty;
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
         */
    }

    public class UpdateUserCode
    {

        public int? CodigoParaRestablecerContraseña { get; set; }
       
    }


    public class CreateUser
    {

        public int? EmpleadoID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int UserRole { get; set; }
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
        public string NomEmpTabuladorSubNivelClave { get; set; }
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

    public class CreateOnlyUser
    {

        public int? EmpleadoID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int UserRole { get; set; }

    }



    public class CreateUser2
    {


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
        public string NomEmpTabuladorSubNivelClave { get; set; }
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

}
