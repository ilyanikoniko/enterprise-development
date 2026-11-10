using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DryCleaner.Infrastructure.EFCore.Repositories;

/// <summary>
/// Репозиторий для работы с категориями через EF Core
/// </summary>
public class CategoryEfCoreRepository(DryCleanerDbContext context) : ICategoryRepository
{

    /// <inheritdoc/>
    public async Task<Category> Create(Category entity)
    {
        var result = await context.Categories.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(int entityId)
    {
        var entity = await context.Categories.FirstOrDefaultAsync(c => c.Id == entityId);
        if (entity == null)
            return false;

        context.Categories.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }

    /// <inheritdoc/>
    public async Task<Category?> Read(int entityId) =>
        await context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == entityId);

    /// <inheritdoc/>
    public async Task<IList<Category>> ReadAll() =>
        await context.Categories.AsNoTracking().ToListAsync();

    /// <inheritdoc/>
    public async Task<Category> Update(Category entity)
    {
        context.Categories.Update(entity);
        await context.SaveChangesAsync();
        return (await Read(entity.Id))!;
    }
}