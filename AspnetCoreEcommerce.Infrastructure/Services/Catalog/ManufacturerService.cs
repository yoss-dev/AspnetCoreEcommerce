using AspnetCoreEcommerce.Core.Domain.Catalog;
using AspnetCoreEcommerce.Core.Interface.Catalog;
using AspnetCoreEcommerce.Infrastructure.EFRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AspnetCoreEcommerce.Infrastructure.Services.Catalog
{
    public class ManufacturerService : IManufacturerService
    {
        #region Fields

        private readonly ApplicationDbContext _context;
        private readonly IRepository<Manufacturer> _manufacturerRepository;
        private readonly IRepository<ProductManufacturerMapping> _productManufacturerMappingRepository;

        #endregion

        #region Constructor

        public ManufacturerService(
            ApplicationDbContext context,
            IRepository<Manufacturer> manufacturerRepository,
            IRepository<ProductManufacturerMapping> productManufacturerMappingRepository)
        {
            _context = context;
            _manufacturerRepository = manufacturerRepository;
            _productManufacturerMappingRepository = productManufacturerMappingRepository;
        }

        #endregion

        #region Methods

        public IList<Manufacturer> GetManufacturers()
        {
            return _manufacturerRepository.GetAll()
                .OrderBy(x => x.Name)
                .ToList();
        }

        public Manufacturer GetManufacturerById(Guid id)
        {
            if (id.Equals(Guid.Empty))
                return null;

            return _manufacturerRepository.FindByExpression(x => x.Id == id);
        }

        public Manufacturer GetManufacturerBySeo(string seo)
        {
            if (string.IsNullOrEmpty(seo) || string.IsNullOrWhiteSpace(seo))
                return null;

            var result = _manufacturerRepository.FindByExpression(x => x.SeoUrl == seo);
            return result;
        }

        public void InsertManufacturer(Manufacturer manufacturer)
        {
            if (manufacturer == null)
                throw new ArgumentNullException(nameof(manufacturer));

            _manufacturerRepository.Insert(manufacturer);
            _manufacturerRepository.SaveChanges();
        }

        public void UpdateManufacturer(Manufacturer manufacturer)
        {
            if (manufacturer == null)
                throw new ArgumentNullException(nameof(manufacturer));

            _manufacturerRepository.Update(manufacturer);
            _manufacturerRepository.SaveChanges();
        }

        public void DeleteManufacturers(IList<Guid> ids)
        {
            if (ids == null)
                throw new ArgumentNullException(nameof(ids));

            foreach (var id in ids)
            {
                _manufacturerRepository
                    .Delete(
                        _manufacturerRepository
                            .FindByExpression(x => x.Id == id));
                _manufacturerRepository.SaveChanges();
            }
        }

        public void InsertProductManufacturerMappings(IList<ProductManufacturerMapping> productManufacturerMappings)
        {
            if (productManufacturerMappings == null)
                throw new ArgumentNullException(nameof(productManufacturerMappings));

            foreach (var mapping in productManufacturerMappings)
            {
                _productManufacturerMappingRepository.Insert(mapping);
            }

            _productManufacturerMappingRepository.SaveChanges();
        }

        public void DeleteAllProductManufacturersMappings(Guid productId)
        {
            var mappings = _productManufacturerMappingRepository
                .FindManyByExpression(x => x.ProductId == productId);

            foreach (var mapping in mappings)
                _productManufacturerMappingRepository.Delete(mapping);

            _productManufacturerMappingRepository.SaveChanges();
        }

        #endregion
    }
}
