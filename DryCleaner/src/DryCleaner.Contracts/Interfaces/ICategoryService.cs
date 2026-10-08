using DryCleaner.Contracts.Dtos;

namespace DryCleaner.Contracts.Interfaces;

/// <summary>
/// Сервис для работы с категориями
/// </summary>
public interface ICategoryService
{
    /// <summary>
    /// Получить все категории
    /// </summary>
    Task<IReadOnlyList<CategoryDto>> GetAllAsync();

    /// <summary>
    /// Получить категорию по идентификатору
    /// </summary>
    Task<CategoryDto?> GetByIdAsync(int id);

    /// <summary>
    /// Создать категорию
    /// </summary>
    Task<CategoryDto> CreateAsync(CategoryEditDto dto);

    /// <summary>
    /// Обновить категорию
    /// </summary>
    Task<CategoryDto?> UpdateAsync(int id, CategoryEditDto dto);

    /// <summary>
    /// Удалить категорию
    /// </summary>
    Task<bool> DeleteAsync(int id);
}