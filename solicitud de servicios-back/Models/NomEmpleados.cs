using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace solicitud_de_servicios_back.Models
{
    public class NomEmpleados
    {
        [Key]
        public int EmpleadoID { get; set; }

        [Required]
        [MaxLength(50)]  // Assuming length for UD_Clave
        public string NomEmpClave { get; set; }

        [Required]
        [MaxLength(100)]  // Assuming length for UD_Nombre
        public string NomEmpNombre { get; set; }

        [Required]
        [MaxLength(100)]  // Assuming length for UD_Nombre
        public string NomEmpPaterno { get; set; }

        [MaxLength(100)]  // Assuming length for UD_Nombre
        public string NomEmpMaterno { get; set; }

        [Required]
        public byte NomEmpSexo { get; set; }

        [Required]
        public byte NomEmpEdoCivil { get; set; }

        [Required]
        public bool NomEmpTieneHijos { get; set; }

        [Required]
        public DateTime NomEmpFechaNacimiento { get; set; }

        [MaxLength(15)]
        public string NomEmpRFC { get; set; }

        [MaxLength(18)]
        public string NomEmpCURP { get; set; }

        [Required]
        public long NomEmpDatosDireccionID { get; set; }

        [Required]
        public byte NomEmpTipoNomina { get; set; }

        [Required]
        public DateTime NomEmpFechaIngreso { get; set; }

        public DateTime? NomEmpFechaTerminacion { get; set; }


        [Required]
        public int NomEmpPuestoID { get; set; }

        [Required]
        public int NomEmpTabuladorID { get; set; }

        [Required]
        [MaxLength(1)]
        public string NomEmpTabuladorSubNivelClave { get; set; }

        [Required]
        public int NomEmpNivelEstudioID { get; set; }

        [Required]
        public DateTime NomEmpFechaAltaIsssteson { get; set; }

        [MaxLength(20)]
        public string NomEmpNumeroPension { get; set; }

        [MaxLength(20)]  // Assuming length for UD_NumeroCuentaDepositos
        public string NomEmpNoCuentaBancaria { get; set; }

        public byte[]? NomEmpFoto { get; set; }

        [Required]
        public int SucursalId { get; set; }

        [Required]
        public byte Carrera { get; set; }

        [MaxLength(10)]
        public string TipoSangre { get; set; }

        [MaxLength(15)]
        public string NumLicenciaConducir { get; set; }

        public DateTime? FechaVencimLicencia { get; set; }

        public bool? DeclaracionPatrimonial { get; set; }

        [MaxLength(10)]
        public string Antiguedad { get; set; }

        [MaxLength(15)]
        public string ClaveSindical { get; set; }

        [Required]
        public int BancoId { get; set; }

        [MaxLength(18)]
        public string NumEmpCuentaClave { get; set; }

        public bool? NomEmpEstatus { get; set; }

        public int? UserID { get; set; }

        [Required]
        public int DepartamentoID { get; set; }

        public int? RecursoID { get; set; }

        public byte? NomEmpGradoDominio { get; set; }

        public byte? NomEmpTipoAnalisis { get; set; }

        public byte? NomEmpMarco { get; set; }

        public bool? EstaActivo { get; set; }

        public bool? EnviarCorreoFalta { get; set; }

        [MaxLength(50)]  // Assuming length for UD_Clave
        public string NomEmpClaveRHGE { get; set; }

        public long? AfiliacionIsssteson { get; set; }

        public int? CatPuestoID { get; set; }

        public DateTime? NomEmpIngresoGobEdo { get; set; }

        public long? NSS { get; set; }

        public long? NumeroFonacot { get; set; }

        [MaxLength(10)]
        public string NomEmpClaveAntesRHGE { get; set; }

        public bool? RequiereLicencia { get; set; }

        public int? ActividadPresupuestal { get; set; }

        //public SS_Usuarios? Usuario { get; set; }  // Navigation property to Usuarios

        // Relaciones

        [Required]
        public int NomEmpAdscripcionID { get; set; }
        public DireccionesICEES? DireccionesICEES { get; set; }

       // public ICollection<SS_Solicitud>? SS_Solicitudes { get; set; }

    }
}
