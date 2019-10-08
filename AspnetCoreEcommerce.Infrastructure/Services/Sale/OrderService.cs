using AspnetCoreEcommerce.Core.Domain.Sale;
using AspnetCoreEcommerce.Core.Domain.Statistics;
using AspnetCoreEcommerce.Core.Interface.Sale;
using AspnetCoreEcommerce.Core.Interface.Statistics;
using AspnetCoreEcommerce.Infrastructure.EFRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AspnetCoreEcommerce.Infrastructure.Services.Sale
{
    public class OrderService : IOrderService
    {
        #region Fields

        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderItem> _orderItemRepository;
        private readonly IOrderCountService _orderCountService;
        private readonly ApplicationDbContext _context;

        #endregion

        #region Constructor

        public OrderService(
            IRepository<Order> orderRepository,
            IRepository<OrderItem> orderItemRepository,
            IOrderCountService orderCountService, ApplicationDbContext context)
        {
            _orderItemRepository = orderItemRepository;
            _orderRepository = orderRepository;
            _orderCountService = orderCountService;
            _context = context;
        }

        #endregion

        #region Methods

        public IList<Order> GetAllOrders()
        {
            // TODO: update when lazy loading is available
            var entities = _context.Orders
                .Include(x => x.Items)
                .AsNoTracking()
                .ToList();

            return entities;
        }

        public Order GetOrderById(Guid id)
        {
            return _context.Orders
                .Include(x => x.Items)
                .AsNoTracking()
                .SingleOrDefault(x => x.Id == id);
        }

        public Order GetOrderByOrderId(string orderId)
        {
            return _context.Orders
                .Include(x => x.Items)
                .AsNoTracking()
                .SingleOrDefault(x => x.OrderNumber == orderId);
        }

        public IList<Order> GetAllOrdersByUserId(Guid userId)
        {
            var entities = _context.Orders
                .Include(x => x.Items)
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .ToList();

            return entities;
        }

        public void InsertOrder(Order order)
        {
            if (order == null)
                throw new ArgumentNullException("order");

            _orderRepository.Insert(order);
            _orderItemRepository.SaveChanges();

            //add or update order count
            var orderCountEntity = _orderCountService.GetOrderCountByDate(DateTime.Now);
            if (orderCountEntity != null)
                _orderCountService.UpdateOrderCount(orderCountEntity);
            else
            {
                var orderCountModel = new OrderCount
                {
                    Date = DateTime.Now,
                    Count = 1
                };

                _orderCountService.InsertOrderCount(orderCountModel);
            }
        }

        public void UpdateOrder(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            _orderRepository.Update(order);
            _orderRepository.SaveChanges();
        }

        public void DeleteOrders(IList<Guid> ordersIds)
        {
            if (ordersIds == null)
                throw new ArgumentNullException(nameof(ordersIds));

            foreach (var id in ordersIds)
                _orderRepository.Delete(GetOrderById(id));

            _orderRepository.SaveChanges();
        }

        #endregion
    }
}
