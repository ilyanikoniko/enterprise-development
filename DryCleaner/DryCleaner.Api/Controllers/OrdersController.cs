using DryCleaner.Contracts.Dtos;
using DryCleaner.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc; 

namespace DryCleaner.Api.Controllers;

/// <summary>
/// Контроллер для работы с заказами
/// </summary>
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
    /// <response code="404">Заказы для клиента не найдены</response>
    [HttpGet("client/{clientId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetByClient(int clientId)
    {
        logger.LogInformation("Getting orders for client {ClientId}", clientId);
        var orders = await crudService.GetByClientAsync(clientId);

        return orders.Count > 0 ? Ok(orders) : NotFound();
    }
}