using AspnetCoreEcommerce.Core.Domain.Statistics;
using AspnetCoreEcommerce.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AspnetCoreEcommerce.xUnitTest.ServiceTest.Statistics
{
    public class VisitorCountService_Test
    {
        [Fact]
        public void VisitorCountService_Test_GetAllVisitorCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("VisitorCountService_Test_GetAllVisitorCount")
                .Options;

            var visitorCountEntities = new List<VisitorCount>
            {
                new VisitorCount() {Date = DateTime.Now.AddDays(-1).Date, ViewCount = 100},
                new VisitorCount() {Date = DateTime.Now, ViewCount = 100}
            };


            using (var context = new ApplicationDbContext(options))
            {
                foreach (var visitorCount in visitorCountEntities)
                    context.VisitorCounts.Add(visitorCount);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);

                //assert
                Assert.Equal(
                    visitorCountEntities.Count,
                    service.VisitorCountService.GetAllVisitorCount().Count);
            }


        }

        [Fact]
        public void VisitorCountService_Test_GetAllVisitorCountTake()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("VisitorCountService_Test_GetAllVisitorCountTake")
                .Options;

            var visitorCountEntities = new List<VisitorCount>
            {
                new VisitorCount() {Date = DateTime.Now.AddDays(-1).Date, ViewCount = 100},
                new VisitorCount() {Date = DateTime.Now, ViewCount = 100}
            };


            using (var context = new ApplicationDbContext(options))
            {
                foreach (var visitorCount in visitorCountEntities)
                    context.VisitorCounts.Add(visitorCount);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);

                //assert
                Assert.Equal(
                    1,
                    service.VisitorCountService.GetAllVisitorCount(1).Count);
            }


        }

        [Fact]
        public void VisitorCountService_Test_GetAllVisitorCountByDate()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("VisitorCountService_Test_GetAllVisitorCountByDate")
                .Options;
            var visitorCountEntities = new List<VisitorCount>
            {
                new VisitorCount() {Date = DateTime.Now.AddDays(-1).Date, ViewCount = 100},
                new VisitorCount() {Date = DateTime.Now.Date, ViewCount = 100}
            };


            using (var context = new ApplicationDbContext(options))
            {
                foreach (var visitorCount in visitorCountEntities)
                    context.VisitorCounts.Add(visitorCount);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);

                //assert
                Assert.NotNull(service.VisitorCountService.GetVisitorCountByDate(DateTime.Now));
            }

        }

        [Fact]
        public void VisitorCountService_Test_InsertVisitorCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("VisitorCountService_Test_InsertVisitorCount")
                .Options;

            var visitorCountEntity = new VisitorCount()
            {
                Date = DateTime.Now,
                ViewCount = 100
            };

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);

                //act
                service.VisitorCountService.InsertVisitorCount(visitorCountEntity);

                //assert
                Assert.NotNull(service.VisitorCountService.GetAllVisitorCount());
            }
        }

        [Fact]
        public void VisitorCountService_Test_UpdateVisitorCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("VisitorCountService_Test_UpdateVisitorCount")
                .Options;

            var visitorCountEntity = new VisitorCount()
            {
                Date = DateTime.Now,
                ViewCount = 100
            };

            using (var context = new ApplicationDbContext(options))
            {
                context.VisitorCounts.Add(visitorCountEntity);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);

                //act
                service.VisitorCountService.UpdateVisitorCount(visitorCountEntity);

                //asert
                Assert.Equal(101,
                    service.VisitorCountService.GetAllVisitorCount().Single().ViewCount);
            }


        }

    }
}
