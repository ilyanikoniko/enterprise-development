using DryCleaner.Api;
using DryCleaner.Application.Services;
using DryCleaner.Contracts.Dtos;
using DryCleaner.Contracts.Interfaces;
using DryCleaner.Domain.Data;       
using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Interfaces;
using DryCleaner.Infrastructure.InMemory.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Добавляем контроллеры и Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.ParameterLocation.Header,
        Description = "Введите JWT токен (без слова 'Bearer', просто сам токен)."
    });
    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = []
    });
});

builder.Services.AddSingleton<ClientInMemoryRepository>();
builder.Services.AddSingleton<ItemInMemoryRepository>();
builder.Services.AddSingleton<OrderInMemoryRepository>();
builder.Services.AddSingleton<CategoryInMemoryRepository>();

// Теперь связываем интерфейсы с уже созданными экземплярами
builder.Services.AddSingleton<IRepository<Client, int>>(sp => sp.GetRequiredService<ClientInMemoryRepository>());
builder.Services.AddSingleton<IRepository<Item, int>>(sp => sp.GetRequiredService<ItemInMemoryRepository>());
builder.Services.AddSingleton<IRepository<Order, int>>(sp => sp.GetRequiredService<OrderInMemoryRepository>());
builder.Services.AddSingleton<IRepository<Category, int>>(sp => sp.GetRequiredService<CategoryInMemoryRepository>());

builder.Services.AddSingleton<IClientRepository>(sp => sp.GetRequiredService<ClientInMemoryRepository>());
builder.Services.AddSingleton<IItemRepository>(sp => sp.GetRequiredService<ItemInMemoryRepository>());
builder.Services.AddSingleton<IOrderRepository>(sp => sp.GetRequiredService<OrderInMemoryRepository>());
builder.Services.AddSingleton<ICategoryRepository>(sp => sp.GetRequiredService<CategoryInMemoryRepository>());

builder.Services.AddSingleton<IAnalyticsService, AnalyticsService>();

// ==========================================
// РЕГИСТРАЦИЯ CRUD-СЕРВИСОВ
// ==========================================
builder.Services.AddSingleton<IApplicationService<ClientDto, ClientEditDto, int>, ClientService>();
builder.Services.AddSingleton<IApplicationService<CategoryDto, CategoryEditDto, int>, CategoryService>();
builder.Services.AddSingleton<IApplicationService<ItemDto, ItemCreateDto, int>, ItemService>();
builder.Services.AddSingleton<IApplicationService<OrderDto, OrderCreateDto, int>, OrderService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();