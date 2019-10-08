using Microsoft.AspNetCore.Identity;
using System;

namespace AspnetCoreEcommerce.Infrastructure.EFModels
{
    public class ApplicationUser : IdentityUser
    {
        public Guid BillingAddressId { get; set; }
    }
}
