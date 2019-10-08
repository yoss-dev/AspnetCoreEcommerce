using AspnetCoreEcommerce.Core.Domain.Catalog;
using AspnetCoreEcommerce.Core.Interface.Catalog;
using AspnetCoreEcommerce.Infrastructure.EFRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AspnetCoreEcommerce.Infrastructure.Services.Catalog
{
    public class ImageManagerService : IImageManagerService
    {
        #region Fields

        private readonly IRepository<Image> _imageRepository;
        private readonly IRepository<ProductImageMapping> _productImagesRepository;

        #endregion

        #region Constructor

        public ImageManagerService(
            IRepository<Image> imageRepository,
            IRepository<ProductImageMapping> productImagesRepository)
        {
            _imageRepository = imageRepository;
            _productImagesRepository = productImagesRepository;
        }

        #endregion

        #region Methods

        public IList<Image> GetAllImages()
        {
            return _imageRepository.GetAll()
                .OrderBy(x => x.FileName).ToList();
        }

        public Image GetImageById(Guid id)
        {
            if (id.Equals(Guid.Empty))
                return null;

            var result = _imageRepository.FindByExpression(x => x.Id == id);

            return result;
        }

        public IList<Image> SearchImages(string keyword)
        {
            return _imageRepository.FindManyByExpression(x => x.FileName.Contains(keyword))
                .OrderBy(x => x.FileName)
                .ToList();
        }

        public void InsertImages(IList<Image> images)
        {
            foreach (var image in images)
            {
                _imageRepository.Insert(image);
            }

            _imageRepository.SaveChanges();
        }

        public void DeleteImages(IList<Guid> ids)
        {
            foreach (var id in ids)
            {
                _imageRepository.Delete(GetImageById(id));
            }

            _imageRepository.SaveChanges();
        }

        public void InsertProductImageMappings(IList<ProductImageMapping> productImageMappings)
        {
            if (productImageMappings == null)
                throw new ArgumentNullException(nameof(productImageMappings));

            foreach (var productImageMapping in productImageMappings)
            {
                _productImagesRepository.Insert(productImageMapping);
            }

            _productImagesRepository.SaveChanges();
        }

        public void DeleteAllProductImageMappings(Guid productId)
        {
            if (productId.Equals(Guid.Empty))
                throw new ArgumentNullException(nameof(productId));

            var productImageMappings = _productImagesRepository.FindManyByExpression(x => x.ProductId == productId).ToList();

            foreach (var productImageMapping in productImageMappings)
                _productImagesRepository.Delete(productImageMapping);

            _productImagesRepository.SaveChanges();
        }

        #endregion
    }
}
