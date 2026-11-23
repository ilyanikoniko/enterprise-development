
using System.ComponentModel.DataAnnotations;

namespace DryCleaner.Contracts.Dtos.Auth;

/// <summary>
/// Данные для авторизации пользователя
/// </summary>
/// <param name="Username">Логин пользователя</param>
/// <param name="Password">Пароль пользователя</param>
public record LoginRequest(
    [Required] string Username,
    [Required] string Password);
