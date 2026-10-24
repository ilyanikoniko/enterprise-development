using DryCleaner.Application.Mappers;
using DryCleaner.Contracts.Dtos;
using DryCleaner.Contracts.Interfaces;
using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Interfaces;

namespace DryCleaner.Application.Services;

public class ItemService(IRepository<Item, int> repository)
    : IApplicationService<ItemDto, ItemCreateDto, int> 
{
    public async Task<ItemDto> Create(ItemCreateDto dto)
    {
        var item = Mapper.Map(dto);
        var result = await repository.Create(item);
        return Mapper.Map(result);
    }

    public async Task<ItemDto?> Get(int id)
    {
        var item = await repository.Read(id);
        return item == null ? null : Mapper.Map(item);
    }

    public async Task<IList<ItemDto>> GetAll()
    {
        var items = await repository.ReadAll();
        return Mapper.Map(items);
    }

    public async Task<ItemDto> Update(ItemCreateDto dto, int id)
    {
        var updatedItem = Mapper.Map(dto);
        updatedItem.Id = id;
        var result = await repository.Update(updatedItem);
        return Mapper.Map(result);
    }

    public async Task<bool> Delete(int id) => await repository.Delete(id);
}