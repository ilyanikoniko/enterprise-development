using DryCleaner.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace DryCleaner.Api.Controllers;

/// <summary>
/// Базовый абстрактный контроллер для выполнения CRUD операций над сущностями
/// </summary>
/// <param name="appService">Сервис приложения для работы с сущностью</param>
/// <param name="logger">Логгер для записи информации о запросах</param>
[Route("api/[controller]")]
[ApiController]
public abstract class CrudControllerBase<TDto, TCreateUpdateDto, TKey>(
    IApplicationService<TDto, TCreateUpdateDto, TKey> appService,
    ILogger<CrudControllerBase<TDto, TCreateUpdateDto, TKey>> logger) : ControllerBase
{

    /// <summary>
    /// Создать новую запись
    /// </summary>
    /// <response code="201">Запись успешно создана</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TDto>> Create(TCreateUpdateDto newDto)
    {
        logger.LogInformation("Вызван метод {method} контроллера {controller} с параметром {@dto}", 
            nameof(Create), GetType().Name, newDto);
        try
        {
            var res = await appService.Create(newDto);
            logger.LogInformation("Метод {method} контроллера {controller} успешно выполнен", 
                nameof(Create), GetType().Name);
            return CreatedAtAction(nameof(Create), res);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при выполнении метода {method} контроллера {controller}", 
                nameof(Create), GetType().Name);
            return StatusCode(500, "Внутренняя ошибка сервера. Пожалуйста, повторите попытку позже.");
        }
    }

    /// <summary>
    /// Обновить существующую запись по id
    /// </summary>
    /// <response code="200">Запись успешно обновлена</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TDto>> Edit(TKey id, TCreateUpdateDto newDto)
    {
        logger.LogInformation("Вызван метод {method} контроллера {controller} с параметрами {key}, {@dto}", 
            nameof(Edit), GetType().Name, id, newDto);
        try
        {
            var res = await appService.Update(newDto, id);
            logger.LogInformation("Метод {method} контроллера {controller} успешно выполнен", 
                nameof(Edit), GetType().Name);
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при выполнении метода {method} контроллера {controller}", 
                nameof(Edit), GetType().Name);
            return StatusCode(500, "Внутренняя ошибка сервера. Пожалуйста, повторите попытку позже.");
        }
    }

    /// <summary>
    /// Удалить запись по id
    /// </summary>
    /// <response code="200">Запись успешно удалена</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Delete(TKey id)
    {
        logger.LogInformation("Вызван метод {method} контроллера {controller} с параметром {id}", 
            nameof(Delete), GetType().Name, id);
        try
        {
            await appService.Delete(id);
            logger.LogInformation("Метод {method} контроллера {controller} успешно выполнен", 
                nameof(Delete), GetType().Name);
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при выполнении метода {method} контроллера {controller}", 
                nameof(Delete), GetType().Name);
            return StatusCode(500, "Внутренняя ошибка сервера. Пожалуйста, повторите попытку позже.");
        }
    }

    /// <summary>
    /// Получить список всех записей
    /// </summary>
    /// <response code="200">Список успешно получен</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<TDto>>> GetAll()
    {
        logger.LogInformation("Вызван метод {method} контроллера {controller}", 
            nameof(GetAll), GetType().Name);
        try
        {
            var res = await appService.GetAll();
            logger.LogInformation("Метод {method} контроллера {controller} успешно выполнен", 
                nameof(GetAll), GetType().Name);
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при выполнении метода {method} контроллера {controller}", 
                nameof(GetAll), GetType().Name);
            return StatusCode(500, "Внутренняя ошибка сервера. Пожалуйста, повторите попытку позже.");
        }
    }

    /// <summary>
    /// Получить запись по id
    /// </summary>
    /// <response code="200">Запись успешно найдена</response>
    /// <response code="404">Запись с указанным идентификатором не найдена</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TDto>> Get(TKey id)
    {
        logger.LogInformation("Вызван метод {method} контроллера {controller} с параметром {id}", 
            nameof(Get), GetType().Name, id);
        try
        {
            var res = await appService.Get(id);
            logger.LogInformation("Метод {method} контроллера {controller} успешно выполнен", 
                nameof(Get), GetType().Name);
            return res != null ? Ok(res) : NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при выполнении метода {method} контроллера {controller}", 
                nameof(Get), GetType().Name);
            return StatusCode(500, "Внутренняя ошибка сервера. Пожалуйста, повторите попытку позже.");
        }
    }
}