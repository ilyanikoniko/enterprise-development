namespace DryCleaner.Contracts.Dtos.Analytics;

/// <summary>
/// DTO для топа клиентов
/// </summary>
/// <param name="ClientId">Идентификатор клиента</param>
/// <param name="ClientFullName">ФИО клиента</param>
/// <param name="OrdersCount">Количество заказов</param>
public record TopClientDto(int ClientId, string ClientFullName, int OrdersCount);