namespace DryCleaner.Contracts.Dtos.Categories;

/// <summary>
/// DTO категории для чтения
/// </summary>
public record CategoryDto(int Id, string Name, string RecommendedCleaningType, decimal CleaningPrice);