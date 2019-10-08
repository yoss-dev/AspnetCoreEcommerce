using AspnetCoreEcommerce.Core.Domain.Catalog;
using System;
using System.Collections.Generic;

namespace AspnetCoreEcommerce.Core.Interface.Catalog
{
    public interface IImageManagerService
    {
        /// <summary>
        /// Get all images
        /// </summary>
        /// <returns>List of image entities</returns>
        IList<Image> GetAllImages();

        /// <summary>
        /// Get image using id
        /// </summary>
        /// <param name="id">Image id</param>
        /// <returns></returns>
        Image GetImageById(Guid id);

        /// <summary>
        /// Search images
        /// </summary>
        /// <param name="keyword">keyword</param>
        /// <returns>List of image entities</returns>
        IList<Image> SearchImages(string keyword);

        /// <summary>
        /// Insert Image
        /// </summary>
        /// <param name="images">List of images to insert</param>
        void InsertImages(IList<Image> images);

        /// <summary>
        /// Delete images with the specified id
        /// </summary>
        /// <param name="ids">Ids of image entities to delete</param>
        void DeleteImages(IList<Guid> ids);

        /// <summary>
        /// Insert a product image mapping
        /// </summary>
        /// <param name="productImageMappings">List of product image mappings</param>
        void InsertProductImageMappings(IList<ProductImageMapping> productImageMappings);

        /// <summary>
        /// Delete product image mapping
        /// </summary>
        /// <param name="productId">Product id</param>
        void DeleteAllProductImageMappings(Guid productId);
    }
}
