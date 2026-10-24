using DryCleaner.Contracts.Dtos;

namespace DryCleaner.Contracts.Interfaces;

/// <summary>
/// Сервис для работы с клиентами
/// </summary>
public interface IClientService
{
    /// <summary>
    /// Получить всех клиентов
    /// </summary>
    public Task<IReadOnlyList<ClientDto>> GetAllAsync();

    /// <summary>
    /// Получить клиента по идентификатору
    /// </summary>
    public Task<ClientDto?> GetByIdAsync(int id);

    /// <summary>
    /// Создать клиента
    /// </summary>
    public Task<ClientDto> CreateAsync(ClientEditDto dto);

    /// <summary>
    /// Обновить клиента
    /// </summary>
    public Task<ClientDto?> UpdateAsync(int id, ClientEditDto dto);

    /// <summary>
    /// Удалить клиента
    /// </summary>
    public Task<bool> DeleteAsync(int id);
}