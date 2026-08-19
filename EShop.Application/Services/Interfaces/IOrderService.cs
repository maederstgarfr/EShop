using System;
using System.Threading.Tasks;
using EShop.Data.DTOs.PaymentDto;
using EShop.Data.DTOs.OrderDto;
using EShop.Data.Entities.OrderEntities;

namespace EShop.Application.Services.Interfaces
{
    public interface IOrderService: IAsyncDisposable
    {
        Task<OrderDetailDto> OrderDetail(long orderId);
        Task<OpenOrderDto?> UserOpenOrderDetail(long userId);
        Task<FilterOrderDto> FilterOrders(FilterOrderDto filter);
        Task<Order?> GetUserOpenOrder(long userId);
        Task<Order> GetOrderById(long OrderId);
        Task<int> UpdateOrderDetailPrices(long orderId);
        Task<int> GetOrderTotalPrice(long OrderId);
        Task<ProcessOrderDto> GetProcessOrder(long orderId);
        Task<long> AddOrderForUser(long userId);    
        Task ProcessOrder(ProcessOrderDto dto);
        Task AddProductToOrder(SubmitOrderDetailDto dto);
        Task ChangeOrderDetailCount(long orderDetailId, int count);
        Task RemoveOrderDetail(long orderDetailId);
        Task PayOrderPrice(PaymentVerificationResultData dto);
        Task DeleteOrder(long orderId);

    }
}
