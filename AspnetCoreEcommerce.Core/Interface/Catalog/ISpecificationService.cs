using AspnetCoreEcommerce.Core.Domain.Catalog;
using System;
using System.Collections.Generic;

namespace AspnetCoreEcommerce.Core.Interface.Catalog
{
    public interface ISpecificationService
    {
        /// <summary>
        /// Get all Specifications
        /// </summary>
        /// <returns>List of Specifications entities</returns>
        IList<Specification> GetAllSpecifications();

        /// <summary>
        /// Get Specification by Id
        /// </summary>
        /// <param name="id">Specification id</param>
        /// <returns>Specification Entity</returns>
        Specification GetSpecificationById(Guid id);

        /// <summary>
        /// Insert specification
        /// </summary>
        /// <param name="specification">Specification entity</param>
        void InsertSpecification(Specification specification);

        /// <summary>
        /// Insert Specification
        /// </summary>
        /// <param name="specification"></param>
        void UpdateSpecification(Specification specification);

        /// <summary>
        /// Delete specifications
        /// </summary>
        /// <param name="specifications">List of specification ids</param>
        void DeleteSpecifications(IList<Guid> specifications);

        /// <summary>
        /// Insert product specification mappings
        /// </summary>
        /// <param name="productSpecificationMappings">Product specification mappings</param>
        void InsertProductSpecificationMappings(IList<ProductSpecificationMapping> productSpecificationMappings);

        /// <summary>
        /// Delete all product specifications by product id
        /// </summary>
        /// <param name="productId">Product id</param>
        void DeleteAllProductSpecificationMappings(Guid productId);
    }


}
