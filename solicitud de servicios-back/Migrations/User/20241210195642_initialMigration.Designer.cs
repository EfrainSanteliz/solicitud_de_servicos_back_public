﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using solicitud_de_servicios_back.Models;

#nullable disable

namespace solicitud_de_servicios_back.Migrations.User
{
    [DbContext(typeof(UserContext))]
    [Migration("20241210195642_initialMigration")]
    partial class initialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SocialMediaApi.Controllers.SS_ConActivosFijosSP", b =>
                {
                    b.Property<string>("AFClave")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("AFDescripcion")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("AFNombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("ActivoFijoID")
                        .HasColumnType("int");

                    b.ToTable("SS_ConActivosFijosSP");
                });

            modelBuilder.Entity("SocialMediaApi.Controllers.SS_showSolicitudes", b =>
                {
                    b.Property<string>("AFClave")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AFDescripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Archivo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DescripcionServicio_Solicitado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DescripcionSolicitud_De_Servicio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DireccionesDescripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Estatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaSolicitada")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirmaEmpleado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirmaJefe")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirmaJefeDepartamento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomEmpMaterno")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomEmpNombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomEmpPaterno")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Prioridad")
                        .HasColumnType("int");

                    b.Property<bool?>("RevisadoSub")
                        .HasColumnType("bit");

                    b.Property<int>("SS_Servicio_solicitado_Id")
                        .HasColumnType("int");

                    b.Property<int>("SS_SolicitudId")
                        .HasColumnType("int");

                    b.Property<int?>("UltimoStatus")
                        .HasColumnType("int");

                    b.ToTable("SS_showSolicitudes");
                });

            modelBuilder.Entity("SocialMediaApi.Controllers.SS_solicitudDetails", b =>
                {
                    b.Property<string>("Archivo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DescripcionServicio_Solicitado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DireccionesDescripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Estatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaSolicitada")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirmaEmpleado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomEmpMaterno")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomEmpNombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomEmpPaterno")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Prioridad")
                        .HasColumnType("int");

                    b.Property<bool?>("RevisadoSub")
                        .HasColumnType("bit");

                    b.Property<int>("SS_SolicitudId")
                        .HasColumnType("int");

                    b.Property<int?>("UltimoStatus")
                        .HasColumnType("int");

                    b.ToTable("SS_solicitudDetails");
                });

            modelBuilder.Entity("solicitud_de_servicios_back.Controllers.SS_ListEmpleados", b =>
                {
                    b.Property<int>("EmpleadoID")
                        .HasColumnType("int");

                    b.Property<bool?>("EstaActivo")
                        .HasColumnType("bit");

                    b.Property<string>("NomEmpMaterno")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomEmpNombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomEmpPaterno")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("SS_ListEmpleados");
                });

            modelBuilder.Entity("solicitud_de_servicios_back.Controllers.SS_UserDetailsResult", b =>
                {
                    b.Property<string>("DireccionesDescripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmpleadoID")
                        .HasColumnType("int");

                    b.Property<string>("NomEmpMaterno")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomEmpNombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomEmpPaterno")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SS_UsuarioId")
                        .HasColumnType("int");

                    b.Property<int>("UserRole")
                        .HasColumnType("int");

                    b.ToTable("UserDetailsResult");
                });

            modelBuilder.Entity("solicitud_de_servicios_back.Controllers.SS_listEmpleadoAndUsers", b =>
                {
                    b.Property<string>("DireccionesDescripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EmpleadoID")
                        .HasColumnType("int");

                    b.Property<string>("NomEmpMaterno")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomEmpNombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomEmpPaterno")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SS_UsuarioId")
                        .HasColumnType("int");

                    b.ToTable("SS_ListEmpleadoAndUsers");
                });

            modelBuilder.Entity("solicitud_de_servicios_back.Models.SS_HistorialComentarios", b =>
                {
                    b.Property<int>("SS_HistorialComentariosID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SS_HistorialComentariosID"));

                    b.Property<string>("Comentarios")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("Remitente")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SS_SolicitudId")
                        .HasColumnType("int");

                    b.HasKey("SS_HistorialComentariosID");

                    b.HasIndex("SS_SolicitudId");

                    b.ToTable("SS_HistorialComentarios");
                });

            modelBuilder.Entity("solicitud_de_servicios_back.Models.SS_HistorialPrioridad", b =>
                {
                    b.Property<int>("SS_HistorialPrioridadId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SS_HistorialPrioridadId"));

                    b.Property<DateTime?>("FechaPrioridad")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Prioridad")
                        .HasColumnType("int");

                    b.Property<string>("Quien")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SS_SolicitudId")
                        .HasColumnType("int");

                    b.HasKey("SS_HistorialPrioridadId");

                    b.HasIndex("SS_SolicitudId");

                    b.ToTable("SS_HistorialPrioridads");
                });

            modelBuilder.Entity("solicitud_de_servicios_back.Models.SS_HistorialStatus", b =>
                {
                    b.Property<int>("SS_HistorialStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SS_HistorialStatusId"));

                    b.Property<DateTime?>("FechaStatus")
                        .HasColumnType("datetime2");

                    b.Property<string>("Quien")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SS_SolicitudId")
                        .HasColumnType("int");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.HasKey("SS_HistorialStatusId");

                    b.HasIndex("SS_SolicitudId");

                    b.ToTable("SS_HistorialStatus");
                });

            modelBuilder.Entity("solicitud_de_servicios_back.Models.SS_Servicio_Solicitado", b =>
                {
                    b.Property<int>("SS_Servicio_solicitado_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SS_Servicio_solicitado_Id"));

                    b.Property<string>("DescripcionServicio_Solicitado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("HabilitadoServicio_Solicitado")
                        .HasColumnType("bit");

                    b.HasKey("SS_Servicio_solicitado_Id");

                    b.ToTable("SS_Servicio_Solicitados");
                });

            modelBuilder.Entity("solicitud_de_servicios_back.Models.SS_Solicitud", b =>
                {
                    b.Property<int>("SS_SolicitudId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SS_SolicitudId"));

                    b.Property<int?>("ActivoFijoID")
                        .HasColumnType("int");

                    b.Property<string>("Archivo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EmpleadoID")
                        .HasColumnType("int");

                    b.Property<int?>("Estatus")
                        .HasColumnType("int");

                    b.Property<DateTime?>("FechaSolicitada")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirmaEmpleado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirmaJefe")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirmaJefeDepartamento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Prioridad")
                        .HasColumnType("int");

                    b.Property<bool?>("RevisadoSub")
                        .HasColumnType("bit");

                    b.Property<int?>("SS_Servicio_solicitado_Id")
                        .HasColumnType("int");

                    b.Property<int?>("SS_Solicitud_de_servicio_id")
                        .HasColumnType("int");

                    b.Property<int?>("ServicioSolicitado")
                        .HasColumnType("int");

                    b.Property<int?>("UltimoStatus")
                        .HasColumnType("int");

                    b.HasKey("SS_SolicitudId");

                    b.HasIndex("SS_Servicio_solicitado_Id");

                    b.HasIndex("SS_Solicitud_de_servicio_id");

                    b.ToTable("SS_Solicitudes");
                });

            modelBuilder.Entity("solicitud_de_servicios_back.Models.SS_Solicitud_de_servicio", b =>
                {
                    b.Property<int>("SS_Solicitud_de_servicio_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SS_Solicitud_de_servicio_id"));

                    b.Property<string>("DescripcionSolicitud_De_Servicio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Habilitado")
                        .HasColumnType("bit");

                    b.HasKey("SS_Solicitud_de_servicio_id");

                    b.ToTable("SS_Solicitud_De_Servicios");
                });

            modelBuilder.Entity("solicitud_de_servicios_back.Models.SS_Usuarios", b =>
                {
                    b.Property<int>("SS_UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SS_UsuarioId"));

                    b.Property<string>("CodigoParaRestablecerContraseña")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EmpleadoID")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserRole")
                        .HasColumnType("int");

                    b.HasKey("SS_UsuarioId");

                    b.ToTable("SS_Usuarios");
                });

            modelBuilder.Entity("solicitud_de_servicios_back.Models.SS_HistorialComentarios", b =>
                {
                    b.HasOne("solicitud_de_servicios_back.Models.SS_Solicitud", "SS_Solicitudes")
                        .WithMany("HistorialComentarios")
                        .HasForeignKey("SS_SolicitudId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SS_Solicitudes");
                });

            modelBuilder.Entity("solicitud_de_servicios_back.Models.SS_HistorialPrioridad", b =>
                {
                    b.HasOne("solicitud_de_servicios_back.Models.SS_Solicitud", "SS_Solicitudes")
                        .WithMany("HistorialPrioridads")
                        .HasForeignKey("SS_SolicitudId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SS_Solicitudes");
                });

            modelBuilder.Entity("solicitud_de_servicios_back.Models.SS_HistorialStatus", b =>
                {
                    b.HasOne("solicitud_de_servicios_back.Models.SS_Solicitud", "SS_Solicitudes")
                        .WithMany("HistorialStatus")
                        .HasForeignKey("SS_SolicitudId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SS_Solicitudes");
                });

            modelBuilder.Entity("solicitud_de_servicios_back.Models.SS_Solicitud", b =>
                {
                    b.HasOne("solicitud_de_servicios_back.Models.SS_Servicio_Solicitado", "SS_Servicio_Solicitados")
                        .WithMany("SS_Solicitudes")
                        .HasForeignKey("SS_Servicio_solicitado_Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("solicitud_de_servicios_back.Models.SS_Solicitud_de_servicio", "SS_Solicitud_de_servicios")
                        .WithMany("SS_Solicitudes")
                        .HasForeignKey("SS_Solicitud_de_servicio_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("SS_Servicio_Solicitados");

                    b.Navigation("SS_Solicitud_de_servicios");
                });

            modelBuilder.Entity("solicitud_de_servicios_back.Models.SS_Servicio_Solicitado", b =>
                {
                    b.Navigation("SS_Solicitudes");
                });

            modelBuilder.Entity("solicitud_de_servicios_back.Models.SS_Solicitud", b =>
                {
                    b.Navigation("HistorialComentarios");

                    b.Navigation("HistorialPrioridads");

                    b.Navigation("HistorialStatus");
                });

            modelBuilder.Entity("solicitud_de_servicios_back.Models.SS_Solicitud_de_servicio", b =>
                {
                    b.Navigation("SS_Solicitudes");
                });
#pragma warning restore 612, 618
        }
    }
}
