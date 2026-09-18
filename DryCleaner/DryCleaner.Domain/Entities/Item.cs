namespace DryCleaner.Domain.Entities;

/// <summary>
/// Изделие принимаемое в химчистку
/// </summary>
public class Item
{
    /// <summary>
    /// Идентификатор изделия
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Наименование изделия
    /// </summary>
    /// <example>Пальто шерстяное</example>
    public required string Name { get; set; }

    /// <summary>
    /// Идентификатор категории
    /// </summary>
    public required int CategoryId { get; set; }

    /// <summary>
    /// Категория как объект
    /// </summary>
    public Category? Category { get; set; }

    /// <summary>
    /// Материал изделия 
    /// </summary>
    public required string Material { get; set; }
}