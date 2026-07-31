using System;
using System.Threading.Tasks;
using EShop.Data.DTOs.OrderDto;
using EShop.Data.Entities.OrderEntities;

namespace EShop.Application.Services.Interfaces
{
    public interface IOrderService: IAsyncDisposable
    {
        Task<OrderDetailDto> OrderDetail(long orderId);
        Task<FilterOrderDto> FilterOrders(FilterOrderDto filter);
        Task<Order> GetUserOpenOrder(long userId);
        Task<ProcessOrderDto> GetProcessOrder(long orderId);
        Task ProcessOrder(ProcessOrderDto dto);
        Task AddProductToOrder(SubmitOrderDto dto);
        Task PayOrderPrice(long invoiceId);
        Task DeleteOrder(long orderId);

    }
}
