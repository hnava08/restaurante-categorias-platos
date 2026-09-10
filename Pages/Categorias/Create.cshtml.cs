using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestauranteCategoriasPlatos.Data;
using RestauranteCategoriasPlatos.Models;

namespace RestauranteCategoriasPlatos.Pages.Categorias;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public CreateModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Categoria Categoria { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        ModelState.Remove($"{nameof(Categoria)}.{nameof(Categoria.Platos)}");

        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Categorias.Add(Categoria);
        await _context.SaveChangesAsync();

        TempData["Mensaje"] = $"Se registró la categoría \"{Categoria.Nombre}\".";
        return RedirectToPage("./Index");
    }
}
