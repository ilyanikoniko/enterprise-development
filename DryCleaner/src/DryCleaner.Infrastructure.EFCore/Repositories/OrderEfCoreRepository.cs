using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DryCleaner.Infrastructure.EFCore.Repositories;

/// <summary>
/// Репозиторий для работы с заказами через EF Core
/// </summary>
public class OrderEfCoreRepository(DryCleanerDbContext context) : IOrderRepository
{
    /// <inheritdoc/>
    public async Task<Order> Create(Order entity)
    {
        var result = await context.Orders.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(int entityId)
    {
        var entity = await context.Orders.FirstOrDefaultAsync(o => o.Id == entityId);
        if (entity == null)
            return false;

        context.Orders.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }

    /// <inheritdoc/>
    public async Task<Order?> Read(int entityId) =>
        await context.Orders
            .Include(o => o.Client) 
            .Include(o => o.Item).AsNoTracking().FirstOrDefaultAsync(o => o.Id == entityId);

    /// <inheritdoc/>
    public async Task<IList<Order>> ReadAll() =>
        await context.Orders
            .Include(o => o.Client) 
            .Include(o => o.Item)   
            .ToListAsync();

    /// <inheritdoc/>
    public async Task<Order> Update(Order entity)
    {
        context.Orders.Update(entity);
        await context.SaveChangesAsync();
        return (await Read(entity.Id))!;
    }
}