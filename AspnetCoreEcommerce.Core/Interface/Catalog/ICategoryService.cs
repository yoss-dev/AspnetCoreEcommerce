using AspnetCoreEcommerce.Core.Domain.Catalog;
using System;
using System.Collections.Generic;

namespace AspnetCoreEcommerce.Core.Interface.Catalog
{
    public interface ICategoryService
    {
        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns>List of Category entities</returns>
        IList<Category> GetAllCategories();

        /// <summary>
        /// Get all categories without parent
        /// </summary>
        /// <returns>List of categories entities without parent</returns>
        IList<Category> GetAllCategoriesWithoutParent();

        /// <summary>
        /// Get category by id
        /// </summary>
        /// <param name="id">Category id</param>
        /// <returns>Category entity</returns>
        Category GetCategoryById(Guid id);

        /// <summary>
        /// Get category using SEO
        /// </summary>
        /// <param name="seo">Category SEO</param>
        /// <returns>Category entity</returns>
        Category GetCategoryBySeo(string seo);

        ///<summary>
        /// Insert category
        /// </summary>
        /// <param name="category">Category entity</param>
        void InsertCategory(Category category);

        /// <summary>
        /// Update Category
        /// </summary>
        /// <param name="category">Category entity</param>
        void UpdateCategory(Category category);

        /// <summary>
        /// Delete multiple categories
        /// </summary>
        /// <param name="ids">Ids of the categories to delete</param>
        void DeleteCategories(IList<Guid> ids);

        /// <summary>
        /// Insert product category mappings
        /// </summary>
        /// <param name="productCategoryMappings">List of product category mapping</param>
        void InsertProductCategoryMappings(IList<ProductCategoryMapping> productCategoryMappings);

        /// <summary>
        /// Delete all product category mappings using product id
        /// </summary>
        /// <param name="productId">Product id</param>
        void DeleteAllProductCategoryMappingsByProductId(Guid productId);

    }
}
