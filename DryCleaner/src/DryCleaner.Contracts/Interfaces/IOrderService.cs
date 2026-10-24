using DryCleaner.Contracts.Dtos;

namespace DryCleaner.Contracts.Interfaces;

public interface IOrderService : IApplicationService<OrderDto, OrderCreateDto, int>
{
    /// <summary>
    /// Получить заказы конкретного клиента
    /// </summary>
    public Task<IReadOnlyList<OrderDto>> GetByClientAsync(int clientId);
}