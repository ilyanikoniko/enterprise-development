using System.ComponentModel.DataAnnotations;

namespace DryCleaner.Contracts.Dtos;

/// <summary>
/// DTO изделия (для обновления)
/// </summary>
public class ItemUpdateDto
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
}