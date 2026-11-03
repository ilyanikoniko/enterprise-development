using DryCleaner.Domain.Data;
using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Interfaces;

namespace DryCleaner.Api;

/// <summary>
/// Статический класс для заполнения репозиториев тестовыми данными при запуске приложения
/// </summary>
public static class DataSeeder
{
    /// <summary>
    /// Заполняет репозитории данными из DataSeed
    /// </summary>
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var clientRepo = serviceProvider.GetRequiredService<IClientRepository>();
        var itemRepo = serviceProvider.GetRequiredService<IItemRepository>();
        var orderRepo = serviceProvider.GetRequiredService<IOrderRepository>();
        var categoryRepo = serviceProvider.GetRequiredService<ICategoryRepository>();

        foreach (var c in DataSeed.Categories) await categoryRepo.Create(c);
        foreach (var c in DataSeed.Clients) await clientRepo.Create(c);
        foreach (var i in DataSeed.Items) await itemRepo.Create(i);
        foreach (var o in DataSeed.Orders) await orderRepo.Create(o);
    }
}