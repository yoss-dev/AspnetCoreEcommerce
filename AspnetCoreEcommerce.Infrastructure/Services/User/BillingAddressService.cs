using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetCoreEcommerce.Core.Domain.User;
using AspnetCoreEcommerce.Core.Interface.User;
using AspnetCoreEcommerce.Infrastructure.EFRepository;

namespace AspnetCoreEcommerce.Infrastructure.Services.User
{
    public class BillingAddressService: IBillingAddressService
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepository<BillingAddress> _billingAddressRepository;

        public BillingAddressService(ApplicationDbContext context, 
            IRepository<BillingAddress> billingAddressRepository)
        {
            _context = context;
            _billingAddressRepository = billingAddressRepository;
        }

        public BillingAddress GetBillingAddressById(Guid id)
        {
            return _billingAddressRepository.FindByExpression(x => x.Id == id);
        }

        public void InsertBillingAddress(BillingAddress billingAddress)
        {
            if (billingAddress == null)
                throw new ArgumentNullException("billingAddress");

            _billingAddressRepository.Insert(billingAddress);
            _billingAddressRepository.SaveChanges();
        }

        public void UpdateBillingAddress(BillingAddress billingAddress)
        {
            if (billingAddress == null)
                throw new ArgumentNullException("billingAddress");

            _billingAddressRepository.Update(billingAddress);
            _billingAddressRepository.SaveChanges();
        }
    }
}
