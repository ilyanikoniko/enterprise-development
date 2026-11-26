using DryCleaner.Contracts.Protos;
using Grpc.Core;

namespace DryCleaner.Generator.Grpc.Host.Services;

/// <summary>
/// Служба потоковой передачи сгенерированных заказов
/// </summary>
public class OrderStreamingService(
    OrderGenerator generator,
    ILogger<OrderStreamingService> logger)
    : OrderGrpcService.OrderGrpcServiceBase
{
    private const int BatchCount = 3;
    private const int BatchSize = 5;
    private const int DelayMilliseconds = 1000;

    /// <inheritdoc />
    public override async Task OrderGetStream(
        OrderGetStreamRequest request,
        IServerStreamWriter<OrderGetStreamResponse> responseStream,
        ServerCallContext context)
    {
        if (request.ClientIds.Count == 0 ||
            request.ItemIds.Count == 0 ||
            request.ClientIds.Any(id => id <= 0) ||
            request.ItemIds.Any(id => id <= 0))
        {
            throw new RpcException(new Status(
                StatusCode.InvalidArgument,
                "Необходимо передать корректные идентификаторы клиентов и изделий"));
        }

        logger.LogInformation("Начата потоковая передача заказов");

        for (var i = 0; i < BatchCount; i++)
        {
            context.CancellationToken.ThrowIfCancellationRequested();

            var orders = generator.Generate(
                request.ClientIds,
                request.ItemIds,
                BatchSize);

            var response = new OrderGetStreamResponse
            {
                IsFinal = i == BatchCount - 1
            };

            foreach (var order in orders)
            {
                response.Orders.Add(new OrderResponse
                {
                    ClientId = order.ClientId,
                    ItemId = order.ItemId,
                    CompletionDays = order.CompletionDays,
                    Status = order.Status
                });
            }

            await responseStream.WriteAsync(response);

            logger.LogInformation(
                "Отправлен пакет {BatchNumber} из {BatchCount}",
                i + 1,
                BatchCount);

            if (!response.IsFinal)
            {
                await Task.Delay(
                    DelayMilliseconds,
                    context.CancellationToken);
            }
        }

        logger.LogInformation("Потоковая передача заказов завершена");
    }
}