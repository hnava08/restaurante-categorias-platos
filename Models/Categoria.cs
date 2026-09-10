using System.ComponentModel.DataAnnotations;

namespace RestauranteCategoriasPlatos.Models;

public class Categoria
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre de la categoría es obligatorio.")]
    [Display(Name = "Nombre")]
    [StringLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Display(Name = "Descripción")]
    [StringLength(300)]
    public string? Descripcion { get; set; }

    // Relación uno a muchos: una categoría puede tener muchos platos
    public List<Plato> Platos { get; set; } = new();
}
