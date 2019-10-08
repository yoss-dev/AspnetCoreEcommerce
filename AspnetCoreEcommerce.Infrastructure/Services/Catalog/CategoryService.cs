using AspnetCoreEcommerce.Core.Domain.Catalog;
using AspnetCoreEcommerce.Core.Interface.Catalog;
using AspnetCoreEcommerce.Infrastructure.EFRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AspnetCoreEcommerce.Infrastructure.Services.Catalog
{
    public class CategoryService : ICategoryService
    {
        #region Fields

        private readonly ApplicationDbContext _context;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<ProductCategoryMapping> _productCategoryRepository;

        #endregion

        #region Constructor

        public CategoryService(
            ApplicationDbContext context,
            IRepository<Category> categoryRepository,
            IRepository<ProductCategoryMapping> productCategoryMappingRepository)
        {
            _context = context;
            _categoryRepository = categoryRepository;
            _productCategoryRepository = productCategoryMappingRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns></returns>
        public IList<Category> GetAllCategories()
        {
            var entities = _categoryRepository.GetAll()
                .OrderBy(x => x.Name)
                .ToList();

            return entities;
        }

        public IList<Category> GetAllCategoriesWithoutParent()
        {
            var entities = _categoryRepository
                .FindManyByExpression(x => x.ParentCategoryId == Guid.Empty)
                .OrderBy(x => x.Name)
                .ToList();

            return entities;

        }

        public Category GetCategoryById(Guid id)
        {
            var entity = _categoryRepository
                .FindByExpression(x => x.Id == id);

            return entity;
        }

        public Category GetCategoryBySeo(string seoUrl)
        {
            if (string.IsNullOrEmpty(seoUrl) || string.IsNullOrWhiteSpace(seoUrl))
                return null;

            var entity = _categoryRepository
                .FindByExpression(x => x.SeoUrl == seoUrl);

            return entity;
        }

        public void InsertCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException("category");

            _categoryRepository.Insert(category);
            _categoryRepository.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            if (category == null)
                throw new ArgumentException("category");

            _categoryRepository.Update(category);
            _categoryRepository.SaveChanges();
        }

        public void DeleteCategories(IList<Guid> ids)
        {
            if (ids == null)
                throw new ArgumentNullException("category");

            foreach (var categoryId in ids)
                _categoryRepository.Delete(GetCategoryById(categoryId));

            _categoryRepository.SaveChanges();
        }

        public void InsertProductCategoryMappings(IList<ProductCategoryMapping> productCategoryMappings)
        {
            if (productCategoryMappings == null)
                throw new ArgumentNullException("productCategoryMappings");

            foreach (var prodMapping in productCategoryMappings)
                _productCategoryRepository.Insert(prodMapping);

            _productCategoryRepository.SaveChanges();
        }

        public void DeleteAllProductCategoryMappingsByProductId(Guid productId)
        {
            if (productId == null)
                throw new ArgumentNullException("productId");

            var mappings = _productCategoryRepository.FindManyByExpression(x => x.ProductId == productId);

            foreach (var mapping in mappings)
                _productCategoryRepository.Delete(mapping);

            _productCategoryRepository.SaveChanges();
        }

        #endregion
    }
}
