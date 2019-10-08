using AspnetCoreEcommerce.Core.Domain.Catalog;
using AspnetCoreEcommerce.Core.Domain.Messages;
using AspnetCoreEcommerce.Core.Domain.Sale;
using AspnetCoreEcommerce.Core.Domain.Statistics;
using AspnetCoreEcommerce.Core.Domain.User;
using AspnetCoreEcommerce.Infrastructure;
using AspnetCoreEcommerce.Infrastructure.EFRepository;
using AspnetCoreEcommerce.Infrastructure.Services.Catalog;
using AspnetCoreEcommerce.Infrastructure.Services.Messages;
using AspnetCoreEcommerce.Infrastructure.Services.Sale;
using AspnetCoreEcommerce.Infrastructure.Services.Statistics;

namespace AspnetCoreEcommerce.xUnitTest
{
    public class Service
    {
        public Service(ApplicationDbContext context)
        {
            //repository
            BillingAddressRepository = new Repository<BillingAddress>(context);
            CategoryRepository = new Repository<Category>(context);
            ImageRepository = new Repository<Image>(context);
            ReviewRepository = new Repository<Review>(context);
            ManufacturerRepository = new Repository<Manufacturer>(context);
            OrderRepository = new Repository<Order>(context);
            OrderCountRepository = new Repository<OrderCount>(context);
            OrderItemRepository = new Repository<OrderItem>(context);
            ProductRepository = new Repository<Product>(context);
            ProductCategoryMapping = new Repository<ProductCategoryMapping>(context);
            ProductImageMapping = new Repository<ProductImageMapping>(context);
            ProductManufacturerMapping = new Repository<ProductManufacturerMapping>(context);
            ContactUsMessageRepository = new Repository<ContactUsMessage>(context);
            VisitorCountRepository = new Repository<VisitorCount>(context);
            //service
            CategoryService = new CategoryService(context, CategoryRepository, ProductCategoryMapping);
            ProductService = new ProductService(context, ProductRepository);
            ImageManagerService = new ImageManagerService(ImageRepository, ProductImageMapping);
            ManufacturerService = new ManufacturerService(context, ManufacturerRepository, ProductManufacturerMapping);
            ReviewService = new ReviewService(context, ReviewRepository);
            SpecificationService = new SpecificationService(context, SpecificationRepository, ProductSpecificationMapping);
            ContactUsService = new ContactUsService(ContactUsMessageRepository);
            OrderService = new OrderService(OrderRepository, OrderItemRepository, OrderCountService, context);
            OrderCountService = new OrderCountService(OrderCountRepository);
            VisitorCountService = new VisitorCountService(VisitorCountRepository, context);
        }

        //repository
        private Repository<BillingAddress> BillingAddressRepository { get; set; }
        private Repository<Category> CategoryRepository { get; set; }
        private Repository<Image> ImageRepository { get; set; }
        private Repository<Manufacturer> ManufacturerRepository { get; set; }
        private Repository<Order> OrderRepository { get; set; }
        private Repository<OrderItem> OrderItemRepository { get; set; }
        private Repository<Product> ProductRepository { get; set; }
        private Repository<ProductCategoryMapping> ProductCategoryMapping { get; set; }
        private Repository<ProductImageMapping> ProductImageMapping { get; set; }
        private Repository<ProductManufacturerMapping> ProductManufacturerMapping { get; set; }
        private Repository<ProductSpecificationMapping> ProductSpecificationMapping { get; set; }
        private Repository<Review> ReviewRepository { get; set; }
        private Repository<Specification> SpecificationRepository { get; set; }
        private Repository<VisitorCount> VisitorCountRepository { get; set; }
        private Repository<OrderCount> OrderCountRepository { get; set; }
        private Repository<ContactUsMessage> ContactUsMessageRepository { get; set; }

        // service
        //public BillingAddressService BillingAddressService { get; set; }
        public CategoryService CategoryService { get; set; }
        public ImageManagerService ImageManagerService { get; set; }
        public ManufacturerService ManufacturerService { get; set; }

        public ProductService ProductService { get; set; }
        public ReviewService ReviewService { get; set; }
        public SpecificationService SpecificationService { get; set; }
        public OrderService OrderService { get; private set; }
        public VisitorCountService VisitorCountService { get; set; }
        public OrderCountService OrderCountService { get; set; }
        public ContactUsService ContactUsService { get; set; }
    }
}
