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
    Task<IReadOnlyList<ClientDto>> GetAllAsync();

    /// <summary>
    /// Получить клиента по идентификатору
    /// </summary>
    Task<ClientDto?> GetByIdAsync(int id);

    /// <summary>
    /// Создать клиента
    /// </summary>
    Task<ClientDto> CreateAsync(ClientEditDto dto);

    /// <summary>
    /// Обновить клиента
    /// </summary>
    Task<ClientDto?> UpdateAsync(int id, ClientEditDto dto);

    /// <summary>
    /// Удалить клиента
    /// </summary>
    Task<bool> DeleteAsync(int id);
}