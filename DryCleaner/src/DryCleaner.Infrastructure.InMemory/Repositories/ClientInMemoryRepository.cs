using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Interfaces;

namespace DryCleaner.Infrastructure.InMemory.Repositories;

public class ClientInMemoryRepository : IRepository<Client, int>
{
    private readonly List<Client> _clients = [];

    public Task<Client> Create(Client entity)
    {
        if (entity.Id == 0)
        {
            entity.Id = _clients.Any() ? _clients.Max(c => c.Id) + 1 : 1;
        }

        _clients.Add(entity);
        return Task.FromResult(entity);
    }

    public Task<bool> Delete(int entityId)
    {
        var client = Read(entityId).Result;
        if (client == null)
            return Task.FromResult(false);

        var result = _clients.Remove(client);
        return Task.FromResult(result);
    }

    public Task<Client?> Read(int entityId) =>
        Task.FromResult(_clients.FirstOrDefault(c => c.Id == entityId));

    public Task<IList<Client>> ReadAll() =>
        Task.FromResult<IList<Client>>([.. _clients]);

    public Task<Client> Update(Client entity)
    {
        Delete(entity.Id);
        Create(entity);
        return Task.FromResult(entity);
    }
}