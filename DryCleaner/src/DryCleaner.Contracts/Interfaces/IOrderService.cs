using DryCleaner.Contracts.Dtos;

namespace DryCleaner.Contracts.Interfaces;

/// <summary>
/// Сервис для работы с заказами
/// </summary>
public interface IOrderService
{
    /// <summary>
    /// Получить все заказы
    /// </summary>
    Task<IReadOnlyList<OrderDto>> GetAllAsync();

    /// <summary>
    /// Получить заказ по идентификатору
    /// </summary>
    Task<OrderDto?> GetByIdAsync(int id);

    /// <summary>
    /// Создать заказ
    /// </summary>
    Task<OrderDto> CreateAsync(OrderCreateDto dto);

    /// <summary>
    /// Обновить заказ
    /// </summary>
    Task<OrderDto?> UpdateAsync(int id, OrderUpdateDto dto);

    /// <summary>
    /// Удалить заказ
    /// </summary>
    Task<bool> DeleteAsync(int id);
}