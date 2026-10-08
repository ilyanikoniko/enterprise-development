namespace DryCleaner.Contracts.Dtos;

/// <summary>
/// DTO категории (для чтения)
/// </summary>
public class CategoryDto
{
    /// <summary>
    /// Идентификатор категории
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название категории
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Рекомендуемый вид чистки
    /// </summary>
    public required string RecommendedCleaningType { get; set; }

    /// <summary>
    /// Стоимость чистки
    /// </summary>
    public decimal CleaningPrice { get; set; }
}