using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Data.DTOs.PaymentDto;
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
        private readonly IGenericRepository<ProductVariant> _variantRepository;
        private readonly IGenericRepository<PaymentRecord> _recordRepository;


        public OrderService(IGenericRepository<User> userRepository, IGenericRepository<Order> orderRepository, IGenericRepository<OrderDetail> orderDetailRepository, IGenericRepository<Product> productRepository, IGenericRepository<ProductVariant> variantRepository, IGenericRepository<PaymentRecord> recordRepository)
        {
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _productRepository = productRepository;
            _variantRepository = variantRepository;
            _recordRepository = recordRepository;
            
        }

        public async ValueTask DisposeAsync()
        {
            await _userRepository.DisposeAsync();
            await _orderDetailRepository.DisposeAsync();
            await _orderRepository.DisposeAsync();
            await _productRepository.DisposeAsync();
            await _variantRepository.DisposeAsync();
            await _recordRepository.DisposeAsync();
         
        }
        #endregion

        public async Task<long> AddOrderForUser(long userId)
        {
            var order = await _orderRepository.GetQuery().FirstOrDefaultAsync(o => o.UserId == userId && o.OrderState == OrderState.Submitted);
            if (order != null) return order.Id;    
            
            var newOrder = new Order
            {
                UserId=userId,
                OrderState=OrderState.Submitted,
                TotalPrice=0
            };
            await _orderRepository.AddEntity(newOrder);
            await _orderRepository.SaveAsync();
            return newOrder.Id;
        }

        public async Task AddProductToOrder(SubmitOrderDetailDto dto)
        {
            var orderId = await AddOrderForUser(dto.UserId);

            if (await _orderDetailRepository.GetQuery()
                .AnyAsync(d => d.ProductVariantId == dto.ProductVariantId && d.OrderId == orderId)){
                var detail = await _orderDetailRepository.GetQuery()
                    .FirstAsync(d => d.ProductVariantId == dto.ProductVariantId && d.OrderId == orderId);
                detail.Count = dto.Count;
                _orderDetailRepository.EditEntity(detail);
                await _orderDetailRepository.SaveAsync();
            }
             
            var variant = await _variantRepository.GetEntityById(dto.ProductVariantId);
            var product = await _productRepository.GetEntityById(variant.ProductId);
            var orderDetail = new OrderDetail
            {
                Count = dto.Count,
                OrderId = orderId,
                Price = variant.Price + product.BasePrice,
                ProductVariantId = dto.ProductVariantId
            };
            await _orderDetailRepository.AddEntity(orderDetail);
            await _orderRepository.SaveAsync();



        }

        public async Task ChangeOrderDetailCount(long orderDetailId, int count)
        {
            var orderDetail = await _orderDetailRepository.GetEntityById(orderDetailId);
            if (count == 0)
            {
                await _orderDetailRepository.DeletePermanent(orderDetail);
            }
            else
            {
                orderDetail.Count = count;
                _orderDetailRepository.EditEntity(orderDetail);
                await _orderDetailRepository.SaveAsync();
            }         
        }

        public async Task DeleteOrder(long orderId)
        {
            var data = await _orderRepository.GetEntityById(orderId);
            _orderRepository.DeleteEntity(data);
            await _orderRepository.SaveAsync();
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
            if (filter.PaymentRecordId is > 0)
            {
                query = query.Where(o => o.PaymentRecordId==filter.PaymentRecordId);
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

        public async Task<ProcessOrderDto> GetProcessOrder(long orderId)
        {
            var data = await _orderRepository.GetEntityById(orderId);
            return new ProcessOrderDto
            {
                OrderState = data.OrderState,
                TraceCode = data.TraceCode,
                OrderId = data.Id
            };
        }

        public async Task<Order?> GetUserOpenOrder(long userId)
        {
            return await _orderRepository.GetQuery().FirstOrDefaultAsync(o => o.UserId == userId && o.OrderState == OrderState.Submitted);
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
                OrderState = data.OrderState,
                User = await _userRepository.GetEntityById(data.UserId),
                OrderDetails = await _orderDetailRepository.GetQuery().Include(d=>d.ProductVariant).ThenInclude(v=>v.Product).Where(d => d.OrderId == orderId).ToListAsync(),
                paymentRecord=await _recordRepository.GetQuery().FirstAsync(r=>long.Parse(r.invoice_id) ==orderId)
            };
        }

        public async Task ProcessOrder(ProcessOrderDto dto)
        {
            var data = await _orderRepository.GetEntityById(dto.OrderId);
            data.TraceCode = dto.TraceCode;
            data.OrderState = dto.OrderState;

            _orderRepository.EditEntity(data);
            await _orderRepository.SaveAsync();
        }

        public async Task RemoveOrderDetail(long orderDetailId)
        {
            var data = await _orderDetailRepository.GetEntityById(orderDetailId);
            await _orderDetailRepository.DeletePermanent(data);
            await _orderDetailRepository.SaveAsync();
        }

        public async Task PayOrderPrice(PaymentVerificationResultData dto)
        {

            #region PaymentRecord
            var record = new PaymentRecord
            {
                amount = dto.amount,
                trans_id=dto.trans_id,
                ref_id=dto.ref_id,
                payment_time=dto.payment_time,
                invoice_id=dto.invoice_id,
                card_pan=dto.card_pan,
                buyer_ip=dto.buyer_ip,
                authority=dto.authority
            };
            await _recordRepository.AddEntity(record);
            await _recordRepository.SaveAsync();
            #endregion
            var order = await _orderRepository.GetQuery().Include(o => o.OrderDetails)
               .ThenInclude(d => d.ProductVariant).FirstAsync(o => o.Id == long.Parse(dto.invoice_id));

            #region Update Order
            var user = await _userRepository.GetEntityById(order.UserId);
            order.Address = user.Address;
            order.PostCode = user.PostCode;
            order.DestinationCity = user.UserCity;
            order.UserName = user.FullName;
            order.OrderState = OrderState.Paid;
            //order.PaymentDate = dateTime;
            order.PaymentRecordId = record.Id;
            //order.BankTraceCode = dto.ref_id;

            var totalPrice = order.OrderDetails.Select(item => (item.ProductVariant.Product.BasePrice + item.ProductVariant.Price) * item.Count)
                .Aggregate(0, (current, price) => current + price);
            order.TotalPrice = totalPrice;

            _orderRepository.EditEntity(order);
            await _orderRepository.SaveAsync();
            #endregion
        }

       

        public async Task<Order> GetOrderById(long OrderId)
        {
            return await _orderRepository.GetEntityById(OrderId);
        }

        public async Task<int> GetOrderTotalPrice(long OrderId)
        {
            var detail = await _orderDetailRepository.GetQuery().Where(d => d.OrderId == OrderId).ToListAsync();
            return detail.Sum(d => d.Price * d.Count);
        }

        public async Task<OpenOrderDto> UserOpenOrderDetail(long userId)
        {
            var order = await _orderRepository.GetQuery().FirstOrDefaultAsync(d => d.UserId == userId && d.OrderState == OrderState.Submitted);
            if (order == null) return null;
            return new OpenOrderDto
            {
                User = await _userRepository.GetEntityById(userId),
                Order = order,
                OrderDetails = await _orderDetailRepository.GetQuery()
                .Include(d => d.ProductVariant)
                .ThenInclude(v => v.Product).Where(d => d.OrderId == order.Id).ToListAsync()

            };
        }

        public async Task<int> UpdateOrderDetailPrices(long orderId)
        {
            var order = await _orderRepository.GetQuery()
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.ProductVariant)
                .FirstAsync(o => o.Id == orderId);

            foreach (var item in order.OrderDetails)
            {
                var product = await _productRepository.GetEntityById(item.ProductVariant.ProductId);
                var variant = await _variantRepository.GetEntityById(item.ProductVariantId);

                item.TotalPrice = (product.BasePrice + item.ProductVariant.Price) * item.Count;
                item.ProductPrice = product.BasePrice;
                item.VariantPrice = variant.Price;
                _orderDetailRepository.EditEntity(item);
            }

            await _orderDetailRepository.SaveAsync();

            return order.OrderDetails.Select(item => (item.ProductVariant.Product.BasePrice + item.ProductVariant.Price) * item.Count).Aggregate(0, (current, price) => current + price);

        }
    }    
}
