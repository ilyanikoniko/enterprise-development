using DryCleaner.Contracts.Dtos.Orders;
using DryCleaner.Contracts.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DryCleaner.Api.Controllers;

/// <summary>
/// Контроллер для работы с заказами
/// </summary>
[Authorize]
public class OrdersController(
    IOrderService crudService,
    ILogger<OrdersController> logger)
    : CrudControllerBase<OrderDto, OrderCreateDto, int>(crudService, logger)
{
    /// <summary>
    /// Получить список заказов конкретного клиента
    /// </summary>
    /// <param name="clientId">Идентификатор клиента</param>
    /// <response code="200">Список успешно получен</response>
    [HttpGet("client/{clientId}")]
    [ProducesResponseType(200)]
    public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetByClient(int clientId)
    {
        logger.LogInformation("Запрос заказов для клиента с ID {ClientId}", clientId);

        var orders = await crudService.GetByClientAsync(clientId);

        // Если заказов нет, возвращаем пустой список (200 OK), а не 404
        return Ok(orders);
    }
}