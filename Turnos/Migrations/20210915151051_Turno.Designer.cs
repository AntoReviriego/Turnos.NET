// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Turnos.Models;

namespace Turnos.Migrations
{
    [DbContext(typeof(TurnosContext))]
    [Migration("20210915151051_Turno")]
    partial class Turno
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Turnos.Models.EspecialidadModel", b =>
                {
                    b.Property<int>("IdEspecialidad")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)");

                    b.HasKey("IdEspecialidad");

                    b.ToTable("Especialidad");
                });

            modelBuilder.Entity("Turnos.Models.MedicoEspecialidadModel", b =>
                {
                    b.Property<int>("IdMedico")
                        .HasColumnType("int");

                    b.Property<int>("IdEspecialidad")
                        .HasColumnType("int");

                    b.HasKey("IdMedico", "IdEspecialidad");

                    b.HasIndex("IdEspecialidad");

                    b.ToTable("MedicoEspecialidad");
                });

            modelBuilder.Entity("Turnos.Models.MedicoModel", b =>
                {
                    b.Property<int>("IdMedico")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("HorarioAtencionDesde")
                        .IsUnicode(false)
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("HorarioAtencionHasta")
                        .IsUnicode(false)
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.HasKey("IdMedico");

                    b.ToTable("Medico");
                });

            modelBuilder.Entity("Turnos.Models.PacienteModel", b =>
                {
                    b.Property<int>("IdPaciente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.HasKey("IdPaciente");

                    b.ToTable("Paciente");
                });

            modelBuilder.Entity("Turnos.Models.TurnoModel", b =>
                {
                    b.Property<int>("IdTurno")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("FechaHoraFin")
                        .IsUnicode(false)
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaHoraInicio")
                        .IsUnicode(false)
                        .HasColumnType("datetime2");

                    b.Property<int>("IdMedico")
                        .IsUnicode(false)
                        .HasColumnType("int");

                    b.Property<int>("IdPaciente")
                        .IsUnicode(false)
                        .HasColumnType("int");

                    b.HasKey("IdTurno");

                    b.HasIndex("IdMedico");

                    b.HasIndex("IdPaciente");

                    b.ToTable("Turno");
                });

            modelBuilder.Entity("Turnos.Models.MedicoEspecialidadModel", b =>
                {
                    b.HasOne("Turnos.Models.EspecialidadModel", "Especialidad")
                        .WithMany("MedicoEspecilidad")
                        .HasForeignKey("IdEspecialidad")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Turnos.Models.MedicoModel", "Medico")
                        .WithMany("MedicoEspecilidad")
                        .HasForeignKey("IdMedico")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Especialidad");

                    b.Navigation("Medico");
                });

            modelBuilder.Entity("Turnos.Models.TurnoModel", b =>
                {
                    b.HasOne("Turnos.Models.MedicoModel", "Medico")
                        .WithMany("Turno")
                        .HasForeignKey("IdMedico")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Turnos.Models.PacienteModel", "Paciente")
                        .WithMany("Turno")
                        .HasForeignKey("IdPaciente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medico");

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("Turnos.Models.EspecialidadModel", b =>
                {
                    b.Navigation("MedicoEspecilidad");
                });

            modelBuilder.Entity("Turnos.Models.MedicoModel", b =>
                {
                    b.Navigation("MedicoEspecilidad");

                    b.Navigation("Turno");
                });

            modelBuilder.Entity("Turnos.Models.PacienteModel", b =>
                {
                    b.Navigation("Turno");
                });
#pragma warning restore 612, 618
        }
    }
}
