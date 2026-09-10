using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestauranteCategoriasPlatos.Models;

public class Plato
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre del plato es obligatorio.")]
    [Display(Name = "Nombre")]
    [StringLength(150)]
    public string Nombre { get; set; } = string.Empty;

    [Display(Name = "Descripción")]
    [StringLength(400)]
    public string? Descripcion { get; set; }

    [Required(ErrorMessage = "El precio es obligatorio.")]
    [Display(Name = "Precio")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que 0.")]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Precio { get; set; }

    [Display(Name = "Disponible")]
    public bool Disponible { get; set; } = true;

    // Clave foránea hacia Categoría
    [Required(ErrorMessage = "Debe seleccionar una categoría.")]
    [Display(Name = "Categoría")]
    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una categoría válida.")]
    public int CategoriaId { get; set; }

    [ForeignKey(nameof(CategoriaId))]
    public Categoria? Categoria { get; set; }
}
