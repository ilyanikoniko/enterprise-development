namespace DryCleaner.Contracts.Dtos.Analytics;

/// <summary>
/// DTO со статистикой по категориям изделий
/// </summary>
public record CategoryStatDto(
    IReadOnlyList<CategoryCountDto> MostPopular,
    IReadOnlyList<CategoryCountDto> LeastPopular
);

/// <summary>
/// DTO с информацией о количестве заказов для конкретной категории
/// </summary>
public record CategoryCountDto(int CategoryId, string CategoryName, int OrdersCount);