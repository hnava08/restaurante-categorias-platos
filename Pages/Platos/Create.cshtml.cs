using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestauranteCategoriasPlatos.Data;
using RestauranteCategoriasPlatos.Models;

namespace RestauranteCategoriasPlatos.Pages.Platos;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public CreateModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Plato Plato { get; set; } = new();

    public SelectList Categorias { get; set; } = default!;

    public async Task OnGetAsync()
    {
        await CargarCategoriasAsync();
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

        _context.Platos.Add(Plato);
        await _context.SaveChangesAsync();

        TempData["Mensaje"] = $"Se registró el plato \"{Plato.Nombre}\".";
        return RedirectToPage("./Index");
    }

    private async Task CargarCategoriasAsync()
    {
        Categorias = new SelectList(
            await _context.Categorias.OrderBy(c => c.Nombre).ToListAsync(),
            nameof(Categoria.Id),
            nameof(Categoria.Nombre));
    }
}
