using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

public class Matricula
{
    public int Id { get; set; }

    public int CursoId { get; set; }
    public Curso? Curso { get; set; }   // 👈 nullable

    public string UsuarioId { get; set; } = string.Empty;
    public IdentityUser? Usuario { get; set; }  // 👈 nullable

    public DateTime FechaRegistro { get; set; }

    public string Estado { get; set; } = string.Empty;
}