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
using Microsoft.AspNetCore.Identity;
using Scalar.AspNetCore;
using System.Text;
using DryCleaner.Api.BackgroundServices;
using DryCleaner.Contracts.Protos;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

builder.Services.AddGrpcClient<OrderGrpcService.OrderGrpcServiceClient>(
    options =>
    {
        options.Address = new Uri("https://ordergenerator");
    });

if (builder.Configuration.GetValue<bool>("OrderGeneration:Enabled"))
{
    builder.Services.AddHostedService<OrderGenerationBackgroundService>();
}

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Encoder =
            System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOpenApi(options =>
{
    // Добавление описаний CRUD-эндпоинтов
    options.AddOperationTransformer((operation, context, cancellationToken) =>
    {
        if (context.Description.ActionDescriptor is
            Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor action)
        {
            var method = action.MethodInfo;

            if (method.DeclaringType?.IsGenericType == true &&
                method.DeclaringType.GetGenericTypeDefinition() ==
                    typeof(DryCleaner.Api.Controllers.CrudControllerBase<,,>))
            {
                operation.Summary = method.Name switch
                {
                    "Create" => "Создать новую запись",
                    "Edit" => "Обновить существующую запись по id",
                    "Delete" => "Удалить запись по id",
                    "GetAll" => "Получить список всех записей",
                    "Get" => "Получить запись по id",
                    _ => operation.Summary
                };
            }
        }

        return Task.CompletedTask;
    });

    // Настройка JWT Bearer в OpenAPI
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Components ??= new Microsoft.OpenApi.OpenApiComponents();

        document.Components.SecuritySchemes =
            new Dictionary<string, Microsoft.OpenApi.IOpenApiSecurityScheme>
            {
                ["Bearer"] = new Microsoft.OpenApi.OpenApiSecurityScheme
                {
                    Type = Microsoft.OpenApi.SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = "Введите JWT токен"
                }
            };

        foreach (var path in document.Paths.Values)
        {
            if (path.Operations is null)
                continue;

            foreach (var operation in path.Operations.Values)
            {
                operation.Security ??= [];

                operation.Security.Add(
                    new Microsoft.OpenApi.OpenApiSecurityRequirement
                    {
                        [new Microsoft.OpenApi.OpenApiSecuritySchemeReference(
                            "Bearer", document)] = []
                    });
            }
        }

        return Task.CompletedTask;
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
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

var app = builder.Build();

app.UseMiddleware<DryCleaner.Api.Middleware.ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference(options =>
    {
        options.WithOpenApiRoutePattern("/openapi/{documentName}.json");
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
