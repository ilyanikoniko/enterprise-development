using System.ComponentModel.DataAnnotations;

namespace DryCleaner.Contracts.Dtos;

/// <summary>
/// DTO клиента (для создания/редактирования)
/// </summary>
public class ClientEditDto
{
    /// <summary>
    /// ФИО клиента
    /// </summary>
    [Required(ErrorMessage = "ФИО обязательно")]
    [MaxLength(200)]
    public required string FullName { get; set; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    [Required(ErrorMessage = "Телефон обязателен")]
    [Phone(ErrorMessage = "Неверный формат телефона")]
    [MaxLength(20)]
    public required string PhoneNumber { get; set; }
}