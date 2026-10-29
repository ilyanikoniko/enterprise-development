namespace DryCleaner.Contracts.Dtos.Orders;

/// <summary>
/// DTO заказа для чтения
/// </summary>
public record OrderDto(
    int Id,
    int ClientId,
    string ClientFullName,
    int ItemId,
    string ItemName,
    DateTime AcceptanceDate,
    int CompletionDays,
    string Status
);