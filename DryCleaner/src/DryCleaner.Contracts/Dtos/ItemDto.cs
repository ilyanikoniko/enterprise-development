namespace DryCleaner.Contracts.Dtos;

/// <summary>
/// DTO изделия для чтения
/// </summary>
public record ItemDto(int Id, string Name, string Material, int CategoryId, string CategoryName);