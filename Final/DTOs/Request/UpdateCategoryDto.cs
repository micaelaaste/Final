using System.ComponentModel.DataAnnotations;

namespace Final.DTOs.Request;

public class UpdateCategoryDto
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 50 caracteres.")]
    public string Name { get; set; } = string.Empty;
}
