using DryCleaner.Domain.Data;
using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Interfaces;

namespace DryCleaner.Infrastructure.InMemory.Repositories;

/// <summary>
/// In-memory реализация репозитория клиентов.
/// </summary>
public class InMemoryClientRepository : IClientRepository
{
    private readonly List<Client> _clients = DataSeed.Clients;

    /// <inheritdoc />
    public Task<IReadOnlyList<Client>> GetAllAsync()
    {
        IReadOnlyList<Client> result = _clients;
        return Task.FromResult(result);
    }

    /// <inheritdoc />
    public Task<Client?> GetByIdAsync(int id)
    {
        var client = _clients.FirstOrDefault(c => c.Id == id);
        return Task.FromResult(client);
    }

    /// <inheritdoc />
    public Task<Client> AddAsync(Client entity)
    {
        var nextId = _clients.Any() ? _clients.Max(c => c.Id) + 1 : 1;
        entity.Id = nextId;
        _clients.Add(entity);
        return Task.FromResult(entity);
    }

    /// <inheritdoc />
    public Task<Client?> UpdateAsync(Client entity)
    {
        var existing = _clients.FirstOrDefault(c => c.Id == entity.Id);
        if (existing == null)
            return Task.FromResult<Client?>(null);

        existing.FullName = entity.FullName;
        existing.PhoneNumber = entity.PhoneNumber;
        return Task.FromResult<Client?>(existing);
    }

    /// <inheritdoc />
    public Task<bool> DeleteAsync(int id)
    {
        var existing = _clients.FirstOrDefault(c => c.Id == id);
        if (existing == null)
            return Task.FromResult(false);

        _clients.Remove(existing);
        return Task.FromResult(true);
    }
}