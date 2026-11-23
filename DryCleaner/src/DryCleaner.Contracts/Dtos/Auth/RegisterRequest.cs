
using System.ComponentModel.DataAnnotations;

namespace DryCleaner.Contracts.Dtos.Auth;

/// <summary>
/// Данные для регистрации пользователя
/// </summary>
/// <param name="Username">Логин пользователя</param>
/// <param name="Password">Пароль пользователя</param>
public record RegisterRequest(
    [Required, StringLength(100, MinimumLength = 3)] string Username,
    [Required, MinLength(8)] string Password);
