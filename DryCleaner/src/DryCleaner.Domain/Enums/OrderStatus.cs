namespace DryCleaner.Domain.Enums;

/// <summary>
/// Статус заказа в химчистке
/// </summary>
public enum OrderStatus
{
    /// <summary>
    /// Заказ принят
    /// </summary>
    Accepted = 0,

    /// <summary>
    /// Заказ в обработке
    /// </summary>
    InProgress = 1,

    /// <summary>
    /// Заказ готов к выдаче
    /// </summary>
    Ready = 2,

    /// <summary>
    /// Заказ завершён
    /// </summary>
    Completed = 3,

    /// <summary>
    /// Заказ отменён
    /// </summary>
    Cancelled = 4
}