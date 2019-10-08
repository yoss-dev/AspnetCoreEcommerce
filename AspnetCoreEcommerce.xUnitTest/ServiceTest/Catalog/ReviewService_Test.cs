using AspnetCoreEcommerce.Core.Domain.Catalog;
using AspnetCoreEcommerce.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Xunit;

namespace AspnetCoreEcommerce.xUnitTest.Services.Catalog
{
    public class ReviewService_Test
    {
        [Fact]
        public void ReviewService_Test_GetReviewByProductId()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("ReviewService_Test_GetReviewByProductId")
                .Options;

            var productEntity = new Product() { Id = Guid.NewGuid(), Name = "Product 1" };
            var reviewEntities = new List<Review>()
            {
                new Review() {Id = Guid.NewGuid(), ProductId = productEntity.Id},
                new Review() {Id = Guid.NewGuid(), ProductId = Guid.NewGuid()}
            };

            using (var context = new ApplicationDbContext(options))
            {
                context.Products.Add(productEntity);
                foreach (var reviewEntity in reviewEntities)
                {
                    context.Reviews.Add(reviewEntity);
                }
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);

                //assert
                Assert.Equal(
                    1,
                    service.ReviewService.GetReviewsByProductId(productEntity.Id).Count);
            }
        }

        [Fact]
        public void ReviewService_Test_InsertReview()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("ReviewService_Test_InsertReview")
                .Options;

            //arrange
            Guid testProductId = Guid.NewGuid();
            var reviewEntity = new Review() { Id = Guid.NewGuid(), ProductId = testProductId };

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);

                //act
                service.ReviewService.InsertReview(reviewEntity);

                //assert
                Assert.Equal(1, service.ReviewService.GetReviewsByProductId(testProductId).Count);
            }

        }

        [Fact]
        public void ReviewService_Test_EditReview()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("ReviewService_Test_EditReview")
                .Options;


            var testProductId = Guid.NewGuid();
            var testUserId = Guid.NewGuid();
            var reviewEntity = new Review()
            {
                Id = Guid.NewGuid(),
                ProductId = testProductId,
                UserId = testUserId,
                Rating = 5
            };

            using (var context = new ApplicationDbContext(options))
            {
                context.Reviews.Add(reviewEntity);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);

                //act
                var updatedEntity = reviewEntity;
                reviewEntity.Rating = 3;
                service.ReviewService.UpdateReview(updatedEntity);

                //assert
                Assert.Equal(
                    3,
                    service.ReviewService
                        .GetReviewByProductIdUserId(testProductId, testUserId)
                        .Rating);
            }
        }
    }
}
