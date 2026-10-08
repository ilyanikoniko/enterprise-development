using System.ComponentModel.DataAnnotations;
using DryCleaner.Domain.Enums;

namespace DryCleaner.Contracts.Dtos;

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
    /// Дата приёма заказа
    /// </summary>
    [Range(typeof(DateTime), "2020-01-01", "2030-12-31",
       ErrorMessage = "Дата должна быть в диапазоне 2020–2030")]
    public DateTime AcceptanceDate { get; set; }

    /// <summary>
    /// Срок выполнения в днях
    /// </summary>
    [Range(1, 365, ErrorMessage = "Срок должен быть от 1 до 365 дней")]
    public int CompletionDays { get; set; }

    /// <summary>
    /// Статус заказа
    /// </summary>
    [Required(ErrorMessage = "Статус обязателен")]
    public OrderStatus Status { get; set; }
}