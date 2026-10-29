namespace DryCleaner.Contracts.Dtos.Analytics;

/// <summary>
/// DTO для топа клиентов
/// </summary>
public record TopClientDto(int ClientId, string ClientFullName, int OrdersCount);