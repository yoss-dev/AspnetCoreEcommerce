using AspnetCoreEcommerce.Core.Domain.Statistics;
using AspnetCoreEcommerce.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Xunit;

namespace AspnetCoreEcommerce.xUnitTest.Services.Statistics
{
    public class OrderCountService_Test
    {
        [Fact]
        public void OrderCountService_Test_GetAllOrderCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("OrderCount_Service_GetAllOrderCount")
                .Options;


            var orderEntities = new List<OrderCount>
            {
                new OrderCount() {Date = DateTime.Now.AddDays(-1).Date, Count = 100},
                new OrderCount() {Date = DateTime.Now.Date, Count = 100}
            };

            using (var context = new ApplicationDbContext(options))
            {
                foreach (var orderCount in orderEntities)
                {
                    context.OrderCounts.Add(orderCount);
                }

                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);
                //assert
                Assert.Equal(
                    orderEntities.Count,
                    service.OrderCountService.GetAllOrderCount().Count);
            }
        }

        [Fact]
        public void OrderCountService_Test_GetAllOrderCountTake()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("OrderCount_Service_GetAllOrderCountTake")
                .Options;


            var orderEntities = new List<OrderCount>
            {
                new OrderCount() {Date = DateTime.Now.AddDays(-1).Date, Count = 100},
                new OrderCount() {Date = DateTime.Now.Date, Count = 100}
            };

            using (var context = new ApplicationDbContext(options))
            {
                foreach (var orderCount in orderEntities)
                {
                    context.OrderCounts.Add(orderCount);
                }

                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);
                //assert
                Assert.Equal(
                    1,
                    service.OrderCountService.GetAllOrderCount(1).Count);
            }
        }

        [Fact]
        public void OrderCountService_Test_GetOrderCountByDate()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("OrderCount_Service_GetOrderCountByDate")
                .Options;


            var orderEntity = new OrderCount()
            {
                Date = DateTime.Now.Date,
                Count = 100
            };


            using (var context = new ApplicationDbContext(options))
            {
                context.OrderCounts.Add(orderEntity);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);
                //assert
                Assert.NotNull(
                    service.OrderCountService.GetOrderCountByDate(DateTime.Now));
            }
        }

        [Fact]
        public void OrderCountService_Test_InsertOrderCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("OrderCount_Service_InsertOrderCount")
                .Options;


            var orderCountEntity = new OrderCount()
            {
                Date = DateTime.Now,
                Count = 100
            };


            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);

                //act
                service.OrderCountService.InsertOrderCount(orderCountEntity);
                Assert.NotNull(
                    service.OrderCountService.GetOrderCountByDate(DateTime.Now));
            }
        }

        [Fact]
        public void OrderCountService_Test_UpdateOrderCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("OrderCount_Service_UpdateOrderCount")
                .Options;


            var orderCountEntity = new OrderCount()
            {
                Date = DateTime.Now,
                Count = 100
            };


            using (var context = new ApplicationDbContext(options))
            {
                context.Add(orderCountEntity);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);
                service.OrderCountService.UpdateOrderCount(orderCountEntity);

                //act
                Assert.Equal(
                    101,
                    service.OrderCountService.GetOrderCountByDate(DateTime.Now).Count);
            }
        }




    }
}
