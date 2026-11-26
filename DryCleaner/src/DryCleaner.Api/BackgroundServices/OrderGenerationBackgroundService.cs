using DryCleaner.Contracts.Dtos.Clients;
using DryCleaner.Contracts.Dtos.Items;
using DryCleaner.Contracts.Dtos.Orders;
using DryCleaner.Contracts.Interfaces;
using DryCleaner.Contracts.Protos;
using Grpc.Core;

namespace DryCleaner.Api.BackgroundServices;

/// <summary>
/// Фоновая служба получения и сохранения сгенерированных заказов
/// </summary>
/// <param name="scopeFactory">Фабрика областей зависимостей</param>
/// <param name="logger">Журнал событий службы</param>
public class OrderGenerationBackgroundService(
    IServiceScopeFactory scopeFactory,
    ILogger<OrderGenerationBackgroundService> logger)
    : BackgroundService
{
    /// <inheritdoc />
    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        try
        {
            await using var scope = scopeFactory.CreateAsyncScope();

            var clientService = scope.ServiceProvider
                .GetRequiredService<
                    IApplicationService<ClientDto, ClientEditDto, int>>();

            var itemService = scope.ServiceProvider
                .GetRequiredService<
                    IApplicationService<ItemDto, ItemCreateDto, int>>();

            var orderService = scope.ServiceProvider
                .GetRequiredService<IOrderService>();

            var grpcClient = scope.ServiceProvider
                .GetRequiredService<OrderGrpcService.OrderGrpcServiceClient>();

            var clients = await clientService.GetAll();
            var items = await itemService.GetAll();

            if (clients.Count == 0 || items.Count == 0)
            {
                logger.LogWarning(
                    "Генерация невозможна: отсутствуют клиенты или изделия");

                return;
            }

            var request = new OrderGetStreamRequest();

            request.ClientIds.AddRange(clients.Select(c => c.Id));
            request.ItemIds.AddRange(items.Select(i => i.Id));

            logger.LogInformation("Отправлен запрос на генерацию заказов");

            using var call = grpcClient.OrderGetStream(
                request,
                cancellationToken: stoppingToken);

            var totalOrders = 0;
            var isFinalReceived = false;

            await foreach (var response in
                call.ResponseStream.ReadAllAsync(stoppingToken))
            {
                foreach (var order in response.Orders)
                {
                    stoppingToken.ThrowIfCancellationRequested();

                    var dto = new OrderCreateDto
                    {
                        ClientId = order.ClientId,
                        ItemId = order.ItemId,
                        CompletionDays = order.CompletionDays,
                        Status = order.Status
                    };

                    await orderService.Create(dto);

                    totalOrders++;
                }

                logger.LogInformation(
                    "Сохранён пакет из {Count} заказов",
                    response.Orders.Count);

                if (response.IsFinal)
                {
                    isFinalReceived = true;
                }
            }

            if (isFinalReceived)
            {
                logger.LogInformation(
                    "Генерация завершена. Всего сохранено заказов: {Count}",
                    totalOrders);
            }
            else
            {
                logger.LogWarning(
                    "Поток завершился без финального пакета");
            }
        }
        catch (OperationCanceledException)
            when (stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Получение заказов отменено");
        }
        catch (RpcException exception)
            when (exception.StatusCode == StatusCode.Cancelled &&
                  stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Получение заказов отменено");
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Ошибка при получении или сохранении заказов");
        }
    }
}