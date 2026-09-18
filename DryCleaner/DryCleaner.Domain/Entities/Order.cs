using DryCleaner.Domain.Enums;

namespace DryCleaner.Domain.Entities;

/// <summary>
/// Заказ на химчистку 
/// </summary>
public class Order
{
    /// <summary>
    /// Идентификатор заказа
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public required int ClientId { get; set; }

    /// <summary>
    /// Объект клиент
    /// </summary>
    public Client? Client { get; set; }

    /// <summary>
    /// Идентификатор изделия
    /// </summary>
    public required int ItemId { get; set; }

    /// <summary>
    /// Объект изделие
    /// </summary>
    public Item? Item { get; set; }

    /// <summary>
    /// Дата приёма заказа
    /// </summary>
    public required DateTime AcceptanceDate { get; set; }

    /// <summary>
    /// Срок выполнения в днях
    /// </summary>
    public int CompletionDays { get; set; }

    /// <summary>
    /// Текущий статус заказа
    /// </summary>
    public OrderStatus Status { get; set; }
}