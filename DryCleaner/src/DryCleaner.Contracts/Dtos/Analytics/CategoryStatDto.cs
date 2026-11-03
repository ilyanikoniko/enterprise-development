namespace DryCleaner.Contracts.Dtos.Analytics;

/// <summary>
/// DTO со статистикой по категориям изделий
/// </summary>
/// <param name="MostPopular">Список топ-5 самых популярных категорий</param>
/// <param name="LeastPopular">Список топ-5 самых непопулярных категорий</param>
public record CategoryStatDto(
    IReadOnlyList<CategoryCountDto> MostPopular,
    IReadOnlyList<CategoryCountDto> LeastPopular
);

/// <summary>
/// DTO с информацией о количестве заказов для конкретной категории
/// </summary>
/// <param name="CategoryId">Идентификатор категории</param>
/// <param name="CategoryName">Название категории</param>
/// <param name="OrdersCount">Количество заказов</param>
public record CategoryCountDto(int CategoryId, string CategoryName, int OrdersCount);