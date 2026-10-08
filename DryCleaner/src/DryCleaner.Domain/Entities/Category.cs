namespace DryCleaner.Domain.Entities;

/// <summary>
/// Категория изделия (справочник)
/// </summary>
public class Category
{
    /// <summary>
    /// Идентификатор категории
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Название категории
    /// </summary>
    /// <example>Верхняя одежда</example>
    public required string Name { get; set; }

    /// <summary>
    /// Рекомендуемый вид чистки
    /// </summary>
    public required string RecommendedCleaningType { get; set; }

    /// <summary>
    /// Стоимость чистки
    /// </summary>
    public required decimal CleaningPrice { get; set; }
}