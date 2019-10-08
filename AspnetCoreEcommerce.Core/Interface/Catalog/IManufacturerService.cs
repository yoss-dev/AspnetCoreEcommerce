using AspnetCoreEcommerce.Core.Domain.Catalog;
using System;
using System.Collections.Generic;

namespace AspnetCoreEcommerce.Core.Interface.Catalog
{
    public interface IManufacturerService
    {
        IList<Manufacturer> GetManufacturers();

        /// <summary>
        /// Get Manufacturer by Id
        /// </summary>
        /// <param name="id">Id of the Manufacturer</param>
        /// <returns>Manufacturer Entity</returns>
        Manufacturer GetManufacturerById(Guid id);

        /// <summary>
        /// Get Manufacturer by SEO
        /// </summary>
        /// <param name="seo">Manufacturer SEO</param>
        /// <returns>Manufacturer entity</returns>
        Manufacturer GetManufacturerBySeo(string seo);

        /// <summary>
        /// Insert a manufacturer
        /// </summary>
        /// <param name="manufacturer">Manufacturer entity</param>
        void InsertManufacturer(Manufacturer manufacturer);

        /// <summary>
        /// Update a manufacturer
        /// </summary>
        /// <param name="manufacturer">Manufacturer entity</param>
        void UpdateManufacturer(Manufacturer manufacturer);

        /// <summary>
        /// Delete manufacturers
        /// </summary>
        /// <param name="ids">Ids of manufacturers to delete</param>
        void DeleteManufacturers(IList<Guid> ids);

        /// <summary>
        /// Insert product manufacturer mappings
        /// </summary>
        /// <param name="productManufacturerMappings">List of product manufacturer mappings</param>
        void InsertProductManufacturerMappings(IList<ProductManufacturerMapping> productManufacturerMappings);

        /// <summary>
        /// Deletes all manufacturer mappings associated with the specified product
        /// </summary>
        /// <param name="productId">Product id</param>
        void DeleteAllProductManufacturersMappings(Guid productId);
    }
}
