using AspnetCoreEcommerce.Core.Domain.Catalog;
using System;
using System.Collections.Generic;

namespace AspnetCoreEcommerce.Core.Interface.Catalog
{
    public interface IReviewService
    {
        /// <summary>
        /// Get Reviews by Product id
        /// </summary>
        /// <param name="productId">Product id</param>
        /// <returns>List of Product entities</returns>
        IList<Review> GetReviewsByProductId(Guid productId);

        /// <summary>
        /// Get Review by productId and userId
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <param name="userId">User Id</param>
        /// <returns>Review Entity</returns>
        Review GetReviewByProductIdUserId(Guid productId, Guid userId);

        /// <summary>
        /// Insert a Review
        /// </summary>
        /// <param name="review"></param>
        void InsertReview(Review review);

        /// <summary>
        /// Update Review
        /// </summary>
        /// <param name="review"></param>
        void UpdateReview(Review review);
    }
}
