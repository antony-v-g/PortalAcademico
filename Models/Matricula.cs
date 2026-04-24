using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PortalAcademico.Models
{
    public class Matricula
    {
        public int Id { get; set; }

        public int CursoId { get; set; }
        public Curso Curso { get; set; } = null!; // 👈 IMPORTANTE

        public string UsuarioId { get; set; } = string.Empty;
        public IdentityUser Usuario { get; set; } = null!;

        public DateTime FechaRegistro { get; set; }

        public string Estado { get; set; } = "Pendiente";
    }
}