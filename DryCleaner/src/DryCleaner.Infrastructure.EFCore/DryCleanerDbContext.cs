using DryCleaner.Domain.Data;
using DryCleaner.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DryCleaner.Infrastructure.EFCore;

/// <summary>
/// Класс описывающий контекст базы данных
/// </summary>
/// <param name="options">Настройки контекста базы данных</param>
public class DryCleanerDbContext(DbContextOptions<DryCleanerDbContext> options) : DbContext(options)
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Order> Orders { get; set; }

    /// <summary>
    /// Настраивает конфигурацию сущностей и их связей в модели базы данных, добавляет данные
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(builder =>
        {
            builder.HasKey(c => c.Id);
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
            builder.HasOne(i => i.Category)
                   .WithMany()
                   .HasForeignKey(i => i.CategoryId);

            builder.HasData(DataSeed.Items.Select(i => new
            {
                i.Id,
                i.Name,
                i.Material,
                CategoryId = i.Category!.Id
            }));
        });

        modelBuilder.Entity<Order>(builder =>
        {
            builder.HasKey(o => o.Id);
            builder.HasOne(o => o.Client)
                   .WithMany()
                   .HasForeignKey(o => o.ClientId);
            builder.HasOne(o => o.Item)
                   .WithMany()
                   .HasForeignKey(o => o.ItemId);

            builder.HasData(DataSeed.Orders.Select(o => new
            {
                o.Id,
                ClientId = o.Client!.Id,
                ItemId = o.Item!.Id,
                o.AcceptanceDate,
                o.CompletionDays,
                o.Status
            }));
        });
    }
}