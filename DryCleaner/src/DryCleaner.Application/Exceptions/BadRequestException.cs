namespace DryCleaner.Application.Exceptions;

/// <summary>
/// Исключение которое указывает на некорректные данные в запросе (ошибка клиента)
/// </summary>
public class BadRequestException(string message) : Exception(message) { }