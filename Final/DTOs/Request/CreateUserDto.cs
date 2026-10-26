using System.ComponentModel.DataAnnotations;

namespace Final.DTOs.Request
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Name { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "El email no tiene un formato válido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre del restaurante es obligatorio.")]
        public string RestaurantName { get; set; } = string.Empty;

        [Required(ErrorMessage = "La dirección es obligatoria.")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [MinLength(8, ErrorMessage = "El numero de telefono debe tener al menos 8 caracteres.")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
