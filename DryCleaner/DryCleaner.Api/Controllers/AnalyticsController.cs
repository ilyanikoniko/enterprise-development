using DryCleaner.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DryCleaner.Contracts.Dtos.Orders;
using DryCleaner.Contracts.Dtos.Analytics;

namespace DryCleaner.Api.Controllers;

/// <summary>
/// Контроллер для выполнения аналитических запросов по данным химчистки
/// Доступ к методам контроллера ограничен (требуется JWT-токен)
/// </summary>
/// <param name="service">Сервис аналитики</param>
/// <param name="logger">Логгер для записи информации о запросах</param>
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class AnalyticsController(
    IAnalyticsService service,
    ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// Получить список заказов находящихся в обработке (упорядоченных по дате приема)
    /// </summary>
    /// <returns>Список DTO заказов в обработке</returns>
    /// <response code="200">Список успешно сформирован</response>
    /// <response code="204">Список пуст</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpGet("orders-in-progress")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetOrdersInProgress()
    {
        logger.LogInformation("Вызван метод {method} контроллера {controller}", 
            nameof(GetOrdersInProgress), GetType().Name);
        try
        {
            var res = await service.GetOrdersInProgressAsync();
            logger.LogInformation("Метод {method} контроллера {controller} успешно выполнен", 
                nameof(GetOrdersInProgress), GetType().Name);
            return res.Count > 0 ? Ok(res) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при выполнении метода {method} контроллера {controller}", 
                nameof(GetOrdersInProgress), GetType().Name);
            return StatusCode(500, "Внутренняя ошибка сервера. Пожалуйста, повторите попытку позже.");
        }
    }

    /// <summary>
    /// Получить топ-5 клиентов по количеству заказов за указанный период
    /// </summary>
    /// <param name="from">Дата начала периода</param>
    /// <param name="to">Дата окончания периода</param>
    /// <returns>Список DTO топ-5 клиентов</returns>
    [HttpGet("top5-clients")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IReadOnlyList<TopClientDto>>> GetTop5Clients([FromQuery] DateTime from, [FromQuery] DateTime to)
    {
        logger.LogInformation("Вызван метод {method} контроллера {controller} с параметрами: с {from} по {to}", 
            nameof(GetTop5Clients), GetType().Name, from, to);
        try
        {
            var res = await service.GetTop5ClientsAsync(from, to);
            logger.LogInformation("Метод {method} контроллера {controller} успешно выполнен", 
                nameof(GetTop5Clients), GetType().Name);
            return res.Count > 0 ? Ok(res) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при выполнении метода {method} контроллера {controller}", 
                nameof(GetTop5Clients), GetType().Name);
            return StatusCode(500, "Внутренняя ошибка сервера. Пожалуйста, повторите попытку позже.");
        }
    }

    /// <summary>
    /// Получить клиентов, чьи заказы обрабатывались дольше всего (упорядочены по ФИО)
    /// </summary>
    /// <returns>Список DTO клиентов с максимальным сроком выполнения заказа</returns>
    [HttpGet("longest-orders")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IReadOnlyList<LongestOrderClientDto>>> GetLongestOrders()
    {
        logger.LogInformation("Вызван метод {method} контроллера {controller}", 
            nameof(GetLongestOrders), GetType().Name);
        try
        {
            var res = await service.GetClientsWithLongestOrdersAsync();
            logger.LogInformation("Метод {method} контроллера {controller} успешно выполнен", 
                nameof(GetLongestOrders), GetType().Name);
            return res.Count > 0 ? Ok(res) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при выполнении метода {method} контроллера {controller}", 
                nameof(GetLongestOrders), GetType().Name);
            return StatusCode(500, "Внутренняя ошибка сервера. Пожалуйста, повторите попытку позже.");
        }
    }

    /// <summary>
    /// Получить топ-5 самых популярных и самых непопулярных категорий изделий за указанный период
    /// </summary>
    /// <param name="from">Дата начала периода</param>
    /// <param name="to">Дата окончания периода</param>
    /// <returns>DTO со списками популярных и непопулярных категорий</returns>
    [HttpGet("top5-categories")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<CategoryStatDto>> GetTop5Categories([FromQuery] DateTime from, [FromQuery] DateTime to)
    {
        logger.LogInformation("Вызван метод {method} контроллера {controller} с параметрами: с {from} по {to}", 
            nameof(GetTop5Categories), GetType().Name, from, to);
        try
        {
            var res = await service.GetTop5CategoriesAsync(from, to);
            logger.LogInformation("Метод {method} контроллера {controller} успешно выполнен", 
                nameof(GetTop5Categories), GetType().Name);
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при выполнении метода {method} контроллера {controller}", 
                nameof(GetTop5Categories), GetType().Name);
            return StatusCode(500, "Внутренняя ошибка сервера. Пожалуйста, повторите попытку позже.");
        }
    }

    /// <summary>
    /// Получить клиента, потратившего наибольшую сумму за все время
    /// </summary>
    /// <returns>DTO клиента с максимальной суммой трат</returns>
    [HttpGet("max-spender")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<MaxSpenderDto>> GetMaxSpender()
    {
        logger.LogInformation("Вызван метод {method} контроллера {controller}", 
            nameof(GetMaxSpender), GetType().Name);
        try
        {
            var res = await service.GetMaxSpenderAsync();
            logger.LogInformation("Метод {method} контроллера {controller} успешно выполнен", 
                nameof(GetMaxSpender), GetType().Name);
            return res != null ? Ok(res) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при выполнении метода {method} контроллера {controller}", 
                nameof(GetMaxSpender), GetType().Name);
            return StatusCode(500, "Внутренняя ошибка сервера. Пожалуйста, повторите попытку позже.");
        }
    }
}