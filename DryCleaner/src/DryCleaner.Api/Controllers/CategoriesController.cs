using DryCleaner.Contracts.Dtos.Categories;
using DryCleaner.Contracts.Interfaces;

namespace DryCleaner.Api.Controllers;

/// <summary>
/// Контроллер для работы с категориями
/// </summary>
public class CategoriesController(
    IApplicationService<CategoryDto, CategoryEditDto, int> crudService,
    ILogger<CategoriesController> logger)
    : CrudControllerBase<CategoryDto, CategoryEditDto, int>(crudService, logger);