namespace DryCleaner.Contracts.Dtos;

/// <summary>
/// DTO изделия (для чтения)
/// </summary>
public class ItemDto
{
    /// <summary>
    /// Идентификатор изделия
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Наименование изделия
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Материал изделия
    /// </summary>
    public required string Material { get; set; }

    /// <summary>
    /// Идентификатор категории
    /// </summary>
    public int CategoryId { get; set; }

    /// <summary>
    /// Название категории
    /// </summary>
    public string CategoryName { get; set; } = string.Empty;
}