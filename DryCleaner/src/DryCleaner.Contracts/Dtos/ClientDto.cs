namespace DryCleaner.Contracts.Dtos;

/// <summary>
/// DTO клиента для чтения
/// </summary>
public record ClientDto(int Id, string FullName, string PhoneNumber);