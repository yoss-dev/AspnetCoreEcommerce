using AspnetCoreEcommerce.Core.Domain.Catalog;
using AspnetCoreEcommerce.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Xunit;

namespace AspnetCoreEcommerce.xUnitTest.Services.Catalog
{
    public class ManufacturerService_Test
    {
        [Fact]
        public void ManufacturerService_Test_GetManufacturers()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("ManufacturerService_Test_GetManufacturers")
                .Options;
            var manufacturerEntities = new List<Manufacturer>()
            {
                new Manufacturer() {Id = Guid.NewGuid(), Name = "Manufacturer 1"},
                new Manufacturer() {Id = Guid.NewGuid(), Name = "Manufacturer 2"},
                new Manufacturer() {Id = Guid.NewGuid(), Name = "Manufacturer 3"}
            };

            using (var context = new ApplicationDbContext(options))
            {
                foreach (var manufacturer in manufacturerEntities)
                    context.Manufacturers.Add(manufacturer);

                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);
                //assert
                Assert.Equal(
                    manufacturerEntities.Count,
                    service.ManufacturerService.GetManufacturers().Count);
            }
        }

        [Fact]
        public void ManufacturerService_Test_GetManufacturerById()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("ManufacturerService_Test_GetManufacturerById")
                .Options;

            var manufacturerEntity = new Manufacturer() { Id = Guid.NewGuid(), Name = "Manufacturer 1" };

            using (var context = new ApplicationDbContext(options))
            {
                context.Manufacturers.Add(manufacturerEntity);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);

                //assert
                Assert.Equal(manufacturerEntity.Name,
                    service.ManufacturerService.GetManufacturerById(manufacturerEntity.Id).Name);
            }
        }

        [Fact]
        public void ManufacturerService_Test_GetManufacturerBySeo()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("ManufacturerService_Test_GetManufacturerBySeo")
                .Options;

            var manufacturerEntity = new Manufacturer()
            {
                Id = Guid.NewGuid(),
                Name = "Manufacturer 1",
                SeoUrl = "Manufacturer-1"
            };

            using (var context = new ApplicationDbContext(options))
            {
                context.Manufacturers.Add(manufacturerEntity);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);

                //assert
                Assert.NotNull(service.ManufacturerService.GetManufacturerBySeo(manufacturerEntity.SeoUrl));
            }
        }

        [Fact]
        public void ManufacturerService_Test_InsertManufacturer()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("ManufacturerService_Test_InsertManufacturer")
                .Options;

            var manufacturerEntity = new Manufacturer() { Id = Guid.NewGuid(), Name = "Manufacturer 1" };

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);

                //act
                service.ManufacturerService.InsertManufacturer(manufacturerEntity);

                //assert
                Assert.Equal(1, service.ManufacturerService.GetManufacturers().Count);
            }
        }

        [Fact]
        public void ManufacturerService_Test_UpdateManufacturer()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("ManufacturerService_Test_UpdateManufacturer")
                .Options;

            var manufacturerEntity = new Manufacturer() { Id = Guid.NewGuid(), Name = "Manufacturer 1" };

            using (var context = new ApplicationDbContext(options))
            {
                context.Manufacturers.Add(manufacturerEntity);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);

                //act
                manufacturerEntity.Name = "Manufacturer 1 updated";
                service.ManufacturerService.UpdateManufacturer(manufacturerEntity);

                //assert
                Assert.Equal(
                    manufacturerEntity.Name,
                    service
                        .ManufacturerService
                        .GetManufacturerById(manufacturerEntity.Id).Name);
            }
        }

        [Fact]
        public void ManufacturerService_Test_DeleteManufacturers()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("ManufacturerService_Test_DeleteManufacturers")
                .Options;

            var manufacturerEntity = new Manufacturer() { Id = Guid.NewGuid(), Name = "Manufacturer 1" };

            using (var context = new ApplicationDbContext(options))
            {
                context.Manufacturers.Add(manufacturerEntity);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);

                //act
                service.ManufacturerService.DeleteManufacturers(new List<Guid>() { manufacturerEntity.Id });

                //assert
                Assert.Empty(service.ManufacturerService.GetManufacturers());
            }
        }

        [Fact]
        public void ManufacturerService_Test_InsertProductManufacturerMappings()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("ManufacturerService_Test_InsertProductManufacturerMappings")
                .Options;

            var productEntity = new Product() { Id = Guid.NewGuid(), Name = "Product 1", Price = 100m };
            var manufacturerEntity1 = new Manufacturer() { Id = Guid.NewGuid(), Name = "Manufacturer 1" };
            var manufacturerEntity2 = new Manufacturer() { Id = Guid.NewGuid(), Name = "Manufacturer 2" };
            var mappings = new List<ProductManufacturerMapping>()
            {
                new ProductManufacturerMapping()
                {
                    Id = Guid.NewGuid(),
                    ManufacturerId = manufacturerEntity1.Id,
                    ProductId = productEntity.Id
                },
                new ProductManufacturerMapping()
                {
                    Id = Guid.NewGuid(),
                    ManufacturerId = manufacturerEntity2.Id,
                    ProductId = productEntity.Id
                }
            };

            using (var context = new ApplicationDbContext(options))
            {
                context.Products.Add(productEntity);
                context.Manufacturers.Add(manufacturerEntity1);
                context.Manufacturers.Add(manufacturerEntity2);

                foreach (var manufacturerMapping in mappings)
                    context.ProductManufacturerMappings.Add(manufacturerMapping);

                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);

                //Assert
                Assert.Equal(
                    mappings.Count,
                    service.ProductService
                        .GetProductById(productEntity.Id)
                        .Manufacturers.Count);
            }
        }

        [Fact]
        public void ManufacturerService_Test_DeleteAllProductManufacturerMappings()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("ManufacturerService_Test_DeleteAllProductManufacturerMappings")
                .Options;

            var productEntity = new Product() { Id = Guid.NewGuid(), Name = "Product 1", Price = 100m };
            var manufacturerEntity1 = new Manufacturer() { Id = Guid.NewGuid(), Name = "Manufacturer 1" };
            var manufacturerEntity2 = new Manufacturer() { Id = Guid.NewGuid(), Name = "Manufacturer 2" };
            var mappings = new List<ProductManufacturerMapping>()
            {
                new ProductManufacturerMapping()
                {
                    Id = Guid.NewGuid(),
                    ManufacturerId = manufacturerEntity1.Id,
                    ProductId = productEntity.Id
                },
                new ProductManufacturerMapping()
                {
                    Id = Guid.NewGuid(),
                    ManufacturerId = manufacturerEntity2.Id,
                    ProductId = productEntity.Id
                }
            };

            using (var context = new ApplicationDbContext(options))
            {
                context.Products.Add(productEntity);
                context.Manufacturers.Add(manufacturerEntity1);
                context.Manufacturers.Add(manufacturerEntity2);

                foreach (var manufacturerMapping in mappings)
                    context.ProductManufacturerMappings.Add(manufacturerMapping);

                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);

                //act
                service.ManufacturerService.DeleteAllProductManufacturersMappings(productEntity.Id);

                //assert
                Assert.Empty(service.ProductService.GetProductById(productEntity.Id).Manufacturers);
            }
        }
    }
}
