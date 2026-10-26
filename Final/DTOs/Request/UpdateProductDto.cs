using System.ComponentModel.DataAnnotations;

namespace Final.DTOs.Request;

public class UpdateProductDto
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres.")]
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    [Range(0.01, 10000000, ErrorMessage = "El precio debe ser mayor a 0.")]
    public decimal Price { get; set; }

    public bool IsFavourite { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "La categoría es obligatoria.")]
    public int CategoryId { get; set; }
}
