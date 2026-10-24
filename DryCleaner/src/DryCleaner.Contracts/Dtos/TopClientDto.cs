namespace DryCleaner.Contracts.Dtos;

/// <summary>
/// DTO для топа клиентов
/// </summary>
public record TopClientDto(int ClientId, string ClientFullName, int OrdersCount);