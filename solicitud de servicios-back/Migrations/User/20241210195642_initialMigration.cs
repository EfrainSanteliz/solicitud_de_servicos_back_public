using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace solicitud_de_servicios_back.Migrations.User
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SS_ConActivosFijosSP",
                columns: table => new
                {
                    ActivoFijoID = table.Column<int>(type: "int", nullable: false),
                    AFClave = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    AFNombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AFDescripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "SS_ListEmpleadoAndUsers",
                columns: table => new
                {
                    SS_UsuarioId = table.Column<int>(type: "int", nullable: true),
                    EmpleadoID = table.Column<int>(type: "int", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomEmpNombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomEmpPaterno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomEmpMaterno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DireccionesDescripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "SS_ListEmpleados",
                columns: table => new
                {
                    EmpleadoID = table.Column<int>(type: "int", nullable: false),
                    NomEmpNombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomEmpPaterno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomEmpMaterno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstaActivo = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "SS_Servicio_Solicitados",
                columns: table => new
                {
                    SS_Servicio_solicitado_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescripcionServicio_Solicitado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HabilitadoServicio_Solicitado = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SS_Servicio_Solicitados", x => x.SS_Servicio_solicitado_Id);
                });

            migrationBuilder.CreateTable(
                name: "SS_showSolicitudes",
                columns: table => new
                {
                    SS_SolicitudId = table.Column<int>(type: "int", nullable: false),
                    FirmaEmpleado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescripcionServicio_Solicitado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescripcionSolicitud_De_Servicio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SS_Servicio_solicitado_Id = table.Column<int>(type: "int", nullable: false),
                    FechaSolicitada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RevisadoSub = table.Column<bool>(type: "bit", nullable: true),
                    Estatus = table.Column<int>(type: "int", nullable: true),
                    Prioridad = table.Column<int>(type: "int", nullable: true),
                    UltimoStatus = table.Column<int>(type: "int", nullable: true),
                    Archivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirmaJefeDepartamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirmaJefe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomEmpNombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomEmpPaterno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomEmpMaterno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AFClave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AFDescripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DireccionesDescripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "SS_Solicitud_De_Servicios",
                columns: table => new
                {
                    SS_Solicitud_de_servicio_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescripcionSolicitud_De_Servicio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Habilitado = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SS_Solicitud_De_Servicios", x => x.SS_Solicitud_de_servicio_id);
                });

            migrationBuilder.CreateTable(
                name: "SS_solicitudDetails",
                columns: table => new
                {
                    SS_SolicitudId = table.Column<int>(type: "int", nullable: false),
                    FirmaEmpleado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescripcionServicio_Solicitado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaSolicitada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RevisadoSub = table.Column<bool>(type: "bit", nullable: true),
                    Estatus = table.Column<int>(type: "int", nullable: true),
                    Prioridad = table.Column<int>(type: "int", nullable: true),
                    UltimoStatus = table.Column<int>(type: "int", nullable: true),
                    Archivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomEmpNombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomEmpPaterno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomEmpMaterno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DireccionesDescripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "SS_Usuarios",
                columns: table => new
                {
                    SS_UsuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserRole = table.Column<int>(type: "int", nullable: false),
                    CodigoParaRestablecerContraseña = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmpleadoID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SS_Usuarios", x => x.SS_UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "UserDetailsResult",
                columns: table => new
                {
                    SS_UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserRole = table.Column<int>(type: "int", nullable: false),
                    EmpleadoID = table.Column<int>(type: "int", nullable: false),
                    NomEmpNombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomEmpPaterno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomEmpMaterno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DireccionesDescripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "SS_Solicitudes",
                columns: table => new
                {
                    SS_SolicitudId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServicioSolicitado = table.Column<int>(type: "int", nullable: true),
                    FechaSolicitada = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estatus = table.Column<int>(type: "int", nullable: true),
                    FirmaEmpleado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirmaJefeDepartamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirmaJefe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Archivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prioridad = table.Column<int>(type: "int", nullable: true),
                    RevisadoSub = table.Column<bool>(type: "bit", nullable: true),
                    UltimoStatus = table.Column<int>(type: "int", nullable: true),
                    EmpleadoID = table.Column<int>(type: "int", nullable: true),
                    ActivoFijoID = table.Column<int>(type: "int", nullable: true),
                    SS_Servicio_solicitado_Id = table.Column<int>(type: "int", nullable: true),
                    SS_Solicitud_de_servicio_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SS_Solicitudes", x => x.SS_SolicitudId);
                    table.ForeignKey(
                        name: "FK_SS_Solicitudes_SS_Servicio_Solicitados_SS_Servicio_solicitado_Id",
                        column: x => x.SS_Servicio_solicitado_Id,
                        principalTable: "SS_Servicio_Solicitados",
                        principalColumn: "SS_Servicio_solicitado_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SS_Solicitudes_SS_Solicitud_De_Servicios_SS_Solicitud_de_servicio_id",
                        column: x => x.SS_Solicitud_de_servicio_id,
                        principalTable: "SS_Solicitud_De_Servicios",
                        principalColumn: "SS_Solicitud_de_servicio_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SS_HistorialComentarios",
                columns: table => new
                {
                    SS_HistorialComentariosID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Comentarios = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remitente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SS_SolicitudId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SS_HistorialComentarios", x => x.SS_HistorialComentariosID);
                    table.ForeignKey(
                        name: "FK_SS_HistorialComentarios_SS_Solicitudes_SS_SolicitudId",
                        column: x => x.SS_SolicitudId,
                        principalTable: "SS_Solicitudes",
                        principalColumn: "SS_SolicitudId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SS_HistorialPrioridads",
                columns: table => new
                {
                    SS_HistorialPrioridadId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prioridad = table.Column<int>(type: "int", nullable: true),
                    FechaPrioridad = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SS_SolicitudId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SS_HistorialPrioridads", x => x.SS_HistorialPrioridadId);
                    table.ForeignKey(
                        name: "FK_SS_HistorialPrioridads_SS_Solicitudes_SS_SolicitudId",
                        column: x => x.SS_SolicitudId,
                        principalTable: "SS_Solicitudes",
                        principalColumn: "SS_SolicitudId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SS_HistorialStatus",
                columns: table => new
                {
                    SS_HistorialStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    FechaStatus = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SS_SolicitudId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SS_HistorialStatus", x => x.SS_HistorialStatusId);
                    table.ForeignKey(
                        name: "FK_SS_HistorialStatus_SS_Solicitudes_SS_SolicitudId",
                        column: x => x.SS_SolicitudId,
                        principalTable: "SS_Solicitudes",
                        principalColumn: "SS_SolicitudId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SS_HistorialComentarios_SS_SolicitudId",
                table: "SS_HistorialComentarios",
                column: "SS_SolicitudId");

            migrationBuilder.CreateIndex(
                name: "IX_SS_HistorialPrioridads_SS_SolicitudId",
                table: "SS_HistorialPrioridads",
                column: "SS_SolicitudId");

            migrationBuilder.CreateIndex(
                name: "IX_SS_HistorialStatus_SS_SolicitudId",
                table: "SS_HistorialStatus",
                column: "SS_SolicitudId");

            migrationBuilder.CreateIndex(
                name: "IX_SS_Solicitudes_SS_Servicio_solicitado_Id",
                table: "SS_Solicitudes",
                column: "SS_Servicio_solicitado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_SS_Solicitudes_SS_Solicitud_de_servicio_id",
                table: "SS_Solicitudes",
                column: "SS_Solicitud_de_servicio_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SS_ConActivosFijosSP");

            migrationBuilder.DropTable(
                name: "SS_HistorialComentarios");

            migrationBuilder.DropTable(
                name: "SS_HistorialPrioridads");

            migrationBuilder.DropTable(
                name: "SS_HistorialStatus");

            migrationBuilder.DropTable(
                name: "SS_ListEmpleadoAndUsers");

            migrationBuilder.DropTable(
                name: "SS_ListEmpleados");

            migrationBuilder.DropTable(
                name: "SS_showSolicitudes");

            migrationBuilder.DropTable(
                name: "SS_solicitudDetails");

            migrationBuilder.DropTable(
                name: "SS_Usuarios");

            migrationBuilder.DropTable(
                name: "UserDetailsResult");

            migrationBuilder.DropTable(
                name: "SS_Solicitudes");

            migrationBuilder.DropTable(
                name: "SS_Servicio_Solicitados");

            migrationBuilder.DropTable(
                name: "SS_Solicitud_De_Servicios");
        }
    }
}
