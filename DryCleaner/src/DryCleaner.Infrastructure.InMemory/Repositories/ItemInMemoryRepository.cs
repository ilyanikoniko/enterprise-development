using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Interfaces;

namespace DryCleaner.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий для работы с изделиями в памяти
/// </summary>
public class ItemInMemoryRepository : IItemRepository
{
    private readonly List<Item> _items = [];
    private int _nextId = 1;

    /// <summary>
    /// Создать новое изделие
    /// </summary>
    public Task<Item> Create(Item entity)
    {
        if (entity.Id == 0)
        {
            entity.Id = _nextId++;
        }
        else if (entity.Id >= _nextId)
        {
            _nextId = entity.Id + 1;
        }

        _items.Add(entity);
        return Task.FromResult(entity);
    }

    /// <summary>
    /// Удалить изделие по id
    /// </summary>
    public Task<bool> Delete(int entityId)
    {
        var item = Read(entityId).Result;
        if (item == null)
            return Task.FromResult(false);

        var result = _items.Remove(item);
        return Task.FromResult(result);
    }

    /// <summary>
    /// Получить изделие по id
    /// </summary>
    public Task<Item?> Read(int entityId) =>
        Task.FromResult(_items.FirstOrDefault(i => i.Id == entityId));

    /// <summary>
    /// Получить список всех изделий
    /// </summary>
    public Task<IList<Item>> ReadAll() =>
        Task.FromResult<IList<Item>>([.. _items]);

    /// <summary>
    /// Обновить существующее изделие
    /// </summary>
    public Task<Item> Update(Item entity)
    {
        Delete(entity.Id);
        Create(entity);
        return Task.FromResult(entity);
    }
}