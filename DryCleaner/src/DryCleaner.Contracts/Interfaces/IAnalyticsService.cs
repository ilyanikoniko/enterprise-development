using DryCleaner.Contracts.Dtos;

namespace DryCleaner.Contracts.Interfaces;

/// <summary>
/// Сервис аналитики.
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Заказы в обработке, упорядоченные по дате приёма
    /// </summary>
    Task<IReadOnlyList<OrderDto>> GetOrdersInProgressAsync();

    /// <summary>
    /// Топ-5 клиентов по количеству заказов за период
    /// </summary>
    Task<IReadOnlyList<TopClientDto>> GetTop5ClientsAsync(DateTime from, DateTime to);

    /// <summary>
    /// Клиенты с самыми долгими заказами, упорядоченные по ФИО
    /// </summary>
    Task<IReadOnlyList<LongestOrderClientDto>> GetClientsWithLongestOrdersAsync();

    /// <summary>
    /// Топ-5 популярных и непопулярных категорий за последний год
    /// </summary>
    Task<CategoryStatDto> GetTop5CategoriesAsync(DateTime from, DateTime to);

    /// <summary>
    /// Клиент, потративший наибольшую сумму
    /// </summary>
    Task<MaxSpenderDto?> GetMaxSpenderAsync();
}