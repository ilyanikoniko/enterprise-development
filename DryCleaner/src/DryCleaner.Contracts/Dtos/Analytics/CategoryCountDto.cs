namespace DryCleaner.Contracts.Dtos.Analytics;

/// <summary>
/// DTO с информацией о количестве заказов для конкретной категории
/// </summary>
/// <param name="CategoryId">Идентификатор категории</param>
/// <param name="CategoryName">Название категории</param>
/// <param name="OrdersCount">Количество заказов</param>
public record CategoryCountDto(int CategoryId, string CategoryName, int OrdersCount);