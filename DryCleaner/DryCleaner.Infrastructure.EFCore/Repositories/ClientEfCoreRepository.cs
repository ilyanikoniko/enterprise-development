using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DryCleaner.Infrastructure.EFCore.Repositories;

/// <summary>
/// Репозиторий для работы с клиентами через EF Core
/// </summary>
public class ClientEfCoreRepository(DryCleanerDbContext context) : IClientRepository
{
    public async Task<Client> Create(Client entity)
    {
        var result = await context.Clients.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<bool> Delete(int entityId)
    {
        var entity = await context.Clients.FirstOrDefaultAsync(c => c.Id == entityId);
        if (entity == null)
            return false;

        context.Clients.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<Client?> Read(int entityId) =>
        await context.Clients.FirstOrDefaultAsync(c => c.Id == entityId);

    public async Task<IList<Client>> ReadAll() =>
        await context.Clients.ToListAsync();

    public async Task<Client> Update(Client entity)
    {
        context.Clients.Update(entity);
        await context.SaveChangesAsync();
        return (await Read(entity.Id))!;
    }
}