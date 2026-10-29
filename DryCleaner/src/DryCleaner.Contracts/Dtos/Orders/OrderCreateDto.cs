using System.ComponentModel.DataAnnotations;

namespace DryCleaner.Contracts.Dtos.Orders;

/// <summary>
/// DTO заказа (для создания)
/// </summary>
public class OrderCreateDto
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Выберите клиента")]
    public int ClientId { get; set; }

    /// <summary>
    /// Идентификатор изделия
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Выберите изделие")]
    public int ItemId { get; set; }

    /// <summary>
    /// Срок выполнения в днях
    /// </summary>
    [Range(1, 365, ErrorMessage = "Срок должен быть от 1 до 365 дней")]
    public int CompletionDays { get; set; }

    /// <summary>
    /// Статус заказа
    /// </summary>
    [Required(ErrorMessage = "Статус обязателен")]
    public required string Status { get; set; }
}