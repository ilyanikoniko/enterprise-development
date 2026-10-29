using DryCleaner.Contracts.Dtos.Categories;
using DryCleaner.Contracts.Dtos.Clients;
using DryCleaner.Contracts.Dtos.Items;
using DryCleaner.Contracts.Dtos.Orders;
using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Enums;

namespace DryCleaner.Application.Mappers;

/// <summary>
/// Статический класс для преобразования доменных сущностей в DTO и обратно
/// </summary>
public static class Mapper
{
    // ================= КЛИЕНТЫ =================

    /// <summary>
    /// Преобразует сущность Client в DTO для чтения
    /// </summary>
    public static ClientDto Map(Client entity) =>
        new(entity.Id, entity.FullName, entity.PhoneNumber);

    /// <summary>
    /// Преобразует DTO клиента для создания/обновления в доменную сущность
    /// </summary>
    public static Client Map(ClientEditDto dto) =>
        new() { FullName = dto.FullName, PhoneNumber = dto.PhoneNumber };

    /// <summary>
    /// Преобразует коллекцию сущностей Client в коллекцию DTO
    /// </summary>
    public static List<ClientDto> Map(IEnumerable<Client> entities) =>
        [.. entities.Select(Map)];


    // ================= КАТЕГОРИИ =================

    /// <summary>
    /// Преобразует сущность Category в DTO для чтения
    /// </summary>
    public static CategoryDto Map(Category entity) =>
        new(entity.Id, entity.Name, entity.RecommendedCleaningType, entity.CleaningPrice);

    /// <summary>
    /// Преобразует DTO категории для создания/обновления в доменную сущность
    /// </summary>
    public static Category Map(CategoryEditDto dto) =>
        new() { Name = dto.Name, RecommendedCleaningType = dto.RecommendedCleaningType, CleaningPrice = dto.CleaningPrice };

    /// <summary>
    /// Преобразует коллекцию сущностей Category в коллекцию DTO
    /// </summary>
    public static List<CategoryDto> Map(IEnumerable<Category> entities) =>
        [.. entities.Select(Map)];


    // ================= ИЗДЕЛИЯ =================

    /// <summary>
    /// Преобразует сущность Iten в DTO для чтения
    /// </summary>
    public static ItemDto Map(Item entity) =>
        new(entity.Id, entity.Name, entity.Material, entity.CategoryId, entity.Category?.Name ?? "Без категории");

    /// <summary>
    /// Преобразует DTO изделия (для создания) в доменную сущность
    /// </summary>
    public static Item Map(ItemCreateDto dto) =>
        new() { Name = dto.Name, Material = dto.Material, CategoryId = dto.CategoryId };

    /// <summary>
    /// Преобразует коллекцию сущностей Item в коллекцию DTO
    /// </summary>
    public static List<ItemDto> Map(IEnumerable<Item> entities) =>
        [.. entities.Select(Map)];


    // ================= ЗАКАЗЫ =================

    /// <summary>
    /// Преобразует сущность Order в DTO для чтения
    /// </summary>
    public static OrderDto Map(Order entity) =>
        new(entity.Id,
            entity.ClientId,
            entity.Client?.FullName ?? "Неизвестно",
            entity.ItemId,
            entity.Item?.Name ?? "Неизвестно",
            entity.AcceptanceDate,
            entity.CompletionDays,
            entity.Status.ToString());

    /// <summary>
    /// Преобразует DTO заказа (для создания) в доменную сущность
    /// </summary>
    public static Order Map(OrderCreateDto dto) =>
        new() { 
            ClientId = dto.ClientId, 
            ItemId = dto.ItemId, 
            AcceptanceDate = dto.AcceptanceDate, 
            CompletionDays = dto.CompletionDays, 
            Status = Enum.Parse<OrderStatus>(dto.Status) 
        };

    /// <summary>
    /// Преобразует коллекцию сущностей Order в коллекцию DTO
    /// </summary>
    public static List<OrderDto> Map(IEnumerable<Order> entities) =>
        [.. entities.Select(Map)];
}