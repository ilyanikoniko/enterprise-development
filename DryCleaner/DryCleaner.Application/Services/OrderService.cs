using DryCleaner.Application.Mappers;
using DryCleaner.Contracts.Dtos;
using DryCleaner.Contracts.Interfaces;
using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Interfaces;

namespace DryCleaner.Application.Services;

/// <summary>
/// Сервис для управления заказами
/// </summary>
public class OrderService(IRepository<Order, int> repository)
    : IOrderService
{
    /// <summary>
    /// Создать новый заказ
    /// </summary>
    public async Task<OrderDto> Create(OrderCreateDto dto)
    {
        var order = Mapper.Map(dto);
        var result = await repository.Create(order);
        return Mapper.Map(result);
    }

    /// <summary>
    /// Получить заказ по id
    /// </summary>
    public async Task<OrderDto?> Get(int id)
    {
        var order = await repository.Read(id);
        return order == null ? null : Mapper.Map(order);
    }

    /// <summary>
    /// Получить список всех заказов
    /// </summary>
    public async Task<IList<OrderDto>> GetAll()
    {
        var orders = await repository.ReadAll();
        return Mapper.Map(orders);
    }

    /// <summary>
    /// Обновить существующий заказ
    /// </summary>
    public async Task<OrderDto> Update(OrderCreateDto dto, int id)
    {
        var updatedOrder = Mapper.Map(dto);
        updatedOrder.Id = id;
        var result = await repository.Update(updatedOrder);
        return Mapper.Map(result);
    }

    /// <summary>
    /// Удалить заказ по id
    /// </summary>
    public async Task<bool> Delete(int id) => await repository.Delete(id);

    /// <summary>
    /// Получить список заказов конкретного клиента
    /// </summary>
    public async Task<IReadOnlyList<OrderDto>> GetByClientAsync(int clientId)
    {
        var orders = await repository.ReadAll();
        return orders
            .Where(o => o.ClientId == clientId)
            .Select(Mapper.Map)
            .ToList();
    }
}