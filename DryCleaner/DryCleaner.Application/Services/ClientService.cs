using DryCleaner.Application.Mappers;
using DryCleaner.Contracts.Dtos;
using DryCleaner.Contracts.Interfaces; 
using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Interfaces;

namespace DryCleaner.Application.Services;

public class ClientService(IRepository<Client, int> clientRepository)
    : IApplicationService<ClientDto, ClientEditDto, int>
{
    public async Task<ClientDto> Create(ClientEditDto dto)
    {
        var client = Mapper.Map(dto);
        var result = await clientRepository.Create(client);
        return Mapper.Map(result);
    }

    public async Task<ClientDto?> Get(int id)
    {
        var client = await clientRepository.Read(id);
        return client == null ? null : Mapper.Map(client);
    }

    public async Task<IList<ClientDto>> GetAll()
    {
        var clients = await clientRepository.ReadAll();
        return Mapper.Map(clients);
    }

    public async Task<ClientDto> Update(ClientEditDto dto, int id)
    {
        var updatedClient = Mapper.Map(dto);
        updatedClient.Id = id;

        var result = await clientRepository.Update(updatedClient);
        return Mapper.Map(result);
    }

    public async Task<bool> Delete(int id) => await clientRepository.Delete(id);
}