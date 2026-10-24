using DryCleaner.Contracts.Dtos;
using DryCleaner.Domain.Entities;

namespace DryCleaner.Application.Mappers;

public static class Mapper
{
    // Из Сущности в DTO (для чтения)
    public static ClientDto Map(Client entity) =>
    new(entity.Id, entity.FullName, entity.PhoneNumber);

    // Из DTO в Сущность (для создания)
    public static Client Map(ClientEditDto dto) =>
    new() { FullName = dto.FullName, PhoneNumber = dto.PhoneNumber };

    // Маппинг списков (для GetAll)
    public static List<ClientDto> Map(IEnumerable<Client> entities) =>
        [.. entities.Select(Map)];
}