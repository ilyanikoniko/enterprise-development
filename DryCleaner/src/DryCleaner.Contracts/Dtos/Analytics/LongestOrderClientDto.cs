namespace DryCleaner.Contracts.Dtos.Analytics;

/// <summary>
/// DTO для клиента с самым долгим заказом
/// </summary>
/// <param name="ClientId">Идентификатор клиента</param>
/// <param name="ClientFullName">ФИО клиента</param>
/// <param name="MaxCompletionDays">Максимальный срок выполнения заказа в днях</param>
public record LongestOrderClientDto(int ClientId, string ClientFullName, int MaxCompletionDays);