using DryCleaner.Application.Mappers;
using DryCleaner.Contracts.Dtos.Clients;
using DryCleaner.Contracts.Interfaces;
using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Interfaces;

namespace DryCleaner.Application.Services;

/// <summary>
/// Сервис для управления клиентами химчистки
/// </summary>
/// <param name="clientRepository">Репозиторий клиентов</param>
public class ClientService(IRepository<Client, int> clientRepository)
    : IApplicationService<ClientDto, ClientEditDto, int>
{
    /// <summary>
    /// Создать нового клиента
    /// </summary>
    public async Task<ClientDto> Create(ClientEditDto dto)
    {
        var client = Mapper.Map(dto);
        var result = await clientRepository.Create(client);
        return Mapper.Map(result);
    }

    /// <summary>
    /// Получить клиента по id
    /// </summary>
    public async Task<ClientDto?> Get(int id)
    {
        var client = await clientRepository.Read(id);
        return client == null ? null : Mapper.Map(client);
    }

    /// <summary>
    /// Получить список всех клиентов
    /// </summary>
    public async Task<IList<ClientDto>> GetAll()
    {
        var clients = await clientRepository.ReadAll();
        return Mapper.Map(clients);
    }

    /// <summary>
    /// Обновить существующего клиента
    /// </summary>
    public async Task<ClientDto> Update(ClientEditDto dto, int id)
    {
        var updatedClient = Mapper.Map(dto);
        updatedClient.Id = id;

        var result = await clientRepository.Update(updatedClient);
        return Mapper.Map(result);
    }

    /// <summary>
    /// Удалить клиента по id
    /// </summary>
    public async Task<bool> Delete(int id) => await clientRepository.Delete(id);
}