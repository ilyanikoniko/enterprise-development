using System.ComponentModel.DataAnnotations;

namespace DryCleaner.Contracts.Dtos;

/// <summary>
/// DTO изделия (для создания)
/// </summary>
public class ItemCreateDto
{
    /// <summary>
    /// Наименование изделия
    /// </summary>
    [Required(ErrorMessage = "Наименование обязательно")]
    [MaxLength(200)]
    public required string Name { get; set; }

    /// <summary>
    /// Материал изделия
    /// </summary>
    [Required(ErrorMessage = "Материал обязателен")]
    [MaxLength(100)]
    public required string Material { get; set; }

    /// <summary>
    /// Идентификатор категории
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Выберите категорию")]
    public int CategoryId { get; set; }
}