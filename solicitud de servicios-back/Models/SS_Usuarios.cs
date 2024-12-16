using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BCrypt.Net;

namespace solicitud_de_servicios_back.Models
{
    public class SS_Usuarios
    {

        [Key] public int SS_UsuarioId { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        public int UserRole { get; set; }

        public string? CodigoParaRestablecerContraseña { get; set; }

       // public SS_Codigo? SS_Codigos { get; set; }  // Navigation property to Usuarios

        //Roreing key to User
        public int? EmpleadoID { get; set; }

        // [ForeignKey("EmpleadoID")]

        /// public NomEmpleados? NomEmpleados { get; set; }
        /// 
        // public SS_Codigo SS_Codigo { get; set; }

        // Hash the password before saving
        public void SetPassword(string plainTextPassword)
        {
            Password = BCrypt.Net.BCrypt.HashPassword(plainTextPassword);
        }

        // Verify the password
        public bool VerifyPassword(string plainTextPassword)
        {
            return BCrypt.Net.BCrypt.Verify(plainTextPassword, Password);
        }

        public void SetCodigoParaRestablecerContraseña(string? plainTextCodigoParaRestablecerContraseña)
        {
            CodigoParaRestablecerContraseña = BCrypt.Net.BCrypt.HashPassword(plainTextCodigoParaRestablecerContraseña);
        }

        // Verify the password
        public bool VerifyCodigoParaRestablecerContraseña(string? plainTextCodigoParaRestablecerContraseña)
        {
            return BCrypt.Net.BCrypt.Verify(plainTextCodigoParaRestablecerContraseña, CodigoParaRestablecerContraseña);
        }



    }
}
