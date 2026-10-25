using DryCleaner.Domain.Enums;

namespace DryCleaner.Contracts.Dtos;

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
    OrderStatus Status
);