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
    public Task<IReadOnlyList<OrderDto>> GetAllAsync();

    /// <summary>
    /// Получить заказ по идентификатору
    /// </summary>
    public Task<OrderDto?> GetByIdAsync(int id);

    /// <summary>
    /// Создать заказ
    /// </summary>
    public Task<OrderDto> CreateAsync(OrderCreateDto dto);

    /// <summary>
    /// Обновить заказ
    /// </summary>
    public Task<OrderDto?> UpdateAsync(int id, OrderUpdateDto dto);

    /// <summary>
    /// Удалить заказ
    /// </summary>
    public Task<bool> DeleteAsync(int id);
}