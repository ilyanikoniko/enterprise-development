using DryCleaner.Application.Mappers;
using DryCleaner.Contracts.Dtos;
using DryCleaner.Contracts.Interfaces;
using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Enums;
using DryCleaner.Domain.Interfaces;

namespace DryCleaner.Application.Services;

public class AnalyticsService(
    IOrderRepository orderRepository,
    IRepository<Client, int> clientRepository,
    IRepository<Item, int> itemRepository,
    IRepository<Category, int> categoryRepository) : IAnalyticsService
{
    public async Task<IReadOnlyList<OrderDto>> GetOrdersInProgressAsync()
    {
        var orders = await orderRepository.ReadAll();
        return orders
            .Where(o => o.Status == OrderStatus.InProgress)
            .OrderBy(o => o.AcceptanceDate)
            .Select(Mapper.Map)
            .ToList();
    }

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

    public async Task<IReadOnlyList<LongestOrderClientDto>> GetClientsWithLongestOrdersAsync()
    {
        var orders = await orderRepository.ReadAll();
        var clients = await clientRepository.ReadAll();

        if (!orders.Any()) return new List<LongestOrderClientDto>();

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

    public async Task<CategoryStatDto> GetTop5CategoriesAsync(DateTime from, DateTime to)
    {
        var orders = await orderRepository.ReadAll();
        var items = await itemRepository.ReadAll();
        var categories = await categoryRepository.ReadAll();

        // 1. Считаем сколько раз встречается каждая категория
        var categoryStats = orders
            .Where(o => o.AcceptanceDate >= from && o.AcceptanceDate <= to)
            .Join(items, o => o.ItemId, i => i.Id, (o, i) => i.CategoryId) // Связываем заказы с категориями через изделия
            .GroupBy(catId => catId)
            .Select(g => {
                var cat = categories.FirstOrDefault(c => c.Id == g.Key);
                return new CategoryCountDto(g.Key, cat?.Name ?? "Неизвестно", g.Count());
            })
            .ToList();

        // 2. Берем топ-5 популярных и топ-5 непопулярных
        var mostPopular = categoryStats.OrderByDescending(x => x.OrdersCount).Take(5).ToList();
        var leastPopular = categoryStats.OrderBy(x => x.OrdersCount).Take(5).ToList();

        return new CategoryStatDto(mostPopular, leastPopular);
    }

    public async Task<MaxSpenderDto?> GetMaxSpenderAsync()
    {
        var orders = await orderRepository.ReadAll();
        var clients = await clientRepository.ReadAll();
        var items = await itemRepository.ReadAll();
        var categories = await categoryRepository.ReadAll();

        // Считаем сумму для каждого клиента
        var totals = orders
            .GroupBy(o => o.ClientId)
            .Select(g => {
                var client = clients.FirstOrDefault(c => c.Id == g.Key);

                // Сумма = сумма цен категорий всех изделий в заказах клиента
                var totalSpent = g.Sum(o => {
                    var item = items.FirstOrDefault(i => i.Id == o.ItemId);
                    var category = categories.FirstOrDefault(c => c.Id == item?.CategoryId);
                    return category?.CleaningPrice ?? 0;
                });

                return new MaxSpenderDto(g.Key, client?.FullName ?? "Неизвестно", totalSpent);
            })
            .ToList();

        if (totals.Count == 0) return null;

        // Сортируем по убыванию суммы и берем первого
        return totals.OrderByDescending(x => x.TotalSpent).FirstOrDefault();
    }
}