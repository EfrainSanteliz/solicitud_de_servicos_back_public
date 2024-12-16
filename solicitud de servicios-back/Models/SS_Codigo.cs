using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace solicitud_de_servicios_back.Models
{
    public class SS_Codigo
    {
        [Key]
        public int SS_CodigoID { get; set; }

        public int Codigo { get; set; }

        //Roreing key to User
        public int? SS_UsuarioId { get; set; }

        [ForeignKey("SS_UsuarioId")]

        public SS_Usuarios? SS_Usuarios { get; set; }

    }
}
