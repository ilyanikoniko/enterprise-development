using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DryCleaner.Api.Controllers;

/// <summary>
/// Контроллер для аутентификации пользователей и выдачи JWT-токенов
/// </summary>
/// <param name="configuration">Конфигурация приложения</param>
[Route("api/[controller]")]
[ApiController]
public class AuthController(IConfiguration configuration) : ControllerBase
{
    /// <summary>
    /// Модель запроса на авторизацию
    /// </summary>
    /// <param name="Username">Имя пользователя</param>
    /// <param name="Password">Пароль</param>
    public record LoginRequest(string Username, string Password);

    /// <summary>
    /// Вход в систему и выдача JWT-токена при успешной проверке учетных данных
    /// </summary>
    /// <param name="request">Данные для входа (логин и пароль)</param>
    /// <returns>JWT-токен в случае успеха или статус 401 при неверных данных</returns>
    /// <response code="200">Успешная авторизация, возвращен токен</response>
    /// <response code="401">Неверный логин или пароль</response>
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (request.Username == "admin" && request.Password == "password")
        {
            var token = GenerateJwtToken(request.Username);
            return Ok(new { token });
        }
        return Unauthorized("Неверный логин или пароль");
    }

    /// <summary>
    /// Генерирует JWT-токен для указанного пользователя
    /// </summary>
    /// <param name="username">Имя пользователя, для которого создается токен</param>
    /// <returns>Строка с закодированным JWT-токеном</returns>
    private string GenerateJwtToken(string username)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, username),
            new Claim(ClaimTypes.Role, "Admin") // роль админа
        };

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}