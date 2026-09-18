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
}