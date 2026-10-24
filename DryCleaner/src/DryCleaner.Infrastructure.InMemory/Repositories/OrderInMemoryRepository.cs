using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Interfaces;

namespace DryCleaner.Infrastructure.InMemory.Repositories;

public class OrderInMemoryRepository : IRepository<Order, int>
{
    private readonly List<Order> _orders = [];

    public Task<Order> Create(Order entity)
    {
        _orders.Add(entity);
        return Task.FromResult(entity);
    }

    public Task<bool> Delete(int entityId)
    {
        var order = Read(entityId).Result;
        if (order == null)
            return Task.FromResult(false);

        var result = _orders.Remove(order);
        return Task.FromResult(result);
    }

    public Task<Order?> Read(int entityId) =>
        Task.FromResult(_orders.FirstOrDefault(o => o.Id == entityId));

    public Task<IList<Order>> ReadAll() =>
        Task.FromResult<IList<Order>>([.. _orders]);

    public Task<Order> Update(Order entity)
    {
        Delete(entity.Id);
        Create(entity);
        return Task.FromResult(entity);
    }
}