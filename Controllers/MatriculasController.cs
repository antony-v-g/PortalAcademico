using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PortalAcademico.Data;
using PortalAcademico.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

[Authorize]
public class MatriculasController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public MatriculasController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Inscribirse(int cursoId)
    {
        // 🔹 Validar usuario autenticado
        var userId = _userManager.GetUserId(User);
        if (userId == null)
            return Unauthorized();

        // 🔹 Obtener curso (forma correcta sin warning)
        var curso = await _context.Cursos
            .FirstOrDefaultAsync(c => c.Id == cursoId);

        if (curso == null)
            return NotFound();

        // 🔹 Validar cupo
        int inscritos = _context.Matriculas.Count(m => m.CursoId == cursoId);
        if (inscritos >= curso.CupoMaximo)
            return Content("Cupo lleno");

        // 🔹 Validar si ya está inscrito
        bool existe = _context.Matriculas
            .Any(m => m.CursoId == cursoId && m.UsuarioId == userId);

        if (existe)
            return Content("Ya inscrito");

        // 🔹 Crear matrícula
        var matricula = new Matricula
        {
            CursoId = cursoId,
            UsuarioId = userId,
            FechaRegistro = DateTime.Now,
            Estado = "Pendiente"
        };

        _context.Add(matricula);
        await _context.SaveChangesAsync();

        // 🔹 Redirección final
        return RedirectToAction("Index", "Cursos");
    }
}