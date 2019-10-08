using AspnetCoreEcommerce.Core.Domain.Catalog;
using AspnetCoreEcommerce.Core.Interface.Catalog;
using AspnetCoreEcommerce.Infrastructure.EFRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AspnetCoreEcommerce.Infrastructure.Services.Catalog
{
    public class ProductService : IProductService
    {
        #region Fields 

        private readonly ApplicationDbContext _context;
        private readonly IRepository<Product> _productRepository;

        #endregion

        #region Constructor

        public ProductService(
            ApplicationDbContext context,
            IRepository<Product> productRepository)
        {
            _context = context;
            _productRepository = productRepository;
        }

        #endregion

        #region Methods

        public IList<Product> GetAllProducts()
        {
            //TODO: update when lazy loading is available
            var entities = _context.Products
                .Include(x => x.Categories).ThenInclude(x => x.Category)
                .Include(x => x.Images).ThenInclude(x => x.Image)
                .Include(x => x.Manufacturers).ThenInclude(x => x.Manufacturer)
                .Include(x => x.Specifications).ThenInclude(x => x.Specification)
                .AsNoTracking()
                .ToList();

            return entities;
        }

        public Product GetProductById(Guid id)
        {
            if (id.Equals(Guid.Empty))
                return null;

            //TODO: update when lazy loading becomes available
            var entity = _context.Products
                .Include(x => x.Categories).ThenInclude(x => x.Category)
                .Include(x => x.Images).ThenInclude(x => x.Image)
                .Include(x => x.Manufacturers).ThenInclude(x => x.Manufacturer)
                .Include(x => x.Specifications).ThenInclude(x => x.Specification)
                .AsNoTracking()
                .SingleOrDefault(x => x.Id == id);

            return entity;
        }

        public Product GetProductBySeo(string seo)
        {
            if (string.IsNullOrEmpty(seo) || string.IsNullOrWhiteSpace(seo))
                return null;

            var product = _context.Products
                .Include(x => x.Categories).ThenInclude(x => x.Category)
                .Include(x => x.Images).ThenInclude(x => x.Image)
                .Include(x => x.Manufacturers).ThenInclude(x => x.Manufacturer)
                .Include(x => x.Specifications).ThenInclude(x => x.Specification)
                .AsNoTracking()
                .SingleOrDefault(x => x.SeoUrl == seo);

            return product;
        }

        public void InsertProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            _productRepository.Insert(product);
            _productRepository.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            _productRepository.Update(product);
            _productRepository.SaveChanges();

        }

        public void DeleteProducts(IList<Guid> productsId)
        {
            if (productsId == null)
                throw new ArgumentNullException(nameof(productsId));

            foreach (var id in productsId)
                _productRepository.Delete(GetProductById(id));

            _productRepository.SaveChanges();
        }

        public IList<Product> SearchProduct(string nameFilter = null, string seoFilter = null, string[] categoryFilter = null,
            string[] manufacturerFilter = null, string[] priceFilter = null, bool isPublished = true)
        {
            var productEntity = _context.Products
                .Include(x => x.Categories).ThenInclude(x => x.Category)
                .Include(x => x.Images).ThenInclude(x => x.Image)
                .Include(x => x.Manufacturers).ThenInclude(x => x.Manufacturer)
                .Include(x => x.Specifications).ThenInclude(x => x.Specification)
                .AsNoTracking();

            //published filter
            if (isPublished == false)
            {
                productEntity = productEntity.Where(x => x.Published == false);
            }

            //name filter
            if (!string.IsNullOrEmpty(nameFilter))
            {
                productEntity = productEntity
                    .Where(x => x.Name.ToLower().Contains(nameFilter.ToLower()));
            }

            //seo filter
            if (!string.IsNullOrEmpty(seoFilter))
            {
                throw new NotImplementedException(nameof(seoFilter));
            }

            //category filter
            if (categoryFilter != null && categoryFilter.Length > 0)
            {
                productEntity = productEntity
                                    .Where(x => x.Categories.Select(c => c.Category.Name.ToLower())
                                    .Intersect(categoryFilter.Select(cf => cf.ToLower()))
                                    .Count() > 0);
            }

            //manufacturer filter
            if (manufacturerFilter != null && manufacturerFilter.Length > 0)
            {
                productEntity = productEntity
                    .Where(x => x.Manufacturers
                                    .Select(c => c.Manufacturer.Name.ToLower())
                                    .Intersect(manufacturerFilter.Select(mf => mf.ToLower()))
                                    .Count() > 0);
            }

            //price filter
            if (priceFilter != null && priceFilter.Length > 0)
            {
                var tmpResult = new List<Product>();
                foreach (var price in priceFilter)
                {
                    var p = price.Split('-');
                    var minPrice = int.Parse(p[0]);
                    var maxPrice = int.Parse(p[1]);

                    var r = productEntity
                        .Where(
                            x => x.Price >= minPrice &&
                                 x.Price <= maxPrice);

                    if (r.Count() > 0)
                        tmpResult.AddRange(r);

                }

                productEntity = tmpResult.AsQueryable();
            }

            return productEntity.ToList();
        }

        public IQueryable<Product> Table()
        {
            return _context.Products;
        }

        #endregion
    }
}
