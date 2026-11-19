using DryCleaner.Application.Services;
using DryCleaner.Contracts.Dtos.Categories;
using DryCleaner.Contracts.Dtos.Clients;
using DryCleaner.Contracts.Dtos.Items;
using DryCleaner.Contracts.Interfaces;
using DryCleaner.Domain.Interfaces;
using DryCleaner.Infrastructure.EFCore;
using DryCleaner.Infrastructure.EFCore.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using Scalar.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
    });
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{typeof(Program).Assembly.GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    options.IncludeXmlComments(xmlPath);

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "Введите JWT токен"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.AddMySqlDbContext<DryCleanerDbContext>("drycleanerdb");

// Регистрация репозиториев 
builder.Services.AddTransient<IClientRepository, ClientEfCoreRepository>();
builder.Services.AddTransient<IItemRepository, ItemEfCoreRepository>();
builder.Services.AddTransient<IOrderRepository, OrderEfCoreRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryEfCoreRepository>();

builder.Services.AddTransient<IAnalyticsService, AnalyticsService>();

// Регистрация CRUD сервисов
builder.Services.AddTransient<IApplicationService<ClientDto, ClientEditDto, int>, ClientService>();
builder.Services.AddTransient<IApplicationService<CategoryDto, CategoryEditDto, int>, CategoryService>();
builder.Services.AddTransient<IApplicationService<ItemDto, ItemCreateDto, int>, ItemService>();
builder.Services.AddTransient<IOrderService, OrderService>();

builder.Services.AddScoped<IPasswordHasher<string>, PasswordHasher<string>>();
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

app.UseMiddleware<DryCleaner.Api.Middleware.ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.MapScalarApiReference(options =>
    {
        options.WithOpenApiRoutePattern("/swagger/{documentName}/swagger.json");
    });

    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<DryCleanerDbContext>();

    await context.Database.MigrateAsync();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapDefaultEndpoints();
app.Run();