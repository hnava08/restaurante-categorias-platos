using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestauranteCategoriasPlatos.Data;
using RestauranteCategoriasPlatos.Models;

namespace RestauranteCategoriasPlatos.Pages.Platos;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Plato> Platos { get; set; } = new List<Plato>();

    // Carga de datos mediante OnGet (requisito del enunciado)
    public async Task OnGetAsync()
    {
        Platos = await _context.Platos
            .Include(p => p.Categoria)
            .OrderBy(p => p.Nombre)
            .ToListAsync();
    }

    // Handler: asp-page-handler="Eliminar" -> OnPostEliminar
    public async Task<IActionResult> OnPostEliminarAsync(int id)
    {
        var plato = await _context.Platos.FindAsync(id);
        if (plato == null)
        {
            return NotFound();
        }

        _context.Platos.Remove(plato);
        await _context.SaveChangesAsync();

        TempData["Mensaje"] = $"Se eliminó el plato \"{plato.Nombre}\".";
        return RedirectToPage();
    }
}
