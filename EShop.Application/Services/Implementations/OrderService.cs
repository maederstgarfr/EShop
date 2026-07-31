using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Application.Services.Interfaces;
using EShop.Data.DTOs.OrderDto;
using EShop.Data.DTOs.Paging;
using EShop.Data.Entities.Account;
using EShop.Data.Entities.OrderEntities;
using EShop.Data.Entities.ProductEntities;
using EShop.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace EShop.Application.Services.Implementations
{
    public class OrderService : IOrderService
    {
        #region CTOR
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<OrderDetail> _orderDetailRepository;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductVariant> _variantrRepository;
        public OrderService(IGenericRepository<User> userRepository, IGenericRepository<Order> orderRepository, IGenericRepository<OrderDetail> orderDetailRepository, IGenericRepository<Product> productRepository, IGenericRepository<ProductVariant> variantrRepository)
        {
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _productRepository = productRepository;
            _variantrRepository = variantrRepository;

        }
        public async ValueTask DisposeAsync()
        {
            await _userRepository.DisposeAsync();
            await _orderRepository.DisposeAsync();
            await _orderDetailRepository.DisposeAsync();
            await _productRepository.DisposeAsync();
            await _variantrRepository.DisposeAsync();
        }
        #endregion
        public Task<long> AddOrderForUser(long userId)
        {
            throw new NotImplementedException();
        }

        public Task AddProductToOrder(SubmitOrderDto dto)
        {
            throw new NotImplementedException();
        }

        public Task ChangeOrderDetailCount(long orderDetailId, int count)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOrder(long orderId)
        {
            throw new NotImplementedException();
        }



        public async Task<FilterOrderDto> FilterOrders(FilterOrderDto filter)
        {
            #region Query
            var query = _orderRepository.GetQuery().Include(o => o.OrderDetails)
                .OrderByDescending(d => d.CreateDate)
                .AsQueryable();
            #endregion

            #region Switch
            switch (filter.FilterOrderState)
            {
                case FilterOrderState.All:
                    break;
                case FilterOrderState.Submitted:
                    query = query.Where(d => d.OrderState == OrderState.Submitted);
                    break;
                case FilterOrderState.Paid:
                    query = query.Where(d => d.OrderState == OrderState.Paid);
                    break;
                case FilterOrderState.Send:
                    query = query.Where(d => d.OrderState == OrderState.Send);
                    break;
                case FilterOrderState.Canceled:
                    query = query.Where(d => d.OrderState == OrderState.Canceled);
                    break;
                default:
                    throw new NotImplementedException();
            }

            #endregion

            #region Filters
            #region String
            if (!string.IsNullOrEmpty(filter.UserName))
            {
                query = query.Where(p => EF.Functions.Like(p.UserName, $"{filter.UserName }"));
            }
            if (!string.IsNullOrEmpty(filter.Description))
            {
                query = query.Where(p => EF.Functions.Like(p.Description, $"{filter.Description }"));
            }
            if (!string.IsNullOrEmpty(filter.TraceCode))
            {
                query = query.Where(p => EF.Functions.Like(p.TraceCode, $"{filter.TraceCode }"));
            }
            if (!string.IsNullOrEmpty(filter.PaymentNumber))
            {
                query = query.Where(p => EF.Functions.Like(p.PaymentNumber, $"{filter.PaymentNumber }"));
            }
            if (!string.IsNullOrEmpty(filter.DestinationCity))
            {
                query = query.Where(p => EF.Functions.Like(p.DestinationCity, $"{filter.DestinationCity }"));
            }
            #endregion

            #region price
            if (filter.MinimumPrice is > 0)
            {
                query = query.Where(d => d.TotalPrice > filter.MinimumPrice);
            }
            #endregion

            #region Spesificatio Ids
            if (filter.UserId is > 0)
            {
                query = query.Where(d => d.UserId == filter.UserId);
            }
            #endregion




            #endregion

            #region Paging
            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntitiy, filter.HowManyShowPageAfterAndBefore);
            var allEntities = await query.Paging(pager).ToListAsync();
            #endregion
            return filter.SetData(allEntities).SetPaging(pager);
        }

        public Task<ProcessOrderDto> GetProcessOrder(long orderId)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetUserOpenOrder(long userId)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderDetailDto> OrderDetail(long orderId)
        {
            var data = await _orderRepository.GetEntityById(orderId);
            return new OrderDetailDto
            {
                Id = data.Id,
                CreateDate = data.CreateDate,
                LastUpdateDate = data.LastUpdateDate,
                UserId = data.UserId,
                UserName = data.UserName,
                Address = data.Address,
                PostCode = data.PostCode,
                TotalPrice = data.TotalPrice,
                Description = data.Description,
                TraceCode = data.PostCode,
                PaymentNumber = data.PaymentNumber,
                OrderState = data.OrderState,
                User = await _userRepository.GetEntityById(data.UserId),
                OrderDetails = await _orderDetailRepository.GetQuery().Where(d => d.OrderId == orderId).ToListAsync()
            };
        }

        public Task PayOrderPrice(long invoiceId)
        {
            throw new NotImplementedException();
        }

        public Task ProcessOrder(ProcessOrderDto dto)
        {
            throw new NotImplementedException();
        }

        public Task RemoveOrderDetail(long orderDetailId)
        {
            throw new NotImplementedException();
        }
    }
}
