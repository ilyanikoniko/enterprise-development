using DryCleaner.Application.Mappers;
using DryCleaner.Contracts.Dtos;
using DryCleaner.Contracts.Interfaces;
using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Interfaces;

namespace DryCleaner.Application.Services;

public class OrderService(IRepository<Order, int> repository)
    : IApplicationService<OrderDto, OrderCreateDto, int> 
{
    public async Task<OrderDto> Create(OrderCreateDto dto)
    {
        var order = Mapper.Map(dto);
        var result = await repository.Create(order);
        return Mapper.Map(result);
    }

    public async Task<OrderDto?> Get(int id)
    {
        var order = await repository.Read(id);
        return order == null ? null : Mapper.Map(order);
    }

    public async Task<IList<OrderDto>> GetAll()
    {
        var orders = await repository.ReadAll();
        return Mapper.Map(orders);
    }

    public async Task<OrderDto> Update(OrderCreateDto dto, int id)
    {
        var updatedOrder = Mapper.Map(dto);
        updatedOrder.Id = id;
        var result = await repository.Update(updatedOrder);
        return Mapper.Map(result);
    }

    public async Task<bool> Delete(int id) => await repository.Delete(id);
}