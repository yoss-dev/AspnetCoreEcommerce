using AspnetCoreEcommerce.Core.Domain.Catalog;
using AspnetCoreEcommerce.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AspnetCoreEcommerce.xUnitTest.Services.Catalog
{
    public class CategoryService_Test
    {
        [Fact]
        public void CategoryService_Test_GetAllCategories()
        {
            //arrange 
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("CategoryService_Test_GetAllCategories")
                .Options;

            var categoryEntities = new List<Category>()
            {
                new Category()
                    {Id = Guid.NewGuid(), Name = "Category 1", ParentCategoryId = Guid.Empty, SeoUrl = "Category-1"},
                new Category()
                {
                    Id = Guid.NewGuid(), Name = "Category 2", ParentCategoryId = Guid.NewGuid(), SeoUrl = "Category-2"
                },
                new Category()
                    {Id = Guid.NewGuid(), Name = "Category 3", ParentCategoryId = Guid.Empty, SeoUrl = "Category-3"}
            };

            using (var context = new ApplicationDbContext(options))
            {
                foreach (var category in categoryEntities)
                    context.Categories.Add(category);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);
                //assert
                Assert.Equal(categoryEntities.Count, service.CategoryService.GetAllCategories().Count);
            }
        }

        [Fact]
        public void CategoryService_Test_GetAllCategoriesWithoutParent()
        {
            //arrange 
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CategoryService_Test_GetAllCategoriesWithoutParent")
                .Options;

            var categoryEntities = new List<Category>()
            {
                new Category()
                {
                    Id = Guid.NewGuid(), Name = "Category 1", ParentCategoryId = Guid.NewGuid(), SeoUrl = "Category-1"
                },
                new Category()
                    {Id = Guid.NewGuid(), Name = "Category 2", ParentCategoryId = Guid.Empty, SeoUrl = "Category-2"},
                new Category()
                    {Id = Guid.NewGuid(), Name = "Category 3", ParentCategoryId = Guid.Empty, SeoUrl = "Category-3"}
            };

            using (var context = new ApplicationDbContext(options))
            {
                foreach (var category in categoryEntities)
                    context.Categories.Add(category);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);
                //assert
                Assert.Equal(2, service.CategoryService.GetAllCategoriesWithoutParent().Count);
            }
        }

        [Fact]
        public void CategoryService_Test_GetCategoryById()
        {
            //arrange 
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("CategoryService_Test_GetCategoryById")
                .Options;

            var categoryEntity = new Category()
            { Id = Guid.NewGuid(), Name = "Category-1", ParentCategoryId = Guid.Empty };

            using (var context = new ApplicationDbContext(options))
            {
                context.Categories.Add(categoryEntity);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);
                //assert
                Assert.NotNull(service.CategoryService.GetCategoryById(categoryEntity.Id));
            }
        }

        [Fact]
        public void CategoriesService_Test_GetCategoryBySeo()
        {
            //arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("CategoriesService_Test_GetCategoryBySeo")
                .Options;

            var categoryEntity = new Category()
            { Id = Guid.NewGuid(), Name = "Category 1", ParentCategoryId = Guid.Empty, SeoUrl = "Category-1" };

            using (var context = new ApplicationDbContext(options))
            {
                context.Categories.Add(categoryEntity);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);
                //assert
                Assert.NotNull(service.CategoryService.GetCategoryBySeo(categoryEntity.SeoUrl));
            }
        }

        [Fact]
        public void CategoryService_Test_InsertCategory()
        {
            //arrange 
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("CategoryService_Test_InsertCategory")
                .Options;

            var categoryEntity = new Category()
            { Id = Guid.NewGuid(), Name = "Category 1", ParentCategoryId = Guid.Empty };

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);

                //act
                service.CategoryService.InsertCategory(categoryEntity);

                //assert
                Assert.Equal(1, service.CategoryService.GetAllCategories().Count);
            }
        }

        [Fact]
        public void CategoryService_Test_UpdateCategory()
        {
            //arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("CategoryService_Test_UpdateCategory")
                .Options;

            var categoryEntity = new Category()
            { Id = Guid.NewGuid(), Name = "Category 1", ParentCategoryId = Guid.Empty };

            using (var context = new ApplicationDbContext(options))
            {
                context.Categories.Add(categoryEntity);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);

                //act
                categoryEntity.Name = "Category 1 updated";
                service.CategoryService.UpdateCategory(categoryEntity);

                //assert
                Assert.Equal("Category 1 updated", service.CategoryService.GetAllCategories().Single().Name);
            }
        }

        [Fact]
        public void CategoryService_Test_DeleteCategory()
        {
            //arrange 
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("CategoryService_Test_DeleteCategory")
                .Options;

            var categoryEntity = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Category 1",
                ParentCategoryId = Guid.Empty
            };

            using (var context = new ApplicationDbContext(options))
            {
                context.Categories.Add(categoryEntity);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);

                //act 
                service.CategoryService.DeleteCategories(new List<Guid>() { categoryEntity.Id });

                //asert
                Assert.Equal(0, service.CategoryService.GetAllCategories().Count);
            }
        }

        [Fact]
        public void CategoryService_Test_InsertProductCategoryMappings()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("CategoryService_Test_InsertProductCategoryMappings")
                .Options;

            var productEntity = new Product()
            {
                Id = Guid.NewGuid(),
                Name = "Product 1",
                Price = 100m
            };
            var categoryEntity1 = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Category 1",
                ParentCategoryId = Guid.Empty,
                SeoUrl = "Category-1"
            };
            var categoryEntity2 = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Category 2",
                ParentCategoryId = Guid.NewGuid(),
                SeoUrl = "Category-2"
            };

            var categoryMappings = new List<ProductCategoryMapping>()
            {
                new ProductCategoryMapping()
                {
                    Id = Guid.NewGuid(),
                    ProductId = productEntity.Id,
                    CategoryId = categoryEntity1.Id
                },
                new ProductCategoryMapping()
                {
                    Id = Guid.NewGuid(),
                    ProductId = productEntity.Id,
                    CategoryId = categoryEntity2.Id
                }
            };

            using (var context = new ApplicationDbContext(options))
            {
                context.Products.Add(productEntity);
                context.Categories.Add(categoryEntity1);
                context.Categories.Add(categoryEntity2);
                foreach (var mapping in categoryMappings)
                    context.ProductCategoryMappings.Add(mapping);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);
                //assert
                Assert.Equal(2, service.ProductService.GetProductById(productEntity.Id).Categories.Count);
            }

        }

        [Fact]
        public void CategoryService_Test_DeleteAllProductCategoryMappingsByProductId()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("CategoryService_Test_DeleteAllProductCategoryMappingsByProductId")
                .Options;

            var productEntity = new Product()
            {
                Id = Guid.NewGuid(),
                Name = "Product 1",
                Price = 100m
            };
            var categoryEntity1 = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Category 1",
                ParentCategoryId = Guid.Empty,
                SeoUrl = "Category-1"
            };
            var categoryEntity2 = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Category 2",
                ParentCategoryId = Guid.NewGuid(),
                SeoUrl = "Category-2"
            };
            var categoryMappings = new List<ProductCategoryMapping>()
            {
                new ProductCategoryMapping()
                {
                    Id = Guid.NewGuid(),
                    ProductId = productEntity.Id,
                    CategoryId = categoryEntity1.Id
                },
                new ProductCategoryMapping()
                {
                    Id = Guid.NewGuid(),
                    ProductId = productEntity.Id,
                    CategoryId = categoryEntity2.Id
                }
            };

            using (var context = new ApplicationDbContext(options))
            {
                context.Products.Add(productEntity);
                context.Categories.Add(categoryEntity1);
                context.Categories.Add(categoryEntity2);
                foreach (var categoryMapping in categoryMappings)
                    context.ProductCategoryMappings.Add(categoryMapping);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);

                //act 
                service.CategoryService.DeleteAllProductCategoryMappingsByProductId(productEntity.Id);

                //assert
                Assert.Equal(0, service.ProductService.GetProductById(productEntity.Id).Categories.Count);
            }
        }
    }
}