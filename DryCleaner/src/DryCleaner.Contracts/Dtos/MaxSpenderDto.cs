namespace DryCleaner.Contracts.Dtos;

/// <summary>
/// DTO для клиента, потратившего наибольшую сумму
/// </summary>
public class MaxSpenderDto
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
    /// Общая потраченная сумма
    /// </summary>
    public decimal TotalSpent { get; set; }
}