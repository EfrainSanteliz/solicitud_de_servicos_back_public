using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using solicitud_de_servicios_back.Models;
using solicitud_de_servicios_back.Services;

namespace solicitud_de_servicios_back.Controllers
{
    [ApiController]
    [Route("api/email/")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly UserContext _userContext;

        public EmailController(UserContext userContext,IEmailService emailService)
        {
            _emailService = emailService;
            _userContext = userContext;

        }


        [HttpPost("FirmaSuperAdministradorEmail/{email}")]

        public async Task<IActionResult> SendTestEmail(string email)
        {

            var encabezado = "Tu solicitud ha sido firmada por Infraestructura y tecnologias de la informacion ";

            var cuerpo = "inicia sesion para ver el estado de tu solicitud http://solicituddeservicios.becasycredito.gob.mx/";

            try
            {
                await _emailService.SendEmailAsync(email, encabezado, cuerpo);
                return Ok(new { message = "Email sent successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error sending email: {ex.Message}" });
            }
        }

        //metodo para enviar correos 
        [HttpPost("FirmaAdministradorEmail/{email}/")]

        public async Task<IActionResult> SendAdministradorEmail(string email)
        {

            var encabezado = "Tu solicitud ha sido firmada por el jefe de area ";

            var cuerpo = "inicia sesion para ver el estado de tu solicitud http://solicituddeservicios.becasycredito.gob.mx/";

            try
            {
                await _emailService.SendEmailAsync(email, encabezado, cuerpo);
                return Ok(new { message = "Email sent successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error sending email: {ex.Message}" });
            }
        }

        //metodo para enviar un correo sobre comentarios de la solicitud

        [HttpPost("EmailComentario/{email}/")]

        public async Task<IActionResult> EmailComentario(string email)
        {

            var encabezado = "Tienes Nuevos Comentarios en tu solicitud";

            var cuerpo = "Inicia Sesion en la pagina para ver tus comentarios http://solicituddeservicios.becasycredito.gob.mx/";



            try
            {
                await _emailService.SendEmailAsync(email, encabezado, cuerpo);
                return Ok(new { message = "Email sent successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error sending email: {ex.Message}" });
            }
        }

        [HttpPost("send-test-emailSub/{email}")]
        public async Task<IActionResult> SendTestEmailsub(string email)
        {

            var encabezado = "Tienes un nuevo comentario en tu solicitud";
            var encabezado2 = "El subAdministrador ha comentado una solicitud";
            var cuerpo = "Entra a este link para ver el estado de tu solicitud";
            var cuerpo2 = "Entra a este link para ver el estado de la solicitud";



            // Get the list of Super Administrators' emails
            var superAdminEmails = await _userContext.SS_Usuarios
                .Where(u => u.UserRole == 4)
                .Select(u => u.Email)
                .ToListAsync();

            try
            {
                // Send the first email to the provided email
                await _emailService.SendEmailAsync(email, encabezado, cuerpo);

                // Send the second email to each Super Administrator
                foreach (var superAdminEmail in superAdminEmails)
                {
                    if (superAdminEmail != null)
                    {
                        await _emailService.SendEmailAsync(superAdminEmail, encabezado2, cuerpo2);
                    }
                }

                return Ok(new { message = "Both emails sent successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error sending email: {ex.Message}" });
            }
        }




    }
}
//
