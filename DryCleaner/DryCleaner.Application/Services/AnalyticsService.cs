using DryCleaner.Application.Mappers;
using DryCleaner.Contracts.Dtos.Analytics;
using DryCleaner.Contracts.Dtos.Orders;
using DryCleaner.Contracts.Interfaces;
using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Enums;
using DryCleaner.Domain.Interfaces;

namespace DryCleaner.Application.Services;

/// <summary>
/// Сервис для выполнения аналитических запросов по данным химчистки
/// </summary>
public class AnalyticsService(
    IOrderRepository orderRepository,
    IRepository<Client, int> clientRepository,
    IRepository<Item, int> itemRepository,
    IRepository<Category, int> categoryRepository) : IAnalyticsService
{

    /// <summary>
    /// Получить список заказов находящихся в обработке, упорядоченных по дате приема
    /// </summary>
    /// <returns>Список DTO заказов в обработке</returns>
    public async Task<IReadOnlyList<OrderDto>> GetOrdersInProgressAsync()
    {
        var orders = await orderRepository.ReadAll();
        return orders
            .Where(o => o.Status == OrderStatus.InProgress)
            .OrderBy(o => o.AcceptanceDate)
            .Select(Mapper.Map)
            .ToList();
    }

    /// <summary>
    /// Получить топ-5 клиентов по количеству заказов за указанный период
    /// </summary>
    /// <param name="from">Дата начала периода</param>
    /// <param name="to">Дата окончания периода</param>
    /// <returns>Список DTO топ-5 клиентов</returns>
    public async Task<IReadOnlyList<TopClientDto>> GetTop5ClientsAsync(DateTime from, DateTime to)
    {
        var orders = await orderRepository.ReadAll();
        var clients = await clientRepository.ReadAll();

        return orders
            .Where(o => o.AcceptanceDate >= from && o.AcceptanceDate <= to)
            .GroupBy(o => o.ClientId)
            .Select(g => {
                var client = clients.FirstOrDefault(c => c.Id == g.Key);
                return new TopClientDto(g.Key, client?.FullName ?? "Неизвестно", g.Count());
            })
            .OrderByDescending(x => x.OrdersCount)
            .Take(5)
            .ToList();
    }

    /// <summary>
    /// Получить клиентов чьи заказы обрабатывались дольше всего, упорядоченных по ФИО
    /// </summary>
    /// <returns>Список DTO клиентов с максимальным сроком выполнения заказа</returns>
    public async Task<IReadOnlyList<LongestOrderClientDto>> GetClientsWithLongestOrdersAsync()
    {
        var orders = await orderRepository.ReadAll();
        var clients = await clientRepository.ReadAll();

        if (orders.Count == 0) return new List<LongestOrderClientDto>();

        var maxDays = orders.Max(o => o.CompletionDays);

        return orders
            .Where(o => o.CompletionDays == maxDays)
            .Select(o => o.ClientId)
            .Distinct()
            .Select(id => {
                var client = clients.FirstOrDefault(c => c.Id == id);
                return new LongestOrderClientDto(id, client?.FullName ?? "Неизвестно", maxDays);
            })
            .OrderBy(c => c.ClientFullName)
            .ToList();
    }

    /// <summary>
    /// Получить топ-5 самых популярных и самых непопулярных категорий изделий за указанный период
    /// </summary>
    /// <param name="from">Дата начала периода</param>
    /// <param name="to">Дата окончания периода</param>
    /// <returns>DTO со списками популярных и непопулярных категорий</returns>
    public async Task<CategoryStatDto> GetTop5CategoriesAsync(DateTime from, DateTime to)
    {
        var orders = await orderRepository.ReadAll();
        var items = await itemRepository.ReadAll();
        var categories = await categoryRepository.ReadAll();

        var categoryStats = orders
            .Where(o => o.AcceptanceDate >= from && o.AcceptanceDate <= to)
            .Join(items, o => o.ItemId, i => i.Id, (o, i) => i.CategoryId) 
            .GroupBy(catId => catId)
            .Select(g => {
                var cat = categories.FirstOrDefault(c => c.Id == g.Key);
                return new CategoryCountDto(g.Key, cat?.Name ?? "Неизвестно", g.Count());
            })
            .ToList();

        var mostPopular = categoryStats.OrderByDescending(x => x.OrdersCount).Take(5).ToList();
        var leastPopular = categoryStats.OrderBy(x => x.OrdersCount).Take(5).ToList();

        return new CategoryStatDto(mostPopular, leastPopular);
    }

    /// <summary>
    /// Получить клиента потратившего наибольшую сумму за все время
    /// </summary>
    /// <returns>DTO клиента с максимальной суммой трат или null, если данных нет</returns>
    public async Task<MaxSpenderDto?> GetMaxSpenderAsync()
    {
        var orders = await orderRepository.ReadAll();
        var clients = await clientRepository.ReadAll();
        var items = await itemRepository.ReadAll();
        var categories = await categoryRepository.ReadAll();

        var totals = orders
            .GroupBy(o => o.ClientId)
            .Select(g => {
                var client = clients.FirstOrDefault(c => c.Id == g.Key);

                var totalSpent = g.Sum(o => {
                    var item = items.FirstOrDefault(i => i.Id == o.ItemId);
                    var category = categories.FirstOrDefault(c => c.Id == item?.CategoryId);
                    return category?.CleaningPrice ?? 0;
                });

                return new MaxSpenderDto(g.Key, client?.FullName ?? "Неизвестно", totalSpent);
            })
            .ToList();

        if (totals.Count == 0) return null;

        return totals.OrderByDescending(x => x.TotalSpent).FirstOrDefault();
    }
}