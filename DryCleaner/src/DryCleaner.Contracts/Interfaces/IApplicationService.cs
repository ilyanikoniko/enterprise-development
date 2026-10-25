namespace DryCleaner.Contracts.Interfaces;

/// <summary>
/// Базовый интерфейс для сервисов, реализующих CRUD-операции над сущностями
/// </summary>
public interface IApplicationService<TDto, TCreateUpdateDto, TKey>
{
    /// <summary>
    /// Создать новую запись
    /// </summary>
    public Task<TDto> Create(TCreateUpdateDto dto);

    /// <summary>
    /// Получить запись по id
    /// </summary>
    public Task<TDto?> Get(TKey id);

    /// <summary>
    /// Получить список всех записей
    /// </summary>
    public Task<IList<TDto>> GetAll();

    /// <summary>
    /// Обновить существующую запись
    /// </summary>
    public Task<TDto> Update(TCreateUpdateDto dto, TKey id);

    /// <summary>
    /// Удалить запись по id
    /// </summary>
    public Task<bool> Delete(TKey id);
}