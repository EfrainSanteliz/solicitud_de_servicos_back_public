using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using solicitud_de_servicios_back.Models;
using System.Text.Json;
using System.Text;
using System.Net.Http;



namespace solicitud_de_servicios_back.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string message);
    }

    public class EmailService : IEmailService
    {
        private readonly string _apiKey;

        public EmailService(IOptions<SmtpSettings> SmtpSettings)
        {
            _apiKey = SmtpSettings.Value.ApiKey;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("api-key", _apiKey);

            var emailRequest = new
            {
                sender = new {name = "Becas y Credito Educativo", email = "soporte@becasycredito.gob.mx" },
                to = new[] {new {email = toEmail}},
                subject = subject,
                htmlContent = message
            };

         
            var content = new StringContent(JsonSerializer.Serialize(emailRequest),Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://api.sendinblue.com/v3/smtp/email", content);

            if (!response.IsSuccessStatusCode) 
            {
             var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error sending email: {response.StatusCode}, {error}");
            }
        }
    }
}//