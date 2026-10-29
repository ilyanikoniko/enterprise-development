using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Interfaces;

namespace DryCleaner.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий для работы с клиентами в памяти
/// </summary>
public class ClientInMemoryRepository : IClientRepository
{
    private readonly List<Client> _clients = [];
    private int _nextId = 1;

    /// <summary>
    /// Создать нового клиента
    /// </summary>
    public Task<Client> Create(Client entity)
    {
        if (entity.Id == 0)
        {
            entity.Id = _nextId++; 
        }
        else if (entity.Id >= _nextId)
        {
            _nextId = entity.Id + 1; 
        }

        _clients.Add(entity);
        return Task.FromResult(entity);
    }

    /// <summary>
    /// Удалить клиента по id
    /// </summary>
    public Task<bool> Delete(int entityId)
    {
        var client = Read(entityId).Result;
        if (client == null)
            return Task.FromResult(false);

        var result = _clients.Remove(client);
        return Task.FromResult(result);
    }

    /// <summary>
    /// Получить клиента по id
    /// </summary>
    public Task<Client?> Read(int entityId) =>
        Task.FromResult(_clients.FirstOrDefault(c => c.Id == entityId));

    /// <summary>
    /// Получить список всех клиентов
    /// </summary>
    public Task<IList<Client>> ReadAll() =>
        Task.FromResult<IList<Client>>([.. _clients]);

    /// <summary>
    /// Обновить существующего клиента
    /// </summary>
    public Task<Client> Update(Client entity)
    {
        Delete(entity.Id);
        Create(entity);
        return Task.FromResult(entity);
    }
}