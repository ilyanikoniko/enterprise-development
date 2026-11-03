namespace DryCleaner.Contracts.Dtos.Analytics;

/// <summary>
/// DTO для клиента, потратившего наибольшую сумму
/// </summary>
/// <param name="ClientId">Идентификатор клиента</param>
/// <param name="ClientFullName">ФИО клиента</param>
/// <param name="TotalSpent">Общая потраченная сумма</param>
public record MaxSpenderDto(int ClientId, string ClientFullName, decimal TotalSpent);