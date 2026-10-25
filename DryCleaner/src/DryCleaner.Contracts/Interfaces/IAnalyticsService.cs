using DryCleaner.Contracts.Dtos;

namespace DryCleaner.Contracts.Interfaces;

/// <summary>
/// Сервис аналитики
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Заказы в обработке, упорядоченные по дате приёма
    /// </summary>
    public Task<IReadOnlyList<OrderDto>> GetOrdersInProgressAsync();

    /// <summary>
    /// Топ-5 клиентов по количеству заказов за период
    /// </summary>
    public Task<IReadOnlyList<TopClientDto>> GetTop5ClientsAsync(DateTime from, DateTime to);

    /// <summary>
    /// Клиенты с самыми долгими заказами, упорядоченные по ФИО
    /// </summary>
    public Task<IReadOnlyList<LongestOrderClientDto>> GetClientsWithLongestOrdersAsync();

    /// <summary>
    /// Топ-5 популярных и непопулярных категорий за последний год
    /// </summary>
    public Task<CategoryStatDto> GetTop5CategoriesAsync(DateTime from, DateTime to);

    /// <summary>
    /// Клиент, потративший наибольшую сумму
    /// </summary>
    public Task<MaxSpenderDto?> GetMaxSpenderAsync();
}