using DryCleaner.Contracts.Dtos.Items;
using DryCleaner.Contracts.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace DryCleaner.Api.Controllers;

/// <summary>
/// Контроллер для работы с изделиями
/// </summary>
[Authorize]
public class ItemsController(
    IApplicationService<ItemDto, ItemCreateDto, int> crudService,
    ILogger<ItemsController> logger)
    : CrudControllerBase<ItemDto, ItemCreateDto, int>(crudService, logger);