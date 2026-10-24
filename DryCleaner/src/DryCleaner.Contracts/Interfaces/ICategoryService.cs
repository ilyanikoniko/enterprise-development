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
    public Task<IReadOnlyList<CategoryDto>> GetAllAsync();

    /// <summary>
    /// Получить категорию по идентификатору
    /// </summary>
    public Task<CategoryDto?> GetByIdAsync(int id);

    /// <summary>
    /// Создать категорию
    /// </summary>
    public Task<CategoryDto> CreateAsync(CategoryEditDto dto);

    /// <summary>
    /// Обновить категорию
    /// </summary>
    public Task<CategoryDto?> UpdateAsync(int id, CategoryEditDto dto);

    /// <summary>
    /// Удалить категорию
    /// </summary>
    public Task<bool> DeleteAsync(int id);
}