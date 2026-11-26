using Bogus;
using DryCleaner.Contracts.Dtos.Orders;

namespace DryCleaner.Generator;

/// <summary>
/// Генератор случайных заказов химчистки
/// </summary>
public class OrderGenerator
{
    /// <summary>
    /// Генерирует пакет случайных заказов
    /// </summary>
    /// <param name="clientIds">Идентификаторы клиентов</param>
    /// <param name="itemIds">Идентификаторы изделий</param>
    /// <param name="count">Количество заказов</param>
    /// <returns>Список сгенерированных заказов</returns>
    public List<OrderCreateDto> Generate(
        IEnumerable<int> clientIds,
        IEnumerable<int> itemIds,
        int count)
    {
        var clients = clientIds.ToArray();
        var items = itemIds.ToArray();

        var faker = new Faker();
        var orders = new List<OrderCreateDto>();

        for (var i = 0; i < count; i++)
        {
            orders.Add(new OrderCreateDto
            {
                ClientId = faker.PickRandom(clients),
                ItemId = faker.PickRandom(items),
                CompletionDays = faker.Random.Int(1, 30),
                Status = "Accepted"
            });
        }

        return orders;
    }
}