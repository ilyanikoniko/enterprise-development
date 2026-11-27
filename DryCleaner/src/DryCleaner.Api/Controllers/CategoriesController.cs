using DryCleaner.Contracts.Dtos.Categories;
using DryCleaner.Contracts.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace DryCleaner.Api.Controllers;

/// <summary>
/// Контроллер для работы с категориями
/// </summary>
/// <param name="crudService">Сервис для работы с категориями</param>
/// <param name="logger">Логгер контроллера</param>
[Authorize]
public class CategoriesController(
    IApplicationService<CategoryDto, CategoryEditDto, int> crudService,
    ILogger<CategoriesController> logger)
    : CrudControllerBase<CategoryDto, CategoryEditDto, int>(crudService, logger);