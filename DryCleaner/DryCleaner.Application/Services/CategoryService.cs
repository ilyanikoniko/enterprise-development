using DryCleaner.Application.Mappers;
using DryCleaner.Contracts.Dtos;
using DryCleaner.Contracts.Interfaces;
using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Interfaces;

namespace DryCleaner.Application.Services;

/// <summary>
/// Сервис для управления категориями изделий
/// </summary>
/// <param name="repository">Репозиторий категорий</param>
public class CategoryService(IRepository<Category, int> repository)
    : IApplicationService<CategoryDto, CategoryEditDto, int>
{
    /// <summary>
    /// Создать новую категорию
    /// </summary>
    public async Task<CategoryDto> Create(CategoryEditDto dto)
    {
        var category = Mapper.Map(dto);
        var result = await repository.Create(category);
        return Mapper.Map(result);
    }

    /// <summary>
    /// Получить категорию по id
    /// </summary>
    public async Task<CategoryDto?> Get(int id)
    {
        var category = await repository.Read(id);
        return category == null ? null : Mapper.Map(category);
    }

    /// <summary>
    /// Получить список всех категорий
    /// </summary>
    public async Task<IList<CategoryDto>> GetAll()
    {
        var categories = await repository.ReadAll();
        return Mapper.Map(categories);
    }

    /// <summary>
    /// Обновить существующую категорию
    /// </summary>
    public async Task<CategoryDto> Update(CategoryEditDto dto, int id)
    {
        var entity = Mapper.Map(dto);
        entity.Id = id;

        var result = await repository.Update(entity);
        return Mapper.Map(result);
    }

    /// <summary>
    /// Удалить категорию по идентификатору
    /// </summary>
    public async Task<bool> Delete(int id) => await repository.Delete(id);
}