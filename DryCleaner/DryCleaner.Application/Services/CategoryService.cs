using DryCleaner.Application.Mappers;
using DryCleaner.Contracts.Dtos;
using DryCleaner.Contracts.Interfaces;
using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Interfaces;

namespace DryCleaner.Application.Services;

public class CategoryService(IRepository<Category, int> repository)
    : IApplicationService<CategoryDto, CategoryEditDto, int>
{
    public async Task<CategoryDto> Create(CategoryEditDto dto) => Mapper.Map(await repository.Create(Mapper.Map(dto)));
    public async Task<CategoryDto?> Get(int id) => (await repository.Read(id)) is { } entity ? Mapper.Map(entity) : null;
    public async Task<IList<CategoryDto>> GetAll() => Mapper.Map(await repository.ReadAll());

    public async Task<CategoryDto> Update(CategoryEditDto dto, int id)
    {
        var entity = Mapper.Map(dto);
        entity.Id = id;
        return Mapper.Map(await repository.Update(entity));
    }
    public async Task<bool> Delete(int id) => await repository.Delete(id);
}