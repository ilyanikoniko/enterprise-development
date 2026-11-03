using DryCleaner.Application.Mappers;
using DryCleaner.Contracts.Dtos.Items;
using DryCleaner.Contracts.Interfaces;
using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Interfaces;

namespace DryCleaner.Application.Services;

/// <summary>
/// Сервис для управления изделиями
/// </summary>
public class ItemService(IRepository<Item, int> repository, IRepository<Category, int> categoryRepository) 
    : IApplicationService<ItemDto, ItemCreateDto, int>
{
    /// <summary>
    /// Создать новое изделие
    /// </summary>
    public async Task<ItemDto> Create(ItemCreateDto dto)
    {
        var category = await categoryRepository.Read(dto.CategoryId);
        if (category == null)
            throw new ArgumentException($"Категория с идентификатором {dto.CategoryId} не найдена.");

        var item = Mapper.Map(dto);
        var result = await repository.Create(item);
        return Mapper.Map(result);
    }

    /// <summary>
    /// Получить изделие по id
    /// </summary>
    public async Task<ItemDto?> Get(int id)
    {
        var item = await repository.Read(id);
        return item == null ? null : Mapper.Map(item);
    }

    /// <summary>
    /// Получить список всех изделий
    /// </summary>
    public async Task<IList<ItemDto>> GetAll()
    {
        var items = await repository.ReadAll();
        return Mapper.Map(items);
    }

    /// <summary>
    /// Обновить существующее изделие
    /// </summary>
    public async Task<ItemDto> Update(ItemCreateDto dto, int id)
    {
        var existing = await repository.Read(id);
        if (existing == null)
            throw new KeyNotFoundException($"Изделие с идентификатором {id} не найдено");

        var category = await categoryRepository.Read(dto.CategoryId);
        if (category == null)
            throw new ArgumentException($"Категория с идентификатором {dto.CategoryId} не найдена");

        var updatedItem = Mapper.Map(dto);
        updatedItem.Id = id;
        var result = await repository.Update(updatedItem);
        return Mapper.Map(result);
    }

    /// <summary>
    /// Удалить изделие по id
    /// </summary>
    public async Task<bool> Delete(int id)
    {
        var existing = await repository.Read(id);
        if (existing == null)
            throw new KeyNotFoundException($"Изделие с идентификатором {id} не найдено.");

        return await repository.Delete(id);
    }
}