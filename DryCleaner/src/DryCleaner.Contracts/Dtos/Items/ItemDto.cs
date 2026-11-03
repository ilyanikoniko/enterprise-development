namespace DryCleaner.Contracts.Dtos.Items;

/// <summary>
/// DTO изделия для чтения
/// </summary>
/// <param name="Id">Идентификатор изделия</param>
/// <param name="Name">Наименование изделия</param>
/// <param name="Material">Материал изделия</param>
/// <param name="CategoryId">Идентификатор категории</param>
/// <param name="CategoryName">Название категории</param>
public record ItemDto(int Id, string Name, string Material, int CategoryId, string CategoryName);