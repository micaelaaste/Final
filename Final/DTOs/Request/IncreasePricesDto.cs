using System.ComponentModel.DataAnnotations;

namespace Final.DTOs.Request;

public class IncreasePricesDto
{
    [Range(0.01, 1000, ErrorMessage = "El porcentaje debe ser mayor a 0.")]
    public decimal Percentage { get; set; }
}
