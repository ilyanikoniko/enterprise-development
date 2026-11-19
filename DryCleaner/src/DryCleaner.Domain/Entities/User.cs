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
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Хэш пароля пользователя
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;
}