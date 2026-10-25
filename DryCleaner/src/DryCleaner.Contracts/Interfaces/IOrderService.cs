using DryCleaner.Contracts.Dtos;

namespace DryCleaner.Contracts.Interfaces;

/// <summary>
/// Интерфейс сервиса для работы с заказами, расширяющий базовый CRUD-функционал
/// </summary>
public interface IOrderService : IApplicationService<OrderDto, OrderCreateDto, int>
{
    /// <summary>
    /// Получить заказы конкретного клиента
    /// </summary>
    public Task<IReadOnlyList<OrderDto>> GetByClientAsync(int clientId);
}