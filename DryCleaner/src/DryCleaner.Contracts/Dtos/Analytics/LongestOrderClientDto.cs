namespace DryCleaner.Contracts.Dtos.Analytics;

/// <summary>
/// DTO для клиента с самым долгим заказом
/// </summary>
public record LongestOrderClientDto(int ClientId, string ClientFullName, int MaxCompletionDays);