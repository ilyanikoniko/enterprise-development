namespace DryCleaner.Domain.Entities;

/// <summary>
/// Клиент химчистки
/// </summary>
public class Client
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// ФИО клиента
    /// </summary>
    /// <example>Иванов Павел Михайлович</example>
    public required string FullName { get; set; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    public string? PhoneNumber { get; set; }
}