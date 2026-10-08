using DryCleaner.Contracts.Dtos;

namespace DryCleaner.Contracts.Interfaces;

/// <summary>
/// Сервис для работы с изделиями
/// </summary>
public interface IItemService
{
    /// <summary>
    /// Получить все изделия
    /// </summary>
    Task<IReadOnlyList<ItemDto>> GetAllAsync();

    /// <summary>
    /// Получить изделие по идентификатору
    /// </summary>
    Task<ItemDto?> GetByIdAsync(int id);

    /// <summary>
    /// Создать изделие
    /// </summary>
    Task<ItemDto> CreateAsync(ItemCreateDto dto);

    /// <summary>
    /// Обновить изделие
    /// </summary>
    Task<ItemDto?> UpdateAsync(int id, ItemUpdateDto dto);

    /// <summary>
    /// Удалить изделие
    /// </summary>
    Task<bool> DeleteAsync(int id);
}