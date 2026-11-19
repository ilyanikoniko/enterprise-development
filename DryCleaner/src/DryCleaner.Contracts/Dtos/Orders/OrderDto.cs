using DryCleaner.Contracts.Dtos.Clients;
using DryCleaner.Contracts.Dtos.Items;

namespace DryCleaner.Contracts.Dtos.Orders;

/// <summary>
/// DTO заказа для чтения
/// </summary>
/// <param name="Id">Идентификатор заказа</param>
/// <param name="Client">Клиент</param>
/// <param name="Item">Изделие</param>
/// <param name="AcceptanceDate">Дата приема заказа</param>
/// <param name="CompletionDays">Срок выполнения в днях</param>
/// <param name="Status">Текущий статус заказа</param>
public record OrderDto(
    int Id,
    ClientDto Client,
    ItemDto Item,
    DateTime AcceptanceDate,
    int CompletionDays,
    string Status
);