using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestauranteCategoriasPlatos.Data;
using RestauranteCategoriasPlatos.Models;

namespace RestauranteCategoriasPlatos.Pages.Platos;

public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public EditModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Plato Plato { get; set; } = new();

    public SelectList Categorias { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var plato = await _context.Platos.FindAsync(id);
        if (plato == null)
        {
            return NotFound();
        }

        Plato = plato;
        await CargarCategoriasAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        ModelState.Remove($"{nameof(Plato)}.{nameof(Plato.Categoria)}");

        if (!await _context.Categorias.AnyAsync(c => c.Id == Plato.CategoriaId))
        {
            ModelState.AddModelError($"{nameof(Plato)}.{nameof(Plato.CategoriaId)}",
                "Debe seleccionar una categoría válida.");
        }

        if (!ModelState.IsValid)
        {
            await CargarCategoriasAsync();
            return Page();
        }

        var platoDb = await _context.Platos.FindAsync(Plato.Id);
        if (platoDb == null)
        {
            return NotFound();
        }

        platoDb.Nombre = Plato.Nombre;
        platoDb.Descripcion = Plato.Descripcion;
        platoDb.Precio = Plato.Precio;
        platoDb.Disponible = Plato.Disponible;
        platoDb.CategoriaId = Plato.CategoriaId;

        await _context.SaveChangesAsync();

        TempData["Mensaje"] = $"Se actualizó el plato \"{platoDb.Nombre}\".";
        return RedirectToPage("./Index");
    }

    private async Task CargarCategoriasAsync()
    {
        Categorias = new SelectList(
            await _context.Categorias.OrderBy(c => c.Nombre).ToListAsync(),
            nameof(Categoria.Id),
            nameof(Categoria.Nombre),
            Plato.CategoriaId);
    }
}
