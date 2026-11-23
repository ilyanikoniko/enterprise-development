namespace DryCleaner.Domain.Entities;

/// <summary>
/// Пользователь системы
/// </summary>
public class User
{
    /// <summary>
    /// Уникальный идентификатор пользователя
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Логин пользователя
    /// </summary>
    public required string Username { get; set; }

    /// <summary>
    /// Хэш пароля пользователя
    /// </summary>
    public required string PasswordHash { get; set; }
}