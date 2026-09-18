using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Enums;

namespace DryCleaner.Domain.Data;

/// <summary>
/// Набор тестовых данных для химчистки
/// </summary>
public static class DataSeed
{
    /// <summary>
    /// Список категорий изделий
    /// </summary>
    public static List<Category> Categories { get; } = new()
    {
        new Category { Id = 1,  Name = "Верхняя одежда",   RecommendedCleaningType = "Аквачистка",         CleaningPrice = 1600m },
        new Category { Id = 2,  Name = "Деловые костюмы",  RecommendedCleaningType = "Сухая чистка",       CleaningPrice = 1400m },
        new Category { Id = 3,  Name = "Вечерние платья",  RecommendedCleaningType = "Ручная стирка",      CleaningPrice = 1100m },
        new Category { Id = 4,  Name = "Сорочки",          RecommendedCleaningType = "Машинная стирка",    CleaningPrice = 450m  },
        new Category { Id = 5,  Name = "Брюки и джинсы",   RecommendedCleaningType = "Аквачистка",         CleaningPrice = 750m  },
        new Category { Id = 6,  Name = "Пальто и шубы",    RecommendedCleaningType = "Сухая чистка",       CleaningPrice = 2200m },
        new Category { Id = 7,  Name = "Куртки и ветровки",RecommendedCleaningType = "Аквачистка",         CleaningPrice = 1350m },
        new Category { Id = 8,  Name = "Домашний текстиль",RecommendedCleaningType = "Ручная стирка",      CleaningPrice = 1900m },
        new Category { Id = 9,  Name = "Постельные принадлежности", RecommendedCleaningType = "Машинная стирка", CleaningPrice = 1500m },
        new Category { Id = 10, Name = "Обувь",            RecommendedCleaningType = "Специальный уход",   CleaningPrice = 950m  }
    };

    /// <summary>
    /// Список клиентов
    /// </summary>
    public static List<Client> Clients { get; } = new()
    {
        new Client { Id = 1,  FullName = "Романов Денис Олегович",        PhoneNumber = "+79161110001" },
        new Client { Id = 2,  FullName = "Ковалёва Марина Игоревна",      PhoneNumber = "+79161110002" },
        new Client { Id = 3,  FullName = "Егоров Павел Андреевич",        PhoneNumber = "+79161110003" },
        new Client { Id = 4,  FullName = "Тихонова Светлана Юрьевна",     PhoneNumber = "+79161110004" },
        new Client { Id = 5,  FullName = "Крылов Артём Витальевич",       PhoneNumber = "+79161110005" },
        new Client { Id = 6,  FullName = "Зайцева Полина Дмитриевна",     PhoneNumber = "+79161110006" },
        new Client { Id = 7,  FullName = "Медведев Кирилл Романович",     PhoneNumber = "+79161110007" },
        new Client { Id = 8,  FullName = "Орлова Вероника Павловна",      PhoneNumber = "+79161110008" },
        new Client { Id = 9,  FullName = "Гусев Станислав Игоревич",      PhoneNumber = "+79161110009" },
        new Client { Id = 10, FullName = "Ларина Алина Максимовна",       PhoneNumber = "+79161110010" },
        new Client { Id = 11, FullName = "Белов Егор Сергеевич",          PhoneNumber = "+79161110011" },
        new Client { Id = 12, FullName = "Панова Дарья Алексеевна",       PhoneNumber = "+79161110012" },
        new Client { Id = 13, FullName = "Фомин Никита Владимирович",     PhoneNumber = "+79161110013" },
        new Client { Id = 14, FullName = "Киселёва Софья Романовна",      PhoneNumber = "+79161110014" },
        new Client { Id = 15, FullName = "Шестаков Максим Олегович",      PhoneNumber = "+79161110015" }
    };

    /// <summary>
    /// Список изделий
    /// </summary>
    public static List<Item> Items { get; } = new()
    {
        new Item { Id = 1,  Name = "Пальто шерстяное",       Material = "Шерсть",        CategoryId = 1,  Category = Categories[0] },
        new Item { Id = 2,  Name = "Пуховик зимний",         Material = "Полиэстер",     CategoryId = 1,  Category = Categories[0] },
        new Item { Id = 3,  Name = "Костюм шерстяной",       Material = "Шерсть",        CategoryId = 2,  Category = Categories[1] },
        new Item { Id = 4,  Name = "Костюм льняной",         Material = "Лён",           CategoryId = 2,  Category = Categories[1] },
        new Item { Id = 5,  Name = "Платье шёлковое",        Material = "Шёлк",          CategoryId = 3,  Category = Categories[2] },
        new Item { Id = 6,  Name = "Платье хлопковое",       Material = "Хлопок",        CategoryId = 3,  Category = Categories[2] },
        new Item { Id = 7,  Name = "Сорочка хлопковая",      Material = "Хлопок",        CategoryId = 4,  Category = Categories[3] },
        new Item { Id = 8,  Name = "Сорочка льняная",        Material = "Лён",           CategoryId = 4,  Category = Categories[3] },
        new Item { Id = 9,  Name = "Брюки шерстяные",        Material = "Шерсть",        CategoryId = 5,  Category = Categories[4] },
        new Item { Id = 10, Name = "Джинсы классические",    Material = "Деним",         CategoryId = 5,  Category = Categories[4] },
        new Item { Id = 11, Name = "Пальто кашемировое",     Material = "Кашемир",       CategoryId = 6,  Category = Categories[5] },
        new Item { Id = 12, Name = "Шуба искусственная",     Material = "Полиэстер",     CategoryId = 6,  Category = Categories[5] },
        new Item { Id = 13, Name = "Куртка кожаная",         Material = "Кожа",          CategoryId = 7,  Category = Categories[6] },
        new Item { Id = 14, Name = "Ветровка спортивная",    Material = "Полиэстер",     CategoryId = 7,  Category = Categories[6] },
        new Item { Id = 15, Name = "Покрывало хлопковое",    Material = "Хлопок",        CategoryId = 8,  Category = Categories[7] },
        new Item { Id = 16, Name = "Скатерть льняная",       Material = "Лён",           CategoryId = 8,  Category = Categories[7] },
        new Item { Id = 17, Name = "Одеяло шерстяное",       Material = "Шерсть",        CategoryId = 9,  Category = Categories[8] },
        new Item { Id = 18, Name = "Плед флисовый",          Material = "Полиэстер",     CategoryId = 9,  Category = Categories[8] },
        new Item { Id = 19, Name = "Ботинки кожаные",        Material = "Кожа",          CategoryId = 10, Category = Categories[9] },
        new Item { Id = 20, Name = "Сапоги замшевые",        Material = "Замша",         CategoryId = 10, Category = Categories[9] }
    };

    /// <summary>
    /// Список заказов
    /// </summary>
    public static List<Order> Orders { get; } = new()
    {
        // Романов (1) — 8 заказов 
        new Order { Id = 1,  ClientId = 1,  Client = Clients[0],  ItemId = 1,  Item = Items[0],  AcceptanceDate = new DateTime(2024, 11, 12), CompletionDays = 7,  Status = OrderStatus.Completed },
        new Order { Id = 2,  ClientId = 1,  Client = Clients[0],  ItemId = 5,  Item = Items[4],  AcceptanceDate = new DateTime(2025, 2, 18), CompletionDays = 4,  Status = OrderStatus.Completed },
        new Order { Id = 3,  ClientId = 1,  Client = Clients[0],  ItemId = 9,  Item = Items[8],  AcceptanceDate = new DateTime(2025, 12, 3), CompletionDays = 12, Status = OrderStatus.InProgress },
        new Order { Id = 4,  ClientId = 1,  Client = Clients[0],  ItemId = 15, Item = Items[14], AcceptanceDate = new DateTime(2026, 4, 22), CompletionDays = 6,  Status = OrderStatus.InProgress },
        new Order { Id = 5,  ClientId = 1,  Client = Clients[0],  ItemId = 3,  Item = Items[2],  AcceptanceDate = new DateTime(2025, 6, 5),  CompletionDays = 5,  Status = OrderStatus.Completed },
        new Order { Id = 6,  ClientId = 1,  Client = Clients[0],  ItemId = 7,  Item = Items[6],  AcceptanceDate = new DateTime(2025, 9, 14), CompletionDays = 8,  Status = OrderStatus.Completed },
        new Order { Id = 7,  ClientId = 1,  Client = Clients[0],  ItemId = 11, Item = Items[10], AcceptanceDate = new DateTime(2026, 1, 20), CompletionDays = 10, Status = OrderStatus.Ready },
        new Order { Id = 8,  ClientId = 1,  Client = Clients[0],  ItemId = 17, Item = Items[16], AcceptanceDate = new DateTime(2026, 5, 30), CompletionDays = 9,  Status = OrderStatus.InProgress },

        // Ковалёва (2) — 7 заказов 
        new Order { Id = 9,  ClientId = 2,  Client = Clients[1],  ItemId = 2,  Item = Items[1],  AcceptanceDate = new DateTime(2024, 12, 5),  CompletionDays = 5,  Status = OrderStatus.Completed },
        new Order { Id = 10, ClientId = 2,  Client = Clients[1],  ItemId = 7,  Item = Items[6],  AcceptanceDate = new DateTime(2025, 5, 14), CompletionDays = 3,  Status = OrderStatus.Completed },
        new Order { Id = 11, ClientId = 2,  Client = Clients[1],  ItemId = 11, Item = Items[10], AcceptanceDate = new DateTime(2026, 1, 9),  CompletionDays = 8,  Status = OrderStatus.Ready },
        new Order { Id = 12, ClientId = 2,  Client = Clients[1],  ItemId = 4,  Item = Items[3],  AcceptanceDate = new DateTime(2025, 7, 22), CompletionDays = 6,  Status = OrderStatus.Completed },
        new Order { Id = 13, ClientId = 2,  Client = Clients[1],  ItemId = 13, Item = Items[12], AcceptanceDate = new DateTime(2025, 10, 8), CompletionDays = 11, Status = OrderStatus.Completed },
        new Order { Id = 14, ClientId = 2,  Client = Clients[1],  ItemId = 18, Item = Items[17], AcceptanceDate = new DateTime(2026, 3, 14), CompletionDays = 7,  Status = OrderStatus.Ready },
        new Order { Id = 15, ClientId = 2,  Client = Clients[1],  ItemId = 6,  Item = Items[5],  AcceptanceDate = new DateTime(2026, 5, 5),  CompletionDays = 9,  Status = OrderStatus.InProgress },

        // Егоров (3) — 6 заказов 
        new Order { Id = 16, ClientId = 3,  Client = Clients[2],  ItemId = 6,  Item = Items[5],  AcceptanceDate = new DateTime(2024, 10, 30), CompletionDays = 9,  Status = OrderStatus.Completed },
        new Order { Id = 17, ClientId = 3,  Client = Clients[2],  ItemId = 13, Item = Items[12], AcceptanceDate = new DateTime(2025, 9, 25), CompletionDays = 14, Status = OrderStatus.Completed },
        new Order { Id = 18, ClientId = 3,  Client = Clients[2],  ItemId = 17, Item = Items[16], AcceptanceDate = new DateTime(2025, 12, 28), CompletionDays = 16, Status = OrderStatus.InProgress },
        new Order { Id = 19, ClientId = 3,  Client = Clients[2],  ItemId = 9,  Item = Items[8],  AcceptanceDate = new DateTime(2025, 4, 17), CompletionDays = 5,  Status = OrderStatus.Completed },
        new Order { Id = 20, ClientId = 3,  Client = Clients[2],  ItemId = 19, Item = Items[18], AcceptanceDate = new DateTime(2026, 2, 12), CompletionDays = 8,  Status = OrderStatus.Ready },
        new Order { Id = 21, ClientId = 3,  Client = Clients[2],  ItemId = 15, Item = Items[14], AcceptanceDate = new DateTime(2026, 6, 18), CompletionDays = 10, Status = OrderStatus.InProgress },

        // Тихонова (4) — 5 заказов 
        new Order { Id = 22, ClientId = 4,  Client = Clients[3],  ItemId = 3,  Item = Items[2],  AcceptanceDate = new DateTime(2025, 1, 14), CompletionDays = 6,  Status = OrderStatus.Completed },
        new Order { Id = 23, ClientId = 4,  Client = Clients[3],  ItemId = 10, Item = Items[9],  AcceptanceDate = new DateTime(2025, 10, 8), CompletionDays = 18, Status = OrderStatus.Completed },
        new Order { Id = 24, ClientId = 4,  Client = Clients[3],  ItemId = 19, Item = Items[18], AcceptanceDate = new DateTime(2026, 2, 17), CompletionDays = 15, Status = OrderStatus.Ready },
        new Order { Id = 25, ClientId = 4,  Client = Clients[3],  ItemId = 14, Item = Items[13], AcceptanceDate = new DateTime(2025, 8, 3),  CompletionDays = 7,  Status = OrderStatus.Completed },
        new Order { Id = 26, ClientId = 4,  Client = Clients[3],  ItemId = 1,  Item = Items[0],  AcceptanceDate = new DateTime(2026, 4, 1),  CompletionDays = 12, Status = OrderStatus.InProgress },

        // Крылов (5) — 4 заказа 
        new Order { Id = 27, ClientId = 5,  Client = Clients[4],  ItemId = 8,  Item = Items[7],  AcceptanceDate = new DateTime(2025, 2, 27), CompletionDays = 5,  Status = OrderStatus.Completed },
        new Order { Id = 28, ClientId = 5,  Client = Clients[4],  ItemId = 14, Item = Items[13], AcceptanceDate = new DateTime(2025, 11, 12), CompletionDays = 7,  Status = OrderStatus.Completed },
        new Order { Id = 29, ClientId = 5,  Client = Clients[4],  ItemId = 18, Item = Items[17], AcceptanceDate = new DateTime(2026, 3, 6),  CompletionDays = 11, Status = OrderStatus.Ready },
        new Order { Id = 30, ClientId = 5,  Client = Clients[4],  ItemId = 20, Item = Items[19], AcceptanceDate = new DateTime(2026, 5, 20), CompletionDays = 6,  Status = OrderStatus.InProgress },

        // Зайцева (6) — 3 заказа 
        new Order { Id = 31, ClientId = 6,  Client = Clients[5],  ItemId = 4,  Item = Items[3],  AcceptanceDate = new DateTime(2025, 4, 9),  CompletionDays = 8,  Status = OrderStatus.Completed },
        new Order { Id = 32, ClientId = 6,  Client = Clients[5],  ItemId = 12, Item = Items[11], AcceptanceDate = new DateTime(2025, 6, 24), CompletionDays = 10, Status = OrderStatus.Completed },
        new Order { Id = 33, ClientId = 6,  Client = Clients[5],  ItemId = 20, Item = Items[19], AcceptanceDate = new DateTime(2026, 1, 18), CompletionDays = 12, Status = OrderStatus.Ready },

        // Медведев (7) — 3 заказа 
        new Order { Id = 34, ClientId = 7,  Client = Clients[6],  ItemId = 5,  Item = Items[4],  AcceptanceDate = new DateTime(2025, 4, 26), CompletionDays = 4,  Status = OrderStatus.Completed },
        new Order { Id = 35, ClientId = 7,  Client = Clients[6],  ItemId = 16, Item = Items[15], AcceptanceDate = new DateTime(2025, 8, 30), CompletionDays = 6,  Status = OrderStatus.Completed },
        new Order { Id = 36, ClientId = 7,  Client = Clients[6],  ItemId = 6,  Item = Items[5],  AcceptanceDate = new DateTime(2026, 1, 27), CompletionDays = 10, Status = OrderStatus.InProgress },

        // Орлова (8) — 3 заказа 
        new Order { Id = 37, ClientId = 8,  Client = Clients[7],  ItemId = 1,  Item = Items[0],  AcceptanceDate = new DateTime(2025, 5, 29), CompletionDays = 9,  Status = OrderStatus.Completed },
        new Order { Id = 38, ClientId = 8,  Client = Clients[7],  ItemId = 15, Item = Items[14], AcceptanceDate = new DateTime(2025, 9, 20), CompletionDays = 13, Status = OrderStatus.Completed },
        new Order { Id = 39, ClientId = 8,  Client = Clients[7],  ItemId = 7,  Item = Items[6],  AcceptanceDate = new DateTime(2026, 2, 22), CompletionDays = 17, Status = OrderStatus.Ready },

        // Гусев (9) — 2 заказа 
        new Order { Id = 40, ClientId = 9,  Client = Clients[8],  ItemId = 2,  Item = Items[1],  AcceptanceDate = new DateTime(2025, 6, 18), CompletionDays = 6,  Status = OrderStatus.Completed },
        new Order { Id = 41, ClientId = 9,  Client = Clients[8],  ItemId = 8,  Item = Items[7],  AcceptanceDate = new DateTime(2025, 12, 20), CompletionDays = 11, Status = OrderStatus.InProgress },

        // Ларина (10) — 2 заказа 
        new Order { Id = 42, ClientId = 10, Client = Clients[9],  ItemId = 3,  Item = Items[2],  AcceptanceDate = new DateTime(2025, 7, 11), CompletionDays = 7,  Status = OrderStatus.Completed },
        new Order { Id = 43, ClientId = 10, Client = Clients[9],  ItemId = 9,  Item = Items[8],  AcceptanceDate = new DateTime(2026, 1, 30), CompletionDays = 9,  Status = OrderStatus.Ready },

        // Белов (11) — 2 заказа 
        new Order { Id = 44, ClientId = 11, Client = Clients[10], ItemId = 4,  Item = Items[3],  AcceptanceDate = new DateTime(2025, 8, 14), CompletionDays = 10, Status = OrderStatus.Completed },
        new Order { Id = 45, ClientId = 11, Client = Clients[10], ItemId = 10, Item = Items[9],  AcceptanceDate = new DateTime(2026, 2, 28), CompletionDays = 15, Status = OrderStatus.InProgress },

        // Панова (12) — 2 заказа 
        new Order { Id = 46, ClientId = 12, Client = Clients[11], ItemId = 11, Item = Items[10], AcceptanceDate = new DateTime(2025, 10, 22), CompletionDays = 14, Status = OrderStatus.Completed },
        new Order { Id = 47, ClientId = 12, Client = Clients[11], ItemId = 16, Item = Items[15], AcceptanceDate = new DateTime(2026, 3, 18), CompletionDays = 18, Status = OrderStatus.InProgress },

        // Фомин (13) — 1 заказ 
        new Order { Id = 48, ClientId = 13, Client = Clients[12], ItemId = 12, Item = Items[11], AcceptanceDate = new DateTime(2026, 4, 5),  CompletionDays = 16, Status = OrderStatus.InProgress },

        // Киселёва (14) — 1 заказ 
        new Order { Id = 49, ClientId = 14, Client = Clients[13], ItemId = 14, Item = Items[13], AcceptanceDate = new DateTime(2026, 5, 27), CompletionDays = 5,  Status = OrderStatus.Ready },

        // Шестаков (15) — 1 заказ 
        new Order { Id = 50, ClientId = 15, Client = Clients[14], ItemId = 20, Item = Items[19], AcceptanceDate = new DateTime(2026, 6, 30), CompletionDays = 10, Status = OrderStatus.InProgress }
    };
}