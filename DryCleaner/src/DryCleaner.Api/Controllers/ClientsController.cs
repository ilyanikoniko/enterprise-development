using DryCleaner.Contracts.Dtos.Clients;
using DryCleaner.Contracts.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace DryCleaner.Api.Controllers;

/// <summary>
/// Контроллер для работы с клиентами
/// </summary>
[Authorize]
public class ClientsController(
    IApplicationService<ClientDto, ClientEditDto, int> crudService,
    ILogger<ClientsController> logger)
    : CrudControllerBase<ClientDto, ClientEditDto, int>(crudService, logger);