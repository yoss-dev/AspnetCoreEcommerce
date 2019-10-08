using AspnetCoreEcommerce.Core.Domain.Catalog;
using AspnetCoreEcommerce.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Xunit;

namespace AspnetCoreEcommerce.xUnitTest.Services.Catalog
{
    public class ImageManagerService_Test
    {
        [Fact]
        public void ImageService_Test_GetAllImages()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("ImageService_Test_GetAllImages")
                .Options;

            var imageEntities = new List<Image>()
            {
                new Image() {Id = Guid.NewGuid(), FileName = "Image 1"},
                new Image() {Id = Guid.NewGuid(), FileName = "Image 2"},
                new Image() {Id = Guid.NewGuid(), FileName = "Image 3"}
            };

            using (var context = new ApplicationDbContext(options))
            {
                foreach (var image in imageEntities)
                {
                    context.Add(image);
                }

                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);

                //assert
                Assert.Equal(imageEntities.Count, service.ImageManagerService.GetAllImages().Count);
            }
        }

        [Fact]
        public void ImageService_Test_GetImageById()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("ImageService_Test_GetImageById")
                .Options;

            var imageEntity = new Image() { Id = Guid.NewGuid(), FileName = "Image 1" };

            using (var context = new ApplicationDbContext(options))
            {
                context.Images.Add(imageEntity);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);

                //Assert
                Assert.NotNull(service.ImageManagerService.GetImageById(imageEntity.Id));
            }
        }

        [Fact]
        public void ImageService_Test_SearchImages()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("ImageService_Test_GetSearchImages")
                .Options;

            var imageEntity = new Image() { Id = Guid.NewGuid(), FileName = "Image 1" };

            using (var context = new ApplicationDbContext(options))
            {
                context.Images.Add(imageEntity);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);
                //Assert
                Assert.NotNull(service.ImageManagerService.SearchImages("Image"));
            }
        }

        [Fact]
        public void ImageService_Test_InsertImages()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("ImageService_Test_InsertImages")
                .Options;

            var imageEntities = new List<Image>()
            {
                new Image {Id = Guid.NewGuid(), FileName = "Image 1"},
                new Image {Id = Guid.NewGuid(), FileName = "Image 2"},
                new Image {Id = Guid.NewGuid(), FileName = "Image 3"}
            };

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);

                //Act
                service.ImageManagerService.InsertImages(imageEntities);
                //Assert
                Assert.Equal(imageEntities.Count, service.ImageManagerService.GetAllImages().Count);
            }
        }

        [Fact]
        public void ImageService_Test_DeleteImages()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("ImageService_Test_DeleteImages")
                .Options;

            var imageEntity = new Image() { Id = Guid.NewGuid(), FileName = "Image 1" };

            using (var context = new ApplicationDbContext(options))
            {
                context.Images.Add(imageEntity);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);

                //act
                service.ImageManagerService.DeleteImages(new List<Guid>() { imageEntity.Id });

                //assert
                Assert.Equal(0, service.ImageManagerService.GetAllImages().Count);
            }


        }

        [Fact]
        public void ImageService_Test_InsertProductImageMappings()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("ImageService_Test_InsertProductImageMappings")
                .Options;

            var productEntity = new Product() { Id = Guid.NewGuid(), Name = "Product 1", Price = 100m };
            var imageEntity = new Image() { Id = Guid.NewGuid(), FileName = "Image 1" };
            var imageEntity2 = new Image() { Id = Guid.NewGuid(), FileName = "Image 2" };

            var imageMappings = new List<ProductImageMapping>()
            {
                new ProductImageMapping()
                {
                    Id = Guid.NewGuid(),
                    ProductId = productEntity.Id,
                    ImageId = imageEntity.Id
                },
                new ProductImageMapping()
                {
                    Id = Guid.NewGuid(),
                    ProductId = productEntity.Id,
                    ImageId = imageEntity2.Id
                },
            };

            using (var context = new ApplicationDbContext(options))
            {
                context.Products.Add(productEntity);
                context.Images.Add(imageEntity);
                context.Images.Add(imageEntity2);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);

                //act
                service.ImageManagerService.InsertProductImageMappings(imageMappings);

                //assert
                Assert.Equal(
                    imageMappings.Count,
                    service.ProductService.GetProductById(productEntity.Id).Images.Count
                    );
            }
        }

        [Fact]
        public void ImageService_Test_DeleteAllProductImageMappings()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("ImageService_Test_DeleteAllProductImageMappings")
                .Options;

            var productEntity = new Product() { Id = Guid.NewGuid(), Name = "Product 1", Price = 100m };
            var imageEntity = new Image() { Id = Guid.NewGuid(), FileName = "Image 1" };
            var imageEntity2 = new Image() { Id = Guid.NewGuid(), FileName = "Image 2" };

            var imageMappings = new List<ProductImageMapping>()
            {
                new ProductImageMapping()
                {
                    Id = Guid.NewGuid(),
                    ProductId = productEntity.Id,
                    ImageId = imageEntity.Id
                },
                new ProductImageMapping()
                {
                    Id = Guid.NewGuid(),
                    ProductId = productEntity.Id,
                    ImageId = imageEntity2.Id
                },
            };

            using (var context = new ApplicationDbContext(options))
            {
                context.Products.Add(productEntity);
                context.Images.Add(imageEntity);
                foreach (var image in imageMappings)
                    context.ProductImageMappings.Add(image);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);

                //act
                service.ImageManagerService.DeleteAllProductImageMappings(productEntity.Id);

                //assert
                Assert.Equal(
                    0,
                    service.ProductService.GetProductById(productEntity.Id).Images.Count
                    );
            }
        }
    }
}
