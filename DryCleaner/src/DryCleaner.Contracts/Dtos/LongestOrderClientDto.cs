namespace DryCleaner.Contracts.Dtos;

/// <summary>
/// DTO для клиентов с самыми долгими заказами
/// </summary>
public class LongestOrderClientDto
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public int ClientId { get; set; }

    /// <summary>
    /// ФИО клиента
    /// </summary>
    public required string ClientFullName { get; set; }

    /// <summary>
    /// Максимальный срок выполнения заказа в днях
    /// </summary>
    public int MaxCompletionDays { get; set; }
}