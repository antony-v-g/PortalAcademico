using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalAcademico.Data;
using PortalAcademico.Models;

public class CursosController : Controller
{
    private readonly ApplicationDbContext _context;

    public CursosController(ApplicationDbContext context)
    {
        _context = context;
    }

    // 📌 LISTADO CON FILTROS
    public async Task<IActionResult> Index(string nombre, int? minCred, int? maxCred)
    {
        var cursos = _context.Cursos.Where(c => c.Activo);

        if (!string.IsNullOrEmpty(nombre))
            cursos = cursos.Where(c => c.Nombre.Contains(nombre));

        if (minCred.HasValue)
            cursos = cursos.Where(c => c.Creditos >= minCred);

        if (maxCred.HasValue)
            cursos = cursos.Where(c => c.Creditos <= maxCred);

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

        // 🔥 GUARDAR ÚLTIMO CURSO EN SESIÓN (esto es lo nuevo)
        HttpContext.Session.SetString("ultimoCurso", curso.Nombre);

        return View(curso);
    }
}