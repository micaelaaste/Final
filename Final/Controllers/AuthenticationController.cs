using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Final.DTOs.Requests;
using Final.Entities;
using Final.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Final.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly IUserService _userService;

    public AuthenticationController(IConfiguration config, IUserService userService)
    {
        _config = config;
        _userService = userService;
    }

    [HttpPost]
    public IActionResult Authenticate(CredentialDto credentials)
    {
        User? user = _userService.Authenticate(credentials.Email, credentials.Password);

        if (user is null)
        {
            return Unauthorized("Email o contraseña incorrectos.");
        }

        var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Authentication:SecretForKey"]!));
        var signature = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

        var claimsForToken = new List<Claim>
        {
            new Claim("sub", user.Id.ToString()),
            new Claim("restaurantName", user.RestaurantName)
        };

        var jwtSecurityToken = new JwtSecurityToken(
            _config["Authentication:Issuer"],    // quién lo emite
            _config["Authentication:Audience"],  // para quién es
            claimsForToken,                      // claims personalizados
            DateTime.UtcNow,                     // desde cuándo es válido
            DateTime.UtcNow.AddHours(1),         // hasta cuándo (expiración)
            signature);                          // la firma

        var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        return Ok(tokenToReturn);
    }
}