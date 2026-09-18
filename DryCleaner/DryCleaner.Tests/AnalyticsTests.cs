using DryCleaner.Domain.Enums;

namespace DryCleaner.Tests;

/// <summary>
/// Аналитические тесты для химчистки
/// </summary>
public class AnalyticsTests : IClassFixture<DryCleanerFixture>
{
    private readonly DryCleanerFixture _fixture;

    /// <summary>
    /// Инициализцаия тестового класса фикстурой
    /// </summary>
    public AnalyticsTests(DryCleanerFixture fixture)
    {
        _fixture = fixture;
    }

    /// <summary>
    /// Тест 1: заказы, находящиеся в обработке, упорядоченные по дате приёма
    /// </summary>
    [Fact]
    public void OrdersInProgressSortedByAcceptanceDate()
    {
        var result = _fixture.Orders
            .Where(o => o.Status == OrderStatus.InProgress)
            .OrderBy(o => o.AcceptanceDate)
            .ToList();

        Assert.NotEmpty(result);
        Assert.All(result, o => Assert.Equal(OrderStatus.InProgress, o.Status));

        for (var i = 1; i < result.Count; i++)
        {
            Assert.True(result[i - 1].AcceptanceDate <= result[i].AcceptanceDate);
        }
    }

    /// <summary>
    /// Тест 2: топ-5 клиентов, сдавших больше всего изделий за заданный период
    /// </summary>
    [Fact]
    public void Top5ClientsByOrdersCountForPeriod()
    {
        var dateFrom = new DateTime(2025, 1, 1);
        var dateTo = new DateTime(2026, 12, 31);

        var result = _fixture.Orders
            .Where(o => o.AcceptanceDate >= dateFrom && o.AcceptanceDate <= dateTo)
            .GroupBy(o => o.Client)
            .Select(g => new
            {
                Client = g.Key,
                OrdersCount = g.Count()
            })
            .OrderByDescending(x => x.OrdersCount)
            .Take(5)
            .ToList();

        Assert.Equal(5, result.Count);
        Assert.All(result, x => Assert.NotNull(x.Client));

        for (var i = 1; i < result.Count; i++)
        {
            Assert.True(result[i - 1].OrdersCount >= result[i].OrdersCount);
        }
    }

    /// <summary>
    /// Тест 3: клиенты, чьи заказы обрабатывались дольше всего, упорядоченные по ФИО
    /// </summary>
    [Fact]
    public void ClientsWithLongestOrdersSortedByName()
    {
        var result = _fixture.Orders
            .GroupBy(o => o.Client)
            .Select(g => new
            {
                Client = g.Key,
                AverageDays = g.Average(o => o.CompletionDays)
            })
            .OrderByDescending(x => x.AverageDays)
            .ThenBy(x => x.Client!.FullName)
            .ToList();

        Assert.NotEmpty(result);

        for (var i = 1; i < result.Count; i++)
        {
            if (result[i - 1].AverageDays == result[i].AverageDays)
            {
                Assert.True(
                    string.Compare(
                        result[i - 1].Client!.FullName,
                        result[i].Client!.FullName,
                        StringComparison.Ordinal) <= 0);
            }
            else
            {
                Assert.True(result[i - 1].AverageDays >= result[i].AverageDays);
            }
        }
    }

    /// <summary>
    /// Тест 4: топ-5 наиболее и наименее популярных категорий изделий за последний год
    /// </summary>
    [Fact]
    public void Top5PopularAndUnpopularCategoriesForLastYear()
    {
        var dateTo = new DateTime(2026, 9, 18);
        var dateFrom = dateTo.AddYears(-1);

        var categories = _fixture.Orders
            .Where(o => o.AcceptanceDate >= dateFrom && o.AcceptanceDate <= dateTo)
            .GroupBy(o => o.Item!.Category)
            .Select(g => new
            {
                Category = g.Key,
                OrdersCount = g.Count()
            })
            .ToList();

        var mostPopular = categories
            .OrderByDescending(x => x.OrdersCount)
            .Take(5)
            .ToList();

        var leastPopular = categories
            .OrderBy(x => x.OrdersCount)
            .Take(5)
            .ToList();

        Assert.Equal(5, mostPopular.Count);
        Assert.Equal(5, leastPopular.Count);

        for (var i = 1; i < mostPopular.Count; i++)
        {
            Assert.True(mostPopular[i - 1].OrdersCount >= mostPopular[i].OrdersCount);
        }

        for (var i = 1; i < leastPopular.Count; i++)
        {
            Assert.True(leastPopular[i - 1].OrdersCount <= leastPopular[i].OrdersCount);
        }
    }

    /// <summary>
    /// Тест 5: клиент, потративший наибольшую сумму за весь период работы химчистки
    /// </summary>
    [Fact]
    public void ClientWithMaxTotalSpent()
    {
        var result = _fixture.Orders
            .GroupBy(o => o.Client)
            .Select(g => new
            {
                Client = g.Key,
                TotalSpent = g.Sum(o => o.Item!.Category!.CleaningPrice)
            })
            .OrderByDescending(x => x.TotalSpent)
            .First();

        Assert.NotNull(result.Client);
        Assert.True(result.TotalSpent > 0);
    }
}