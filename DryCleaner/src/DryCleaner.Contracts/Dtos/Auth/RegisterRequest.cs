using System.ComponentModel.DataAnnotations;

namespace DryCleaner.Contracts.Dtos.Auth;

/// <summary>
/// Данные для регистрации пользователя
/// </summary>
public record RegisterRequest
{
    /// <summary>
    /// Логин пользователя
    /// </summary>
    [StringLength(100, MinimumLength = 3)]
    public required string Username { get; init; }

    /// <summary>
    /// Пароль пользователя
    /// </summary>
    [MinLength(8)]
    public required string Password { get; init; }
}