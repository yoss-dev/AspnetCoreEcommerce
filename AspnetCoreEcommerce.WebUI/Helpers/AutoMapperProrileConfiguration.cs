using AspnetCoreEcommerce.Core.Domain.Catalog;
using AspnetCoreEcommerce.Core.Domain.Messages;
using AspnetCoreEcommerce.Core.Domain.Sale;
using AspnetCoreEcommerce.Core.Domain.User;
using AspnetCoreEcommerce.WebUI.Areas.Admin.Models.Catalog;
using AspnetCoreEcommerce.WebUI.Areas.Admin.Models.Sale;
using AspnetCoreEcommerce.WebUI.Areas.Admin.Models.Support;
using AspnetCoreEcommerce.WebUI.Models;
using AspnetCoreEcommerce.WebUI.Models.ManageViewModels;
using AutoMapper;

namespace AspnetCoreEcommerce.WebUI.Helpers
{
    public class AutoMapperProrileConfiguration : Profile
    {
        public AutoMapperProrileConfiguration()
        {
            //billing address mappings
            CreateMap<BillingAddress, BillingAddressModel>()
                .ReverseMap();
            CreateMap<BillingAddress, CheckoutModel>()
                .ReverseMap();

            //category mappings
            CreateMap<Category, CategoryListModel>();
            CreateMap<Category, CategoryCreateOrUpdateModel>()
                .ReverseMap();

            //manufacturer mappings
            CreateMap<OrderManageModel, Order>();
            CreateMap<Manufacturer, ManufacturerCreateOrUpdateModel>()
                .ReverseMap();

            //order mapping
            CreateMap<OrderManageModel, Order>();

            //product mappings
            CreateMap<Product, ProductListModel>();
            CreateMap<Product, ProductModel>()
                .ForMember(dest => dest.Categories, opt => opt.Ignore())
                .ForMember(dest => dest.Manufacturers, opt => opt.Ignore())
                .ForMember(dest => dest.Specifications, opt => opt.Ignore());
            CreateMap<Product, ProductCreateOrUpdateModel>()
                .ForMember(dest => dest.Images, opt => opt.Ignore())
                .ForMember(dest => dest.Specifications, opt => opt.Ignore());
            CreateMap<ProductCreateOrUpdateModel, Product>()
                .ForMember(dest => dest.Images, opt => opt.Ignore())
                .ForMember(dest => dest.Specifications, opt => opt.Ignore());

            //review
            CreateMap<Review, ReviewModel>()
                .ReverseMap();

            //specifications
            CreateMap<Specification, SpecificationModel>();
            CreateMap<Specification, SpecificationCreateOrUpdateModel>()
                .ReverseMap();

            //support
            CreateMap<ContactUsMessage, ContactUsMessageModel>();
        }
    }
}
