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
    public Task<IReadOnlyList<ItemDto>> GetAllAsync();

    /// <summary>
    /// Получить изделие по идентификатору
    /// </summary>
    public Task<ItemDto?> GetByIdAsync(int id);

    /// <summary>
    /// Создать изделие
    /// </summary>
    public Task<ItemDto> CreateAsync(ItemCreateDto dto);

    /// <summary>
    /// Обновить изделие
    /// </summary>
    public Task<ItemDto?> UpdateAsync(int id, ItemUpdateDto dto);

    /// <summary>
    /// Удалить изделие
    /// </summary>
    public Task<bool> DeleteAsync(int id);
}