using DryCleaner.Domain.Data;
using DryCleaner.Domain.Entities;

namespace DryCleaner.Tests;

/// <summary>
/// Общие данные для тестов
/// </summary>
public class DryCleanerFixture
{
    /// <summary>
    /// Список клиентов
    /// </summary>
    public List<Client> Clients { get; }

    /// <summary>
    /// Список категорий изделий
    /// </summary>
    public List<Category> Categories { get; }

    /// <summary>
    /// Список изделий
    /// </summary>
    public List<Item> Items { get; }

    /// <summary>
    /// Список заказов
    /// </summary>
    public List<Order> Orders { get; }

    /// <summary>
    /// Конструткор
    /// </summary>
    public DryCleanerFixture()
    {
        Categories = DataSeed.Categories;
        Clients = DataSeed.Clients;
        Items = DataSeed.Items;
        Orders = DataSeed.Orders;
    }
}