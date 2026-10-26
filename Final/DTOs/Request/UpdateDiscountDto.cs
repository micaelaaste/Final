using System.ComponentModel.DataAnnotations;

namespace Final.DTOs.Request;

public class UpdateDiscountDto
{
    [Range(0, 100, ErrorMessage = "El descuento debe estar entre 0 y 100.")]
    public int Discount { get; set; }
}
