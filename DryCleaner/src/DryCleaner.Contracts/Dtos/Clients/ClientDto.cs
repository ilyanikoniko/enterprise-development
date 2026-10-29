namespace DryCleaner.Contracts.Dtos.Clients;

/// <summary>
/// DTO клиента для чтения
/// </summary>
public record ClientDto(int Id, string FullName, string PhoneNumber);