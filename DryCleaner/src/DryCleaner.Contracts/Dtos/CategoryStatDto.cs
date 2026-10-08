namespace DryCleaner.Contracts.Dtos;

/// <summary>
/// DTO со статистикой по категориям.
/// </summary>
public class CategoryStatDto
{
    /// <summary>
    /// Топ-5 популярных категорий
    /// </summary>
    public required IReadOnlyList<CategoryCountDto> MostPopular { get; set; }

    /// <summary>
    /// Топ-5 непопулярных категорий
    /// </summary>
    public required IReadOnlyList<CategoryCountDto> LeastPopular { get; set; }
}

/// <summary>
/// DTO с количеством заказов по категории
/// </summary>
public class CategoryCountDto
{
    /// <summary>
    /// Идентификатор категории
    /// </summary>
    public int CategoryId { get; set; }

    /// <summary>
    /// Название категории
    /// </summary>
    public required string CategoryName { get; set; }

    /// <summary>
    /// Количество заказов
    /// </summary>
    public int OrdersCount { get; set; }
}