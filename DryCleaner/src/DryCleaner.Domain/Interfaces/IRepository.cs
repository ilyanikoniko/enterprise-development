namespace DryCleaner.Domain.Interfaces;

/// <summary>
/// Базовый интерфейс репозитория для сущности
/// </summary>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Получить все сущности
    /// </summary>
    Task<IReadOnlyList<T>> GetAllAsync();

    /// <summary>
    /// Получить сущность по идентификатору
    /// </summary>
    Task<T?> GetByIdAsync(int id);

    /// <summary>
    /// Добавить сущность
    /// </summary>
    Task<T> AddAsync(T entity);

    /// <summary>
    /// Обновить сущность
    /// </summary>
    Task<T?> UpdateAsync(T entity);

    /// <summary>
    /// Удалить сущность по идентификатору
    /// </summary>
    Task<bool> DeleteAsync(int id);
}