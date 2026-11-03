namespace DryCleaner.Contracts.Dtos.Orders;

/// <summary>
/// DTO заказа для чтения
/// </summary>
/// <param name="Id">Идентификатор заказа</param>
/// <param name="ClientId">Идентификатор клиента</param>
/// <param name="ClientFullName">ФИО клиента</param>
/// <param name="ItemId">Идентификатор изделия</param>
/// <param name="ItemName">Наименование изделия</param>
/// <param name="AcceptanceDate">Дата приема заказа</param>
/// <param name="CompletionDays">Срок выполнения в днях</param>
/// <param name="Status">Текущий статус заказа</param>
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