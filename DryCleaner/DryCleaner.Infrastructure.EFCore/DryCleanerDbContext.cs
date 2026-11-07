using DryCleaner.Domain.Data;
using DryCleaner.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DryCleaner.Infrastructure.EFCore;

/// <summary>
/// Контекст базы данных для химчистки (EF Core)
/// </summary>
public class DryCleanerDbContext(DbContextOptions<DryCleanerDbContext> options) : DbContext(options)
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Настройка сущностей и связей
        modelBuilder.Entity<Category>(builder =>
        {
            builder.HasKey(c => c.Id);
            // Начальное заполнение данными при создании таблицы
            builder.HasData(DataSeed.Categories);
        });

        modelBuilder.Entity<Client>(builder =>
        {
            builder.HasKey(c => c.Id);
            builder.HasData(DataSeed.Clients);
        });

        modelBuilder.Entity<Item>(builder =>
        {
            builder.HasKey(i => i.Id);
            // Связь: Изделие имеет одну Категорию
            builder.HasOne(i => i.Category)
                   .WithMany()
                   .HasForeignKey(i => i.CategoryId);
            builder.HasData(DataSeed.Items);
        });

        modelBuilder.Entity<Order>(builder =>
        {
            builder.HasKey(o => o.Id);
            // Связь: Заказ имеет одного Клиента
            builder.HasOne(o => o.Client)
                   .WithMany()
                   .HasForeignKey(o => o.ClientId);
            // Связь: Заказ имеет одно Изделие
            builder.HasOne(o => o.Item)
                   .WithMany()
                   .HasForeignKey(o => o.ItemId);
            builder.HasData(DataSeed.Orders);
        });
    }
}