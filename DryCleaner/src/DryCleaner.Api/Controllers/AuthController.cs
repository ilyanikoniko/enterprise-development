
using DryCleaner.Domain.Entities;
using DryCleaner.Infrastructure.EFCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DryCleaner.Api.Controllers;

/// <summary>
/// Контроллер регистрации и авторизации пользователей
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AuthController(
    IConfiguration configuration,
    IPasswordHasher<string> passwordHasher,
    DryCleanerDbContext dbContext)
    : ControllerBase
{
    /// <summary>
    /// Данные для регистрации пользователя
    /// </summary>
    public record RegisterRequest(
        [Required, StringLength(100, MinimumLength = 3)] string Username,
        [Required, MinLength(8)] string Password);

    /// <summary>
    /// Данные для авторизации пользователя
    /// </summary>
    public record LoginRequest(
        [Required] string Username,
        [Required] string Password);

    /// <summary>
    /// Регистрация нового пользователя
    /// </summary>
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequest request)
    {
        var exists = await dbContext.Users
            .AnyAsync(u => u.Username == request.Username);

        if (exists)
            return Conflict("Пользователь с таким логином уже существует");

        var user = new User
        {
            Username = request.Username,
            PasswordHash = passwordHasher.HashPassword(
                request.Username,
                request.Password)
        };

        dbContext.Users.Add(user);

        try
        {
            await dbContext.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            // Уникальный индекс защищает и от одновременной
            // регистрации двух пользователей с одним логином.
            if (await dbContext.Users.AsNoTracking()
                .AnyAsync(u => u.Username == request.Username))
            {
                return Conflict("Пользователь с таким логином уже существует");
            }

            throw;
        }

        return StatusCode(201, new
        {
            user.Id,
            user.Username
        });
    }

    /// <summary>
    /// Авторизация пользователя и выдача JWT-токена
    /// </summary>
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request)
    {
        var user = await dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Username == request.Username);

        if (user == null)
            return Unauthorized("Неверный логин или пароль");

        var result = passwordHasher.VerifyHashedPassword(
            user.Username,
            user.PasswordHash,
            request.Password);

        if (result == PasswordVerificationResult.Failed)
            return Unauthorized("Неверный логин или пароль");

        var token = GenerateJwtToken(user.Username);

        return Ok(new { token });
    }

    /// <summary>
    /// Генерация JWT-токена
    /// </summary>
    private string GenerateJwtToken(string username)
    {
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));

        var credentials = new SigningCredentials(
            securityKey,
            SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, username),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
