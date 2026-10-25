namespace DryCleaner.Contracts.Dtos;

/// <summary>
/// DTO для клиента, потратившего наибольшую сумму
/// </summary>
public record MaxSpenderDto(int ClientId, string ClientFullName, decimal TotalSpent);