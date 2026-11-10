using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DryCleaner.Infrastructure.EFCore.Repositories;

/// <summary>
/// Репозиторий для работы с изделиями через EF Core
/// </summary>
public class ItemEfCoreRepository(DryCleanerDbContext context) : IItemRepository
{
    /// <inheritdoc/>
    public async Task<Item> Create(Item entity)
    {
        var result = await context.Items.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(int entityId)
    {
        var entity = await context.Items.FirstOrDefaultAsync(i => i.Id == entityId);
        if (entity == null)
            return false;

        context.Items.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }

    /// <inheritdoc/>
    public async Task<Item?> Read(int entityId) =>
        await context.Items
            .Include(i => i.Category).AsNoTracking().FirstOrDefaultAsync(i => i.Id == entityId);

    /// <inheritdoc/>
    public async Task<IList<Item>> ReadAll() =>
        await context.Items
            .Include(i => i.Category) 
            .ToListAsync();

    /// <inheritdoc/>
    public async Task<Item> Update(Item entity)
    {
        context.Items.Update(entity);
        await context.SaveChangesAsync();
        return (await Read(entity.Id))!;
    }
}