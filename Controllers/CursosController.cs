using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http; // 👈 NECESARIO para Session
using PortalAcademico.Data;
using PortalAcademico.Models;
using System.Linq;
using System.Threading.Tasks;

public class CursosController : Controller
{
    private readonly ApplicationDbContext _context;

    public CursosController(ApplicationDbContext context)
    {
        _context = context;
    }

    // 📌 LISTADO CON FILTROS
    public async Task<IActionResult> Index(string? nombre, int? minCred, int? maxCred)
    {
        var cursos = _context.Cursos.Where(c => c.Activo);

        if (!string.IsNullOrEmpty(nombre))
            cursos = cursos.Where(c => c.Nombre.Contains(nombre));

        if (minCred.HasValue)
            cursos = cursos.Where(c => c.Creditos >= minCred.Value);

        if (maxCred.HasValue)
            cursos = cursos.Where(c => c.Creditos <= maxCred.Value);

        return View(await cursos.ToListAsync());
    }

    // 📌 DETALLE DEL CURSO
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var curso = await _context.Cursos
            .FirstOrDefaultAsync(c => c.Id == id);

        if (curso == null)
            return NotFound();

        // 🔥 GUARDAR ÚLTIMO CURSO EN SESIÓN
        HttpContext.Session.SetString("ultimoCurso", curso.Nombre);

        return View(curso);
    }
}