using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Interfaces;

namespace DryCleaner.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий для работы с заказами в памяти
/// </summary>
public class OrderInMemoryRepository : IOrderRepository
{
    private readonly List<Order> _orders = [];
    private int _nextId = 1;

    /// <summary>
    /// Создать новый заказ
    /// </summary>
    public Task<Order> Create(Order entity)
    {
        if (entity.Id == 0)
        {
            entity.Id = _nextId++;
        }
        else if (entity.Id >= _nextId)
        {
            _nextId = entity.Id + 1;
        }

        _orders.Add(entity);
        return Task.FromResult(entity);
    }

    /// <summary>
    /// Удалить заказ по id
    /// </summary>
    public Task<bool> Delete(int entityId)
    {
        var order = Read(entityId).Result;
        if (order == null)
            return Task.FromResult(false);

        var result = _orders.Remove(order);
        return Task.FromResult(result);
    }

    /// <summary>
    /// Получить заказ по id
    /// </summary>
    public Task<Order?> Read(int entityId) =>
        Task.FromResult(_orders.FirstOrDefault(o => o.Id == entityId));

    /// <summary>
    /// Получить список всех заказов
    /// </summary>
    public Task<IList<Order>> ReadAll() =>
        Task.FromResult<IList<Order>>([.. _orders]);

    /// <summary>
    /// Обновить существующий заказ
    /// </summary>
    public Task<Order> Update(Order entity)
    {
        Delete(entity.Id);
        Create(entity);
        return Task.FromResult(entity);
    }
}