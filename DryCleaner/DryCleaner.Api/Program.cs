using DryCleaner.Api;
using DryCleaner.Application.Services;
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
using DryCleaner.Contracts.Dtos.Categories;
using DryCleaner.Contracts.Dtos.Clients;
using DryCleaner.Contracts.Dtos.Items;

var builder = WebApplication.CreateBuilder(args);

// Настройка swagger и контроллеров
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Подключение XML-комментариев для Swagger
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);

    var contractsXml = Path.Combine(AppContext.BaseDirectory, "DryCleaner.Contracts.xml");
    if (File.Exists(contractsXml))
    {
        options.IncludeXmlComments(contractsXml);
    }

    // Настройка кнопки Authorize для JWT токена в swagger
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.ParameterLocation.Header,
        Description = "Введите JWT токен"
    });
    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = []
    });
});

// Регистрация репозиторией
builder.Services.AddSingleton<ClientInMemoryRepository>();
builder.Services.AddSingleton<ItemInMemoryRepository>();
builder.Services.AddSingleton<OrderInMemoryRepository>();
builder.Services.AddSingleton<CategoryInMemoryRepository>();

builder.Services.AddSingleton<IRepository<Client, int>>(sp => sp.GetRequiredService<ClientInMemoryRepository>());
builder.Services.AddSingleton<IRepository<Item, int>>(sp => sp.GetRequiredService<ItemInMemoryRepository>());
builder.Services.AddSingleton<IRepository<Order, int>>(sp => sp.GetRequiredService<OrderInMemoryRepository>());
builder.Services.AddSingleton<IRepository<Category, int>>(sp => sp.GetRequiredService<CategoryInMemoryRepository>());

builder.Services.AddSingleton<IClientRepository>(sp => sp.GetRequiredService<ClientInMemoryRepository>());
builder.Services.AddSingleton<IItemRepository>(sp => sp.GetRequiredService<ItemInMemoryRepository>());
builder.Services.AddSingleton<IOrderRepository>(sp => sp.GetRequiredService<OrderInMemoryRepository>());
builder.Services.AddSingleton<ICategoryRepository>(sp => sp.GetRequiredService<CategoryInMemoryRepository>());

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

// Заполняем базу при запуске
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    await DataSeeder.SeedAsync(scope.ServiceProvider);
}

// конвейер запросов
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();