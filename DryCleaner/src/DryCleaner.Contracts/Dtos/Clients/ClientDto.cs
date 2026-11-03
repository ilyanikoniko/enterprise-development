namespace DryCleaner.Contracts.Dtos.Clients;

/// <summary>
/// DTO клиента для чтения
/// </summary>
/// <param name="Id">Идентификатор клиента</param>
/// <param name="FullName">ФИО клиента</param>
/// <param name="PhoneNumber">Номер телефона</param>
public record ClientDto(int Id, string FullName, string PhoneNumber);