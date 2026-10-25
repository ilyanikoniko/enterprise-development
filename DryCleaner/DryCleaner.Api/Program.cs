using DryCleaner.Api;
using DryCleaner.Application.Services;
using DryCleaner.Contracts.Dtos;
using DryCleaner.Contracts.Interfaces;
using DryCleaner.Domain.Data;       
using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Interfaces;
using DryCleaner.Infrastructure.InMemory.Repositories;
using Microsoft.AspNetCore.Builder; 

var builder = WebApplication.CreateBuilder(args);

// Добавляем контроллеры и Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ==========================================
// 1. РЕГИСТРАЦИЯ РЕПОЗИТОРИЕВ
// ==========================================
builder.Services.AddSingleton<IRepository<Client, int>, ClientInMemoryRepository>();
builder.Services.AddSingleton<IRepository<Item, int>, ItemInMemoryRepository>();
builder.Services.AddSingleton<IRepository<Order, int>, OrderInMemoryRepository>();
builder.Services.AddSingleton<IRepository<Category, int>, CategoryInMemoryRepository>();

// ==========================================
// 2. РЕГИСТРАЦИЯ СЕРВИСОВ
// ==========================================
builder.Services.AddSingleton<IApplicationService<ClientDto, ClientEditDto, int>, ClientService>();
builder.Services.AddSingleton<IApplicationService<CategoryDto, CategoryEditDto, int>, CategoryService>();
builder.Services.AddSingleton<IApplicationService<ItemDto, ItemCreateDto, int>, ItemService>();
builder.Services.AddSingleton<IApplicationService<OrderDto, OrderCreateDto, int>, OrderService>();

builder.Services.AddSingleton<IAnalyticsService, AnalyticsService>();

// Временный код для наполнения репозиториев при запуске
// Заполняем базу при запуске
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    await DataSeeder.SeedAsync(scope.ServiceProvider);
}


app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();