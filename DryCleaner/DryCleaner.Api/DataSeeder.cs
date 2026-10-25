using DryCleaner.Domain.Data;
using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Interfaces;

namespace DryCleaner.Api;

public static class DataSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var clientRepo = serviceProvider.GetRequiredService<IRepository<Client, int>>();
        var itemRepo = serviceProvider.GetRequiredService<IRepository<Item, int>>();
        var orderRepo = serviceProvider.GetRequiredService<IRepository<Order, int>>();
        var categoryRepo = serviceProvider.GetRequiredService<IRepository<Category, int>>();

        foreach (var c in DataSeed.Categories) await categoryRepo.Create(c);
        foreach (var c in DataSeed.Clients) await clientRepo.Create(c);
        foreach (var i in DataSeed.Items) await itemRepo.Create(i);
        foreach (var o in DataSeed.Orders) await orderRepo.Create(o);
    }
}