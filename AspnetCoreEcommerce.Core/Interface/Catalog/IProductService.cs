using AspnetCoreEcommerce.Core.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AspnetCoreEcommerce.Core.Interface.Catalog
{
    public interface IProductService
    {
        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns>List of Product entities</returns>
        IList<Product> GetAllProducts();

        /// <summary>
        /// Get product using id
        /// </summary>
        /// <param name="id">Id of the PRoduct</param>
        /// <returns>Product Entity</returns>
        Product GetProductById(Guid id);

        /// <summary>
        /// Get Product using SEO
        /// </summary>
        /// <param name="seo">Product SEO</param>
        /// <returns>Product Entity</returns>
        Product GetProductBySeo(string seo);

        /// <summary>
        /// Insert a Product
        /// </summary>
        /// <param name="product">Product to insert</param>
        void InsertProduct(Product product);

        /// <summary>
        /// Updates the given entity
        /// </summary>
        /// <param name="product">Entity to update</param>
        void UpdateProduct(Product product);

        /// <summary>
        /// Delete all entities specified by id
        /// </summary>
        /// <param name="productsId">List of entity ids</param>
        void DeleteProducts(IList<Guid> productsId);

        /// <summary>
        /// Search products
        /// </summary>
        /// <param name="nameFilter">Name filter</param>
        /// <param name="seoFilter">SEO filter</param>
        /// <param name="categoryFilter">Category filter</param>
        /// <param name="manufacturerFilter">Manufacturer filter</param>
        /// <param name="priceFilter">Price filter</param>
        /// <param name="isPublished">Published filter</param>
        /// <returns>List of product entities</returns>
        IList<Product> SearchProduct(
            string nameFilter = null,
            string seoFilter = null,
            string[] categoryFilter = null,
            string[] manufacturerFilter = null,
            string[] priceFilter = null,
            bool isPublished = true);

        /// <summary>
        /// Get product context table
        /// </summary>
        /// <returns></returns>
        IQueryable<Product> Table();


    }
}
