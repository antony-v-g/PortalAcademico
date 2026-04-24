using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PortalAcademico.Models;

namespace PortalAcademico.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // 🔒 Índice único en Código de Curso
            builder.Entity<Curso>()
                .HasIndex(c => c.Codigo)
                .IsUnique();

            // 🔒 Índice único en Matrícula (Curso + Usuario)
            builder.Entity<Matricula>()
                .HasIndex(m => new { m.CursoId, m.UsuarioId })
                .IsUnique();

            // 🔒 Restricciones (CHECK)
            builder.Entity<Curso>()
                .ToTable(t =>
                {
                    t.HasCheckConstraint("CK_Creditos", "Creditos > 0");
                    t.HasCheckConstraint("CK_Horario", "HorarioInicio < HorarioFin");
                });

            // 🌱 SEED DE DATOS (IMPORTANTE PARA EL EXAMEN)
            builder.Entity<Curso>().HasData(
                new Curso
                {
                    Id = 1,
                    Codigo = "CS101",
                    Nombre = "Programación",
                    Creditos = 3,
                    CupoMaximo = 30,
                    HorarioInicio = new DateTime(2024, 1, 1, 8, 0, 0),
                    HorarioFin = new DateTime(2024, 1, 1, 10, 0, 0),
                    Activo = true
                },
                new Curso
                {
                    Id = 2,
                    Codigo = "MAT101",
                    Nombre = "Matemática",
                    Creditos = 4,
                    CupoMaximo = 25,
                    HorarioInicio = new DateTime(2024, 1, 1, 10, 0, 0),
                    HorarioFin = new DateTime(2024, 1, 1, 12, 0, 0),
                    Activo = true
                },
                new Curso
                {
                    Id = 3,
                    Codigo = "FIS101",
                    Nombre = "Física",
                    Creditos = 3,
                    CupoMaximo = 20,
                    HorarioInicio = new DateTime(2024, 1, 1, 14, 0, 0),
                    HorarioFin = new DateTime(2024, 1, 1, 16, 0, 0),
                    Activo = true
                }
            );
        }
    }
}