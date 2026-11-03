namespace DryCleaner.Contracts.Dtos.Categories;

/// <summary>
/// DTO категории для чтения
/// </summary>
/// <param name="Id">Идентификатор категории</param>
/// <param name="Name">Название категории</param>
/// <param name="RecommendedCleaningType">Рекомендуемый вид чистки</param>
/// <param name="CleaningPrice">Стоимость чистки</param>
public record CategoryDto(int Id, string Name, string RecommendedCleaningType, decimal CleaningPrice);