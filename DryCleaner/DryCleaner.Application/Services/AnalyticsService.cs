using DryCleaner.Application.Mappers;
using DryCleaner.Contracts.Dtos;
using DryCleaner.Contracts.Interfaces;
using DryCleaner.Domain.Enums;
using DryCleaner.Domain.Interfaces;

namespace DryCleaner.Application.Services;

public class AnalyticsService(IOrderRepository orderRepository) : IAnalyticsService
{
    // Тест 1
    public async Task<IReadOnlyList<OrderDto>> GetOrdersInProgressAsync()
    {
        var orders = await orderRepository.ReadAll();
        return orders
            .Where(o => o.Status == OrderStatus.InProgress)
            .OrderBy(o => o.AcceptanceDate)
            .Select(Mapper.Map)
            .ToList();
    }

    // Тест 2
    public async Task<IReadOnlyList<TopClientDto>> GetTop5ClientsAsync(DateTime from, DateTime to)
    {
        var orders = await orderRepository.ReadAll();
        return orders
            .Where(o => o.AcceptanceDate >= from && o.AcceptanceDate <= to)
            .GroupBy(o => o.Client)
            .Select(g => new TopClientDto(g.Key!.Id, g.Key!.FullName, g.Count()))
            .OrderByDescending(x => x.OrdersCount)
            .Take(5)
            .ToList();
    }

    // Тест 3
    public async Task<IReadOnlyList<LongestOrderClientDto>> GetClientsWithLongestOrdersAsync()
    {
        var orders = await orderRepository.ReadAll();
        var maxDays = orders.Max(o => o.CompletionDays);

        return orders
            .Where(o => o.CompletionDays == maxDays)
            .Select(o => o.Client!)
            .Distinct()
            .OrderBy(c => c.FullName)
            .Select(c => new LongestOrderClientDto(c.Id, c.FullName, maxDays))
            .ToList();
    }

    // Тест 4
    public async Task<CategoryStatDto> GetTop5CategoriesAsync(DateTime from, DateTime to)
    {
        var orders = await orderRepository.ReadAll();

        var categories = orders
            .Where(o => o.AcceptanceDate >= from && o.AcceptanceDate <= to)
            .GroupBy(o => o.Item!.Category)
            .Select(g => new CategoryCountDto(g.Key!.Id, g.Key!.Name, g.Count()))
            .ToList();

        var mostPopular = categories.OrderByDescending(x => x.OrdersCount).Take(5).ToList();
        var leastPopular = categories.OrderBy(x => x.OrdersCount).Take(5).ToList();

        return new CategoryStatDto(mostPopular, leastPopular);
    }

    // Тест 5
    public async Task<MaxSpenderDto?> GetMaxSpenderAsync()
    {
        var orders = await orderRepository.ReadAll();

        var totals = orders
            .GroupBy(o => o.Client)
            .Select(g => new MaxSpenderDto(
                g.Key!.Id,
                g.Key!.FullName,
                g.Sum(o => o.Item!.Category!.CleaningPrice)))
            .ToList();

        if (totals.Count == 0) return null;

        var maxSpent = totals.Max(x => x.TotalSpent);
        return totals.OrderBy(x => x.ClientFullName).FirstOrDefault(x => x.TotalSpent == maxSpent);
    }
}