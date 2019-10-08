using AspnetCoreEcommerce.Core.Domain.Statistics;
using AspnetCoreEcommerce.Core.Interface.Statistics;
using AspnetCoreEcommerce.Infrastructure.EFRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AspnetCoreEcommerce.Infrastructure.Services.Statistics
{
    public class OrderCountService : IOrderCountService
    {
        #region Fields

        private readonly IRepository<OrderCount> _orderCountRepository;

        #endregion

        #region Constructor

        public OrderCountService(IRepository<OrderCount> orderCountRepository)
        {
            _orderCountRepository = orderCountRepository;
        }

        #endregion

        #region Methods

        public IList<OrderCount> GetAllOrderCount()
        {
            return _orderCountRepository.GetAll().ToList();
        }

        public IList<OrderCount> GetAllOrderCount(int take)
        {
            return _orderCountRepository.GetAll().Take(take).ToList();
        }

        public OrderCount GetOrderCountByDate(DateTime date)
        {
            return _orderCountRepository
                .FindByExpression(x => x.Date == date.Date);
        }

        public void InsertOrderCount(OrderCount orderCount)
        {
            if (orderCount == null)
                throw new ArgumentNullException(nameof(orderCount));

            orderCount.Date = orderCount.Date.Date;

            _orderCountRepository.Insert(orderCount);
            _orderCountRepository.SaveChanges();
        }

        public void UpdateOrderCount(OrderCount orderCount)
        {
            if (orderCount == null)
                throw new ArgumentNullException(nameof(orderCount));

            orderCount.Date = orderCount.Date.Date;
            orderCount.Count++;

            _orderCountRepository.Update(orderCount);
            _orderCountRepository.SaveChanges();

        }

        #endregion
    }
}
