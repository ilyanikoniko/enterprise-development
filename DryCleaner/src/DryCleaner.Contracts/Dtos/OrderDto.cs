using DryCleaner.Domain.Enums;

namespace DryCleaner.Contracts.Dtos;

/// <summary>
/// DTO заказа (для чтения)
/// </summary>
public class OrderDto
{
    /// <summary>
    /// Идентификатор заказа
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public int ClientId { get; set; }

    /// <summary>
    /// ФИО клиента
    /// </summary>
    public required string ClientFullName { get; set; }

    /// <summary>
    /// Идентификатор изделия
    /// </summary>
    public int ItemId { get; set; }

    /// <summary>
    /// Наименование изделия
    /// </summary>
    public required string ItemName { get; set; }

    /// <summary>
    /// Дата приёма заказа
    /// </summary>
    public DateTime AcceptanceDate { get; set; }

    /// <summary>
    /// Срок выполнения в днях
    /// </summary>
    public int CompletionDays { get; set; }

    /// <summary>
    /// Статус заказа
    /// </summary>
    public OrderStatus Status { get; set; }
}