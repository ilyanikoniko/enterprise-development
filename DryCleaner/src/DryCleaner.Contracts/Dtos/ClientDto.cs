namespace DryCleaner.Contracts.Dtos;

/// <summary>
/// DTO клиента для чтения
/// </summary>
public class ClientDto
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// ФИО клиента
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    public required string PhoneNumber { get; set; }
}