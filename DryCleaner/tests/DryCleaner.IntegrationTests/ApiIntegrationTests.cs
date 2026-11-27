using Aspire.Hosting.Testing;
using DryCleaner.Contracts.Dtos.Auth;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using DryCleaner.Contracts.Protos;
using Grpc.Core;
using Grpc.Net.Client;
using MySqlConnector;

namespace DryCleaner.IntegrationTests;

/// <summary>
/// Интеграционные тесты API химчистки
/// </summary>
/// <param name="fixture">Фикстура распределённого приложения</param>
public class ApiIntegrationTests(AppHostFixture fixture)
    : IClassFixture<AppHostFixture>
{
    /// <summary>
    /// Проверяет доступность API
    /// </summary>
    [Fact]
    public async Task HealthEndpoint_ReturnsSuccess()
    {
        using var client = fixture.App.CreateHttpClient(
            "drycleanerapi", "https");

        using var response = await client.GetAsync(
            "/health",
            TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    /// <summary>
    /// Проверяет регистрацию, авторизацию и доступ к защищённому эндпоинту
    /// </summary>
    [Fact]
    public async Task Auth_RegisterAndLogin_ReturnsToken()
    {
        using var client = fixture.App.CreateHttpClient(
            "drycleanerapi", "https");

        var cancellationToken = TestContext.Current.CancellationToken;

        var username = $"test_{Guid.NewGuid():N}";
        var password = "TestPassword123!";

        // Регистрация
        using var registerResponse = await client.PostAsJsonAsync(
            "/api/auth/register",
            new RegisterRequest(username, password),
            cancellationToken);

        Assert.Equal(
            HttpStatusCode.Created,
            registerResponse.StatusCode);

        // Авторизация
        using var loginResponse = await client.PostAsJsonAsync(
            "/api/auth/login",
            new LoginRequest(username, password),
            cancellationToken);

        Assert.Equal(
            HttpStatusCode.OK,
            loginResponse.StatusCode);

        var body = await loginResponse.Content
            .ReadFromJsonAsync<JsonElement>(cancellationToken);

        var token = body.GetProperty("token").GetString();

        Assert.False(string.IsNullOrWhiteSpace(token));

        // Проверка доступа с полученным токеном
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        using var ordersResponse = await client.GetAsync(
            "/api/orders",
            cancellationToken);

        Assert.Equal(
            HttpStatusCode.OK,
            ordersResponse.StatusCode);
    }

    /// <summary>
    /// Проверяет потоковую передачу сгенерированных заказов
    /// </summary>
    [Fact]
    public async Task OrderGetStream_ReturnsValidBatches()
    {
        using var httpClient = fixture.App.CreateHttpClient(
            "ordergenerator", "https");

        using var channel = GrpcChannel.ForAddress(
            httpClient.BaseAddress!,
            new GrpcChannelOptions
            {
                HttpClient = httpClient
            });

        var grpcClient =
            new OrderGrpcService.OrderGrpcServiceClient(channel);

        var cancellationToken = TestContext.Current.CancellationToken;

        var request = new OrderGetStreamRequest();

        request.ClientIds.AddRange(new[] { 1, 2, 3 });
        request.ItemIds.AddRange(new[] { 1, 2, 3 });

        var batches = new List<OrderGetStreamResponse>();

        using var call = grpcClient.OrderGetStream(
            request,
            deadline: DateTime.UtcNow.AddSeconds(30),
            cancellationToken: cancellationToken);

        await foreach (var response in
            call.ResponseStream.ReadAllAsync(cancellationToken))
        {
            batches.Add(response);
        }

        Assert.Equal(3, batches.Count);
        Assert.Equal(15, batches.Sum(b => b.Orders.Count));

        Assert.All(batches, batch =>
        {
            Assert.Equal(5, batch.Orders.Count);

            Assert.All(batch.Orders, order =>
            {
                Assert.Contains(order.ClientId, request.ClientIds);
                Assert.Contains(order.ItemId, request.ItemIds);
                Assert.InRange(order.CompletionDays, 1, 30);
                Assert.Equal("Accepted", order.Status);
            });
        });

        Assert.False(batches[0].IsFinal);
        Assert.False(batches[1].IsFinal);
        Assert.True(batches[2].IsFinal);
    }

    /// <summary>
    /// Проверяет отклонение некорректного запроса генерации
    /// </summary>
    [Fact]
    public async Task OrderGetStream_EmptyRequest_ReturnsInvalidArgument()
    {
        using var httpClient = fixture.App.CreateHttpClient(
            "ordergenerator", "https");

        using var channel = GrpcChannel.ForAddress(
            httpClient.BaseAddress!,
            new GrpcChannelOptions
            {
                HttpClient = httpClient
            });

        var grpcClient =
            new OrderGrpcService.OrderGrpcServiceClient(channel);

        var cancellationToken = TestContext.Current.CancellationToken;

        var request = new OrderGetStreamRequest();

        using var call = grpcClient.OrderGetStream(
            request,
            deadline: DateTime.UtcNow.AddSeconds(30),
            cancellationToken: cancellationToken);

        var exception = await Assert.ThrowsAsync<RpcException>(
            async () =>
            {
                await call.ResponseStream.MoveNext(cancellationToken);
            });

        Assert.Equal(
            StatusCode.InvalidArgument,
            exception.StatusCode);
    }

    /// <summary>
    /// Проверяет сохранение сгенерированных заказов в MySQL
    /// </summary>
    [Fact]
    public async Task OrderGeneration_SavesOrdersToDatabase()
    {
        const int initialOrders = 50;
        const int generatedOrders = 15;
        const int expectedOrders = initialOrders + generatedOrders;

        var cancellationToken = TestContext.Current.CancellationToken;

        var connectionString = await fixture.App.GetConnectionStringAsync(
            "drycleanerdb", cancellationToken);

        Assert.False(string.IsNullOrWhiteSpace(connectionString));

        await using var connection = new MySqlConnection(connectionString);

        await connection.OpenAsync(cancellationToken);

        await using var countCommand = new MySqlCommand(
        "SELECT COUNT(*) FROM orders", connection);

        var deadline = DateTime.UtcNow.AddSeconds(30);

        var totalOrders = Convert.ToInt64(
            await countCommand.ExecuteScalarAsync(cancellationToken));

        while (totalOrders < expectedOrders && DateTime.UtcNow < deadline)
        {
            await Task.Delay(1000, cancellationToken);

            totalOrders = Convert.ToInt64(
                await countCommand.ExecuteScalarAsync(cancellationToken));
        }

        Assert.Equal(expectedOrders, totalOrders);

        await using var validationCommand = new MySqlCommand(
            """
            SELECT COUNT(*)
            FROM orders
            WHERE id > 50
              AND status = 0
              AND completion_days BETWEEN 1 AND 30
            """,
            connection);

        var validOrders = Convert.ToInt64(
            await validationCommand.ExecuteScalarAsync(cancellationToken));

        Assert.Equal(generatedOrders, validOrders);
    }
}