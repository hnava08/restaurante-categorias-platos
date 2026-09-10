using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestauranteCategoriasPlatos.Data;
using RestauranteCategoriasPlatos.Models;

namespace RestauranteCategoriasPlatos.Pages.Categorias;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Categoria> Categorias { get; set; } = new List<Categoria>();

    public async Task OnGetAsync()
    {
        Categorias = await _context.Categorias
            .Include(c => c.Platos)
            .OrderBy(c => c.Nombre)
            .ToListAsync();
    }
}
