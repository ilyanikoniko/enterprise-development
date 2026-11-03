using DryCleaner.Api;
using DryCleaner.Application.Services;
using DryCleaner.Contracts.Dtos.Categories;
using DryCleaner.Contracts.Dtos.Clients;
using DryCleaner.Contracts.Dtos.Items;
using DryCleaner.Contracts.Interfaces;
using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Interfaces;
using DryCleaner.Infrastructure.InMemory.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Scalar.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Настройка контроллеров
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Генерация спецификации OpenApi (встроенная от Microsoft)
builder.Services.AddOpenApi(options =>
{
    // Настройка безопасности в OpenApi (аналог кнопки Authorize)
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        // В новых версиях Components нужно инициализировать
        document.Components ??= new OpenApiComponents();

        // Добавляем схему Bearer
        document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();
        document.Components.SecuritySchemes["Bearer"] = new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Введите JWT токен"
        };

        // Применяем требование безопасности глобально (в новых версиях используется document.Security)
        document.Security ??= new List<OpenApiSecurityRequirement>();
        document.Security.Add(new OpenApiSecurityRequirement
        {
            {
                // Используем новый класс ссылки, передавая имя схемы и сам документ
                new OpenApiSecuritySchemeReference("Bearer", document),
                new List<string>()
            }
        });

        return Task.CompletedTask;
    });
});

// Регистрация репозиториев
builder.Services.AddSingleton<IClientRepository, ClientInMemoryRepository>();
builder.Services.AddSingleton<IItemRepository, ItemInMemoryRepository>();
builder.Services.AddSingleton<IOrderRepository, OrderInMemoryRepository>();
builder.Services.AddSingleton<ICategoryRepository, CategoryInMemoryRepository>();

builder.Services.AddSingleton<IAnalyticsService, AnalyticsService>();

// Регистрация CRUD сервисов
builder.Services.AddSingleton<IApplicationService<ClientDto, ClientEditDto, int>, ClientService>();
builder.Services.AddSingleton<IApplicationService<CategoryDto, CategoryEditDto, int>, CategoryService>();
builder.Services.AddSingleton<IApplicationService<ItemDto, ItemCreateDto, int>, ItemService>();
builder.Services.AddSingleton<IOrderService, OrderService>();

// Настройка аутентификации JWT
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

var app = builder.Build();

// Заполняем базу при запуске
using (var scope = app.Services.CreateScope())
{
    await DataSeeder.SeedAsync(scope.ServiceProvider);
}

app.UseMiddleware<DryCleaner.Api.Middleware.ExceptionHandlingMiddleware>();

app.MapOpenApi();
app.MapScalarApiReference();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();