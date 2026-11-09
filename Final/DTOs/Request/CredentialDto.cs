using System.ComponentModel.DataAnnotations;

namespace Final.DTOs.Request;

public class CredentialDto
{
    [Required(ErrorMessage = "El email es obligatorio.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    public string Password { get; set; } = string.Empty;
}