using System.ComponentModel.DataAnnotations;

namespace DryCleaner.Contracts.Dtos;

/// <summary>
/// DTO категории (для создания/редактирования)
/// </summary>
public class CategoryEditDto
{
    /// <summary>
    /// Название категории
    /// </summary>
    [Required(ErrorMessage = "Название обязательно")]
    [MaxLength(200)]
    public required string Name { get; set; }

    /// <summary>
    /// Рекомендуемый вид чистки
    /// </summary>
    [Required(ErrorMessage = "Вид чистки обязателен")]
    [MaxLength(100)]
    public required string RecommendedCleaningType { get; set; }

    /// <summary>
    /// Стоимость чистки
    /// </summary>
    [Range(1, 100000, ErrorMessage = "Цена должна быть от 1 до 100000")]
    public decimal CleaningPrice { get; set; }
}