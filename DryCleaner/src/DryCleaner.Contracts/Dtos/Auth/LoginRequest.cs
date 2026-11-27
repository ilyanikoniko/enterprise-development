namespace DryCleaner.Contracts.Dtos.Auth;

/// <summary>
/// DTO запроса на авторизацию
/// </summary>
public record LoginRequest
{
    /// <summary>
    /// Логин пользователя
    /// </summary>
    public required string Username { get; init; }

    /// <summary>
    /// Пароль пользователя
    /// </summary>
    public required string Password { get; init; }
}