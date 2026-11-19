
using DryCleaner.Domain.Data;
using DryCleaner.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DryCleaner.Infrastructure.EFCore;

/// <summary>
/// Контекст базы данных химчистки
/// </summary>
/// <param name="options">Настройки контекста базы данных</param>
public class DryCleanerDbContext(DbContextOptions<DryCleanerDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Таблица клиентов химчистки
    /// </summary>
    public DbSet<Client> Clients { get; set; }

    /// <summary>
    /// Таблица категорий изделий
    /// </summary>
    public DbSet<Category> Categories { get; set; }

    /// <summary>
    /// Таблица изделий, принимаемых в химчистку
    /// </summary>
    public DbSet<Item> Items { get; set; }

    /// <summary>
    /// Таблица заказов на химчистку
    /// </summary>
    public DbSet<Order> Orders { get; set; }

    /// <summary>
    /// Пользователи системы
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Настраивает конфигурацию сущностей и их связей в модели базы данных, добавляет данные
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>(builder =>
        {
            builder.ToTable("categories");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasColumnName("id");
            builder.Property(c => c.Name)
                   .HasColumnName("name")
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(c => c.RecommendedCleaningType)
                   .HasColumnName("recommended_cleaning_type")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(c => c.CleaningPrice)
                   .HasColumnName("cleaning_price")
                   .HasPrecision(10, 2);

            builder.HasData(DataSeed.Categories);
        });

        modelBuilder.Entity<Client>(builder =>
        {
            builder.ToTable("clients");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasColumnName("id");

            builder.Property(c => c.FullName)
                   .HasColumnName("full_name")
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(c => c.PhoneNumber)
                   .HasColumnName("phone_number")
                   .HasMaxLength(20)
                   .IsRequired();

            builder.HasData(DataSeed.Clients);
        });

        modelBuilder.Entity<Item>(builder =>
        {
            builder.ToTable("items");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id).HasColumnName("id");

            builder.Property(i => i.Name)
                   .HasColumnName("name")
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(i => i.Material)
                   .HasColumnName("material")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(i => i.CategoryId)
                   .HasColumnName("category_id");

            builder.HasOne(i => i.Category)
                   .WithMany()
                   .HasForeignKey(i => i.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

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
            builder.ToTable("orders");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id).HasColumnName("id");

            builder.Property(o => o.ClientId)
                   .HasColumnName("client_id");

            builder.Property(o => o.ItemId)
                   .HasColumnName("item_id");

            builder.Property(o => o.AcceptanceDate)
                   .HasColumnName("acceptance_date");

            builder.Property(o => o.CompletionDays)
                   .HasColumnName("completion_days");

            builder.Property(o => o.Status)
                   .HasColumnName("status");

            builder.HasOne(o => o.Client)
                   .WithMany()
                   .HasForeignKey(o => o.ClientId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Item)
                   .WithMany()
                   .HasForeignKey(o => o.ItemId)
                   .OnDelete(DeleteBehavior.Restrict);

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

        modelBuilder.Entity<User>(builder =>
        {
            builder.ToTable("users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                   .HasColumnName("id");

            builder.Property(u => u.Username)
                   .HasColumnName("username")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(u => u.PasswordHash)
                   .HasColumnName("password_hash")
                   .IsRequired();

            builder.HasIndex(u => u.Username)
                   .IsUnique()
                   .HasDatabaseName("ix_users_username");
        });
    }
}
