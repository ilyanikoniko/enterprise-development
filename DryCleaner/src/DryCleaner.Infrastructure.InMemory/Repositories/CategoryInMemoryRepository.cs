using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Interfaces;

namespace DryCleaner.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий для работы с категориями в памяти
/// </summary>
public class CategoryInMemoryRepository : ICategoryRepository
{
    private readonly List<Category> _categories = [];

    /// <summary>
    /// Создать новую категорию
    /// </summary>
    public Task<Category> Create(Category entity)
    {
        if (entity.Id == 0)
        {
            entity.Id = _categories.Any() ? _categories.Max(c => c.Id) + 1 : 1;
        }
        _categories.Add(entity);
        return Task.FromResult(entity);
    }

    /// <summary>
    /// Удалить категорию по id
    /// </summary>
    public Task<bool> Delete(int entityId)
    {
        var category = Read(entityId).Result;
        if (category == null)
            return Task.FromResult(false);

        var result = _categories.Remove(category);
        return Task.FromResult(result);
    }

    /// <summary>
    /// Получить категорию по id
    /// </summary>
    public Task<Category?> Read(int entityId) =>
        Task.FromResult(_categories.FirstOrDefault(c => c.Id == entityId));

    /// <summary>
    /// Получить список всех категорий
    /// </summary>
    public Task<IList<Category>> ReadAll() =>
        Task.FromResult<IList<Category>>([.. _categories]);

    /// <summary>
    /// Обновить существующую категорию
    /// </summary>
    public Task<Category> Update(Category entity)
    {
        Delete(entity.Id);
        Create(entity);
        return Task.FromResult(entity);
    }
}