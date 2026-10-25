using DryCleaner.Contracts.Dtos;
using DryCleaner.Contracts.Interfaces;

namespace DryCleaner.Api.Controllers;

/// <summary>
/// Контроллер для работы с заказами
/// </summary>
public class OrdersController(
    IApplicationService<OrderDto, OrderCreateDto, int> crudService,
    ILogger<OrdersController> logger)
    : CrudControllerBase<OrderDto, OrderCreateDto, int>(crudService, logger);