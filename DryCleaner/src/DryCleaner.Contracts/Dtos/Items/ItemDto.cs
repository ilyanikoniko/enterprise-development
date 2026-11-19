using DryCleaner.Contracts.Dtos.Categories;

namespace DryCleaner.Contracts.Dtos.Items;

/// <summary>
/// DTO изделия для чтения
/// </summary>
/// <param name="Id">Идентификатор изделия</param>
/// <param name="Name">Наименование изделия</param>
/// <param name="Material">Материал изделия</param>
/// <param name="Category">Категория изделия</param>
public record ItemDto(
    int Id,
    string Name,
    string Material,
    CategoryDto Category
);