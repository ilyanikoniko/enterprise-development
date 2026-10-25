using DryCleaner.Contracts.Dtos;
using DryCleaner.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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
        logger.LogInformation("{method} method of {controller} is called", nameof(GetOrdersInProgress), GetType().Name);
        try
        {
            var res = await service.GetOrdersInProgressAsync();
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetOrdersInProgress), GetType().Name);
            return res.Count > 0 ? Ok(res) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetOrdersInProgress), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}");
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
        logger.LogInformation("{method} method of {controller} is called with from: {from}, to: {to}", nameof(GetTop5Clients), GetType().Name, from, to);
        try
        {
            var res = await service.GetTop5ClientsAsync(from, to);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetTop5Clients), GetType().Name);
            return res.Count > 0 ? Ok(res) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetTop5Clients), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}");
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
        logger.LogInformation("{method} method of {controller} is called", nameof(GetLongestOrders), GetType().Name);
        try
        {
            var res = await service.GetClientsWithLongestOrdersAsync();
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetLongestOrders), GetType().Name);
            return res.Count > 0 ? Ok(res) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetLongestOrders), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}");
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
        logger.LogInformation("{method} method of {controller} is called with from: {from}, to: {to}", nameof(GetTop5Categories), GetType().Name, from, to);
        try
        {
            var res = await service.GetTop5CategoriesAsync(from, to);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetTop5Categories), GetType().Name);
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetTop5Categories), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}");
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
        logger.LogInformation("{method} method of {controller} is called", nameof(GetMaxSpender), GetType().Name);
        try
        {
            var res = await service.GetMaxSpenderAsync();
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetMaxSpender), GetType().Name);
            return res != null ? Ok(res) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetMaxSpender), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}");
        }
    }
}