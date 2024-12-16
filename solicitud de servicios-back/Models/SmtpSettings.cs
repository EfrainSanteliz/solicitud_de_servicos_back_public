


namespace solicitud_de_servicios_back.Models
{
    public class SmtpSettings
    {
        public string Server { get; set; }   // Servidor SMTP
        public int Port { get; set; }       // Puerto
        public string Username { get; set; } // Usuario para autenticación SMTP
        public string Password { get; set; } // Contraseña para autenticación SMTP
        public string ApiKey { get; set; }  // Clave de API (opcional si usas API en lugar de SMTP)


    }
}//
