using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Interfaces;

namespace DryCleaner.Infrastructure.InMemory.Repositories;

public class ItemInMemoryRepository : IItemRepository
{
    private readonly List<Item> _items = [];

    public Task<Item> Create(Item entity)
    {
        if (entity.Id == 0)
        {
            entity.Id = _items.Any() ? _items.Max(i => i.Id) + 1 : 1;
        }
        _items.Add(entity);
        return Task.FromResult(entity);
    }

    public Task<bool> Delete(int entityId)
    {
        var item = Read(entityId).Result;
        if (item == null)
            return Task.FromResult(false);

        var result = _items.Remove(item);
        return Task.FromResult(result);
    }

    public Task<Item?> Read(int entityId) =>
        Task.FromResult(_items.FirstOrDefault(i => i.Id == entityId));

    public Task<IList<Item>> ReadAll() =>
        Task.FromResult<IList<Item>>([.. _items]);

    public Task<Item> Update(Item entity)
    {
        Delete(entity.Id);
        Create(entity);
        return Task.FromResult(entity);
    }
}