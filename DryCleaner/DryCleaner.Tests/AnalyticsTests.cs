using DryCleaner.Domain.Enums;

namespace DryCleaner.Tests;

/// <summary>
/// Аналитические тесты для химчистки
/// </summary>
public class AnalyticsTests(DryCleanerFixture fixture) : IClassFixture<DryCleanerFixture>
{
    private readonly DryCleanerFixture _fixture = fixture;

    /// <summary>
    /// Тест 1: заказы, находящиеся в обработке, упорядоченные по дате приёма
    /// </summary>
    [Fact]
    public void OrdersInProgressSortedByAcceptanceDate()
    {
        var expectedIds = new[] { 3, 41, 18, 36, 45, 47, 26, 48, 4, 15, 30, 8, 21, 50 };

        var result = _fixture.Orders
            .Where(o => o.Status == OrderStatus.InProgress)
            .OrderBy(o => o.AcceptanceDate)
            .ToList();

        Assert.Equal(expectedIds, result.Select(o => o.Id));
    }

    /// <summary>
    /// Тест 2: топ-5 клиентов, сдавших больше всего изделий за заданный период
    /// </summary>
    [Fact]
    public void Top5ClientsByOrdersCountForPeriod()
    {
        var dateFrom = new DateTime(2025, 1, 1);
        var dateTo = new DateTime(2026, 12, 31);

        var expectedClientIds = new[] { 1, 2, 3, 4, 5 };

        var result = _fixture.Orders
            .Where(o => o.AcceptanceDate >= dateFrom && o.AcceptanceDate <= dateTo)
            .GroupBy(o => o.Client)
            .Select(g => new { Client = g.Key, OrdersCount = g.Count() })
            .OrderByDescending(x => x.OrdersCount)
            .Take(5)
            .ToList();

        Assert.Equal(expectedClientIds, result.Select(x => x.Client!.Id));
    }

    /// <summary>
    /// Тест 3: топ-5 клиентов, чьи заказы обрабатывались дольше всего, упорядоченные по ФИО
    /// </summary>
    [Fact]
    public void ClientsWithLongestOrdersSortedByName()
    {
        var expectedClientIds = new[] { 12, 4, 8, 3, 13 };

        var result = _fixture.Orders
            .GroupBy(o => o.Client)
            .Select(g => new
            {
                Client = g.Key,
                MaxDays = g.Max(o => o.CompletionDays)
            })
            .OrderByDescending(x => x.MaxDays)
            .ThenBy(x => x.Client!.FullName)
            .Take(5)
            .ToList();

        Assert.Equal(expectedClientIds, result.Select(x => x.Client!.Id));
    }

    /// <summary>
    /// Тест 4: топ-5 наиболее и наименее популярных категорий изделий за последний год
    /// </summary>
    [Fact]
    public void Top5PopularAndUnpopularCategoriesForLastYear()
    {
        var dateTo = new DateTime(2026, 9, 18);
        var dateFrom = dateTo.AddYears(-1);

        var expectedMostPopularIds = new[] { 10, 5, 8, 6, 9 };
        var expectedLeastPopularIds = new[] { 1, 3, 4, 5, 8 };

        var categories = _fixture.Orders
            .Where(o => o.AcceptanceDate >= dateFrom && o.AcceptanceDate <= dateTo)
            .GroupBy(o => o.Item!.Category)
            .Select(g => new { Category = g.Key, OrdersCount = g.Count() })
            .ToList();

        var mostPopular = categories.OrderByDescending(x => x.OrdersCount).Take(5).ToList();
        var leastPopular = categories.OrderBy(x => x.OrdersCount).Take(5).ToList();

        Assert.Equal(expectedMostPopularIds, mostPopular.Select(x => x.Category!.Id));
        Assert.Equal(expectedLeastPopularIds, leastPopular.Select(x => x.Category!.Id));
    }

    /// <summary>
    /// Тест 5: клиент, потративший наибольшую сумму за весь период работы химчистки
    /// </summary>
    [Fact]
    public void ClientWithMaxTotalSpent()
    {
        var expectedClientId = 1;

        var result = _fixture.Orders
            .GroupBy(o => o.Client)
            .Select(g => new
            {
                Client = g.Key,
                TotalSpent = g.Sum(o => o.Item!.Category!.CleaningPrice)
            })
            .OrderByDescending(x => x.TotalSpent)
            .First();

        Assert.Equal(expectedClientId, result.Client!.Id);
    }
}