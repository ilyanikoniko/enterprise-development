using DryCleaner.Contracts.Dtos;
using DryCleaner.Domain.Entities;

namespace DryCleaner.Application.Mappers;

public static class Mapper
{
    // ================= КЛИЕНТЫ =================
    public static ClientDto Map(Client entity) =>
        new(entity.Id, entity.FullName, entity.PhoneNumber);

    public static Client Map(ClientEditDto dto) =>
        new() { FullName = dto.FullName, PhoneNumber = dto.PhoneNumber };

    public static List<ClientDto> Map(IEnumerable<Client> entities) =>
        [.. entities.Select(Map)];


    // ================= КАТЕГОРИИ =================
    public static CategoryDto Map(Category entity) =>
        new(entity.Id, entity.Name, entity.RecommendedCleaningType, entity.CleaningPrice);

    public static Category Map(CategoryEditDto dto) =>
        new() { Name = dto.Name, RecommendedCleaningType = dto.RecommendedCleaningType, CleaningPrice = dto.CleaningPrice };

    public static List<CategoryDto> Map(IEnumerable<Category> entities) =>
        [.. entities.Select(Map)];


    // ================= ИЗДЕЛИЯ =================
    public static ItemDto Map(Item entity) =>
        new(entity.Id, entity.Name, entity.Material, entity.CategoryId, entity.Category?.Name ?? "Без категории");

    public static Item Map(ItemCreateDto dto) =>
        new() { Name = dto.Name, Material = dto.Material, CategoryId = dto.CategoryId };

    public static List<ItemDto> Map(IEnumerable<Item> entities) =>
        [.. entities.Select(Map)];


    // ================= ЗАКАЗЫ =================
    public static OrderDto Map(Order entity) =>
        new(entity.Id,
            entity.ClientId,
            entity.Client?.FullName ?? "Неизвестно",
            entity.ItemId,
            entity.Item?.Name ?? "Неизвестно",
            entity.AcceptanceDate,
            entity.CompletionDays,
            entity.Status);

    public static Order Map(OrderCreateDto dto) =>
        new() { ClientId = dto.ClientId, ItemId = dto.ItemId, AcceptanceDate = dto.AcceptanceDate, CompletionDays = dto.CompletionDays, Status = dto.Status };

    public static List<OrderDto> Map(IEnumerable<Order> entities) =>
        [.. entities.Select(Map)];
}