using AspnetCoreEcommerce.Core.Domain.Catalog;
using AspnetCoreEcommerce.Core.Interface.Catalog;
using AspnetCoreEcommerce.Infrastructure.EFRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AspnetCoreEcommerce.Infrastructure.Services.Catalog
{
    public class ReviewService : IReviewService
    {
        #region Fields

        private readonly ApplicationDbContext _context;
        private readonly IRepository<Review> _reviewRepository;

        #endregion

        #region Constructor

        public ReviewService(ApplicationDbContext context, IRepository<Review> reviewRepository)
        {
            _context = context;
            _reviewRepository = reviewRepository;
        }

        #endregion

        #region Methods

        public IList<Review> GetReviewsByProductId(Guid productId)
        {
            if (productId.Equals(Guid.Empty))
                return null;

            var reviews = _reviewRepository.FindManyByExpression(x => x.ProductId == productId)
                .ToList();

            return reviews;
        }

        public Review GetReviewByProductIdUserId(Guid productId, Guid userId)
        {
            if (productId.Equals(Guid.Empty) || userId.Equals(Guid.Empty))
                return null;

            var result = _reviewRepository
                .FindByExpression(x => (x.ProductId == productId) && x.UserId == userId);

            return result;
        }

        public void InsertReview(Review review)
        {
            if (review == null)
                throw new ArgumentNullException(nameof(review));

            _reviewRepository.Insert(review);
            _reviewRepository.SaveChanges();
        }

        public void UpdateReview(Review review)
        {
            if (review == null)
                throw new ArgumentNullException(nameof(review));

            _reviewRepository.Update(review);
            _reviewRepository.SaveChanges();
        }

        #endregion
    }
}
