using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Interfaces;

namespace DryCleaner.Infrastructure.InMemory.Repositories;

public class CategoryInMemoryRepository : IRepository<Category, int>
{
    private readonly List<Category> _categories = [];

    public Task<Category> Create(Category entity)
    {
        if (entity.Id == 0)
        {
            entity.Id = _categories.Any() ? _categories.Max(c => c.Id) + 1 : 1;
        }
        _categories.Add(entity);
        return Task.FromResult(entity);
    }

    public Task<bool> Delete(int entityId)
    {
        var category = Read(entityId).Result;
        if (category == null)
            return Task.FromResult(false);

        var result = _categories.Remove(category);
        return Task.FromResult(result);
    }

    public Task<Category?> Read(int entityId) =>
        Task.FromResult(_categories.FirstOrDefault(c => c.Id == entityId));

    public Task<IList<Category>> ReadAll() =>
        Task.FromResult<IList<Category>>([.. _categories]);

    public Task<Category> Update(Category entity)
    {
        Delete(entity.Id);
        Create(entity);
        return Task.FromResult(entity);
    }
}