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
