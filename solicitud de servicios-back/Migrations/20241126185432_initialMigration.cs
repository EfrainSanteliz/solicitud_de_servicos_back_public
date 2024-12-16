using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace solicitud_de_servicios_back.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConActivosFijos",
                columns: table => new
                {
                    ActivoFijoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AFTipoActivoFijoID = table.Column<int>(type: "int", nullable: false),
                    AFClave = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    AFNombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AFDescripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AFEmpleadoID = table.Column<int>(type: "int", nullable: true),
                    AFDatosAdicionales = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AFFechaFactura = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    AFFactura = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    AFMarca = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AFModelo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AFValorBien = table.Column<string>(type: "varchar(10)", nullable: false),
                    AFNoSerie = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    AFEstatus = table.Column<byte>(type: "tinyint", nullable: false),
                    AFObservaciones = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AFSucursalId = table.Column<int>(type: "int", nullable: false),
                    AFCuentaContableID = table.Column<int>(type: "int", nullable: false),
                    AFFolio = table.Column<int>(type: "int", nullable: false),
                    AFFechaRegistro = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    DireccionICEESID = table.Column<int>(type: "int", nullable: false),
                    AFPropietarioBienId = table.Column<int>(type: "int", nullable: false),
                    AFFechaAsignacion = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    AFFechaCancelacion = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    AFFechaDonacion = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    AFValorDonacion = table.Column<decimal>(type: "decimal(12,2)", nullable: true),
                    AFesDonacion = table.Column<bool>(type: "bit", nullable: true),
                    AFDonacionValuacion = table.Column<decimal>(type: "decimal(12,2)", nullable: true),
                    AFBienMuebleID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConActivosFijos", x => x.ActivoFijoID);
                });

            migrationBuilder.CreateTable(
                name: "DireccionesICEES",
                columns: table => new
                {
                    DireccionICEESID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    SubgrupoID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CenCostosId = table.Column<int>(type: "int", nullable: true),
                    SucursalId = table.Column<int>(type: "int", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DireccionesICEES", x => x.DireccionICEESID);
                });

            migrationBuilder.CreateTable(
                name: "NomEmpleados",
                columns: table => new
                {
                    EmpleadoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomEmpClave = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NomEmpNombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NomEmpPaterno = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NomEmpMaterno = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NomEmpSexo = table.Column<byte>(type: "tinyint", nullable: false),
                    NomEmpEdoCivil = table.Column<byte>(type: "tinyint", nullable: false),
                    NomEmpTieneHijos = table.Column<bool>(type: "bit", nullable: false),
                    NomEmpFechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NomEmpRFC = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    NomEmpCURP = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: false),
                    NomEmpDatosDireccionID = table.Column<long>(type: "bigint", nullable: false),
                    NomEmpTipoNomina = table.Column<byte>(type: "tinyint", nullable: false),
                    NomEmpFechaIngreso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NomEmpFechaTerminacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NomEmpPuestoID = table.Column<int>(type: "int", nullable: false),
                    NomEmpTabuladorID = table.Column<int>(type: "int", nullable: false),
                    NomEmpTabuladorSubNivelClave = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    NomEmpNivelEstudioID = table.Column<int>(type: "int", nullable: false),
                    NomEmpFechaAltaIsssteson = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NomEmpNumeroPension = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NomEmpNoCuentaBancaria = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NomEmpFoto = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    SucursalId = table.Column<int>(type: "int", nullable: false),
                    Carrera = table.Column<byte>(type: "tinyint", nullable: false),
                    TipoSangre = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    NumLicenciaConducir = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    FechaVencimLicencia = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeclaracionPatrimonial = table.Column<bool>(type: "bit", nullable: true),
                    Antiguedad = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ClaveSindical = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    BancoId = table.Column<int>(type: "int", nullable: false),
                    NumEmpCuentaClave = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: false),
                    NomEmpEstatus = table.Column<bool>(type: "bit", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    DepartamentoID = table.Column<int>(type: "int", nullable: false),
                    RecursoID = table.Column<int>(type: "int", nullable: true),
                    NomEmpGradoDominio = table.Column<byte>(type: "tinyint", nullable: true),
                    NomEmpTipoAnalisis = table.Column<byte>(type: "tinyint", nullable: true),
                    NomEmpMarco = table.Column<byte>(type: "tinyint", nullable: true),
                    EstaActivo = table.Column<bool>(type: "bit", nullable: true),
                    EnviarCorreoFalta = table.Column<bool>(type: "bit", nullable: true),
                    NomEmpClaveRHGE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AfiliacionIsssteson = table.Column<long>(type: "bigint", nullable: true),
                    CatPuestoID = table.Column<int>(type: "int", nullable: true),
                    NomEmpIngresoGobEdo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NSS = table.Column<long>(type: "bigint", nullable: true),
                    NumeroFonacot = table.Column<long>(type: "bigint", nullable: true),
                    NomEmpClaveAntesRHGE = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    RequiereLicencia = table.Column<bool>(type: "bit", nullable: true),
                    ActividadPresupuestal = table.Column<int>(type: "int", nullable: true),
                    NomEmpAdscripcionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NomEmpleados", x => x.EmpleadoID);
                    table.ForeignKey(
                        name: "FK_NomEmpleados_DireccionesICEES_NomEmpAdscripcionID",
                        column: x => x.NomEmpAdscripcionID,
                        principalTable: "DireccionesICEES",
                        principalColumn: "DireccionICEESID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NomEmpleados_NomEmpAdscripcionID",
                table: "NomEmpleados",
                column: "NomEmpAdscripcionID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConActivosFijos");

            migrationBuilder.DropTable(
                name: "NomEmpleados");

            migrationBuilder.DropTable(
                name: "DireccionesICEES");
        }
    }
}
