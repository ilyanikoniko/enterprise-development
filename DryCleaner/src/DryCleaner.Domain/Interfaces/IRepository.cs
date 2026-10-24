namespace DryCleaner.Domain.Interfaces;

/// <summary>
/// Базовый интерфейс репозитория для сущности
/// </summary>
public interface IRepository<TEntity, TKey>
{
    /// <summary>
    /// Добавить сущность
    /// </summary>
    public Task<TEntity> Create(TEntity entity);

    /// <summary>
    /// Получить сущность по идентификатору
    /// </summary>
    public Task<TEntity?> Read(TKey entityId);

    /// <summary>
    /// Получить все сущности
    /// </summary>
    public Task<IList<TEntity>> ReadAll();

    /// <summary>
    /// Обновить сущность
    /// </summary>
    public Task<TEntity> Update(TEntity entity);

    /// <summary>
    /// Удалить сущность по идентификатору
    /// </summary>
    public Task<bool> Delete(TKey entityId);
}