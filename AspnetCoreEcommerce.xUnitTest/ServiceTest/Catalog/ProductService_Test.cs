using AspnetCoreEcommerce.Core.Domain.Catalog;
using AspnetCoreEcommerce.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Xunit;

namespace AspnetCoreEcommerce.xUnitTest.Services.Catalog
{
    public class ProductService_Test
    {
        [Fact]
        public void ProductService_Test_GetAllProducts()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("ProductService_Test_GetAllProducts")
                .Options;

            var productEntities = new List<Product>()
            {
                new Product() {Id = Guid.NewGuid(), Name = "Product 1", Price = 100m},
                new Product() {Id = Guid.NewGuid(), Name = "Product 2", Price = 100m},
                new Product() {Id = Guid.NewGuid(), Name = "Product 3", Price = 100m}
            };

            using (var context = new ApplicationDbContext(options))
            {
                foreach (var productEntity in productEntities)
                    context.Products.Add(productEntity);

                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);

                //assert
                Assert.Equal(productEntities.Count, service.ProductService.GetAllProducts().Count);
            }

        }

        [Fact]
        public void ProductService_Test_GetProductById()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("ProductService_Test_GetProductById")
                .Options;

            var productEntity = new Product() { Id = Guid.NewGuid(), Name = "Product 1", Price = 100m };

            using (var context = new ApplicationDbContext(options))
            {
                context.Products.Add(productEntity);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);

                //assert
                Assert.NotNull(service.ProductService.GetProductById(productEntity.Id));
            }
        }

        [Fact]
        public void ProductService_Test_GetProductBySeo()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("ProductService_Test_GetProdutBySeo")
                .Options;

            var productEntity = new Product()
            { Id = Guid.NewGuid(), Name = "Product 1", Price = 100m, SeoUrl = "Product-1" };

            using (var context = new ApplicationDbContext(options))
            {
                context.Products.Add(productEntity);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);

                //assert
                Assert.NotNull(service.ProductService.GetProductBySeo(productEntity.SeoUrl));
            }
        }

        [Fact]
        public void ProductService_Test_InsertProduct()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("ProductService_Test_InsertProduct")
                .Options;

            var productEntity = new Product()
            {
                Id = Guid.NewGuid(),
                Name = "Product 1",
                Price = 100m
            };

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);

                //act
                service.ProductService.InsertProduct(productEntity);

                //assert
                Assert.Equal(1, service.ProductService.GetAllProducts().Count);
            }
        }

        [Fact]
        public void ProductService_Test_UpdateProduct()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("ProductService_Test_UpdateProduct")
                .Options;

            var product = new Product() { Id = Guid.NewGuid(), Name = "Product 1", Price = 100m };

            using (var context = new ApplicationDbContext(options))
            {
                context.Products.Add(product);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);

                //act
                product.Name = "Product 1 Updated";
                service.ProductService.UpdateProduct(product);

                //Assert
                Assert.Equal(product.Name, service.ProductService.GetProductById(product.Id).Name);
            }
        }

        [Fact]
        public void ProductService_Test_DeleteProducts()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("ProductService_Test_DeleteProducts")
                .Options;

            var productEntity = new Product()
            {
                Id = Guid.NewGuid(),
                Name = "Product 1",
                Price = 100m
            };

            using (var context = new ApplicationDbContext(options))
            {
                context.Products.Add(productEntity);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);

                //act
                service.ProductService.DeleteProducts(new List<Guid>() { productEntity.Id });

                //assert
                Assert.Equal(0, service.ProductService.GetAllProducts().Count);
            }
        }

        //[Fact]
        public void ProductService_Test_SearchProduct()
        {

        }

        [Fact]
        public void ProductService_Test_Table()
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("ProductService_Test_Table")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);

                //Assert
                Assert.NotNull(service.ProductService.Table());
            }
        }
    }
}
