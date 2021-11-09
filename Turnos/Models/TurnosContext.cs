using Microsoft.EntityFrameworkCore;

namespace Turnos.Models
{
    public class TurnosContext : DbContext
    {
        public TurnosContext(DbContextOptions<TurnosContext> options) : base(options)
        {

        }

        // Agrego entidad -> TABLA
        public DbSet<EspecialidadModel> Especialidad { get; set; }
        public DbSet<PacienteModel> Paciente { get; set; }
        public DbSet<MedicoModel> Medico { get; set; }
        public DbSet<MedicoEspecialidadModel> MedicoEspecialidad { get; set; }
        public DbSet<TurnoModel> Turno { get; set; }
        public DbSet<LoginModel> Login { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EspecialidadModel>(entidad =>
            {
                entidad.ToTable("Especialidad"); // nombreTabla -> Especialidad

                entidad.HasKey(e => e.IdEspecialidad); // Primary Key idEspecialidad

                entidad.Property(e => e.Descripcion) // Agrego campo Descripcion y no acepta null 
                    .IsRequired() // no acepta null 
                    .HasMaxLength(200) // maximo de 200
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PacienteModel>(entidad =>
            {
                entidad.ToTable("Paciente");

                entidad.HasKey(p => p.IdPaciente);

                entidad.Property(p => p.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entidad.Property(p => p.Apellido)
                   .IsRequired()
                   .HasMaxLength(50)
                   .IsUnicode(false);
                entidad.Property(p => p.Direccion)
                   .IsRequired()
                   .HasMaxLength(250)
                   .IsUnicode(false);
                entidad.Property(p => p.Telefono)
                   .IsRequired()
                   .HasMaxLength(20)
                   .IsUnicode(false);
                entidad.Property(p => p.Email)
                   .IsRequired()
                   .HasMaxLength(100)
                   .IsUnicode(false);

            });

            modelBuilder.Entity<MedicoModel>(entidad =>
            {
                entidad.ToTable("Medico");

                entidad.HasKey(m => m.IdMedico);

                entidad.Property(m => m.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entidad.Property(m => m.Apellido)
                   .IsRequired()
                   .HasMaxLength(50)
                   .IsUnicode(false);
                entidad.Property(m => m.Direccion)
                   .IsRequired()
                   .HasMaxLength(250)
                   .IsUnicode(false);
                entidad.Property(m => m.Telefono)
                   .IsRequired()
                   .HasMaxLength(20)
                   .IsUnicode(false);
                entidad.Property(m => m.Email)
                   .IsRequired()
                   .HasMaxLength(100)
                   .IsUnicode(false);
                entidad.Property(m => m.HorarioAtencionDesde)
                   .IsRequired()
                   .IsUnicode(false);
                entidad.Property(m => m.HorarioAtencionHasta)
                   .IsRequired()
                   .IsUnicode(false);

            });

            modelBuilder.Entity<TurnoModel>(entidad =>
            {
                entidad.ToTable("Turno");

                entidad.HasKey(t => t.IdTurno);

                entidad.Property(t => t.IdMedico)
                   .IsRequired()
                   .IsUnicode(false);
                entidad.Property(t => t.IdPaciente)
                   .IsRequired()
                   .IsUnicode(false);
                entidad.Property(t => t.FechaHoraInicio)
                   .IsRequired()
                   .IsUnicode(false);
                entidad.Property(t => t.FechaHoraFin)
                   .IsRequired()
                   .IsUnicode(false);
            });

            modelBuilder.Entity<LoginModel>(entidad =>
            {
                entidad.ToTable("Login");

                entidad.HasKey(l => l.LoginId);

                entidad.Property(l => l.Usuario)
                    .IsRequired();
                entidad.Property(l => l.Password)
                    .IsRequired();
            });

            modelBuilder.Entity<MedicoEspecialidadModel>().HasKey(x => new { x.IdMedico, x.IdEspecialidad });
            /* RELACIONES 1:n */
            //MEDICO_ESPECIALIDAD  
            modelBuilder.Entity<MedicoEspecialidadModel>().HasOne(x => x.Medico)
                .WithMany(p => p.MedicoEspecilidad)
                .HasForeignKey(p => p.IdMedico);
            modelBuilder.Entity<MedicoEspecialidadModel>().HasOne(x => x.Especialidad)
                .WithMany(p => p.MedicoEspecilidad)
                .HasForeignKey(p => p.IdEspecialidad);

            // TURNO_MEDICO_PACIENTE 
            modelBuilder.Entity<TurnoModel>().HasOne(x => x.Medico)
               .WithMany(p => p.Turno)
               .HasForeignKey(p => p.IdMedico);
            modelBuilder.Entity<TurnoModel>().HasOne(x => x.Paciente)
                .WithMany(p => p.Turno)
                .HasForeignKey(p => p.IdPaciente);

        }

    }
}

