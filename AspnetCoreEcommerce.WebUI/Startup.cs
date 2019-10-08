using AspnetCoreEcommerce.Core.Interface.Catalog;
using AspnetCoreEcommerce.Core.Interface.Sale;
using AspnetCoreEcommerce.Core.Interface.Statistics;
using AspnetCoreEcommerce.Core.Interface.User;
using AspnetCoreEcommerce.Infrastructure;
using AspnetCoreEcommerce.Infrastructure.EFModels;
using AspnetCoreEcommerce.Infrastructure.EFRepository;
using AspnetCoreEcommerce.Infrastructure.Services.Catalog;
using AspnetCoreEcommerce.Infrastructure.Services.Sale;
using AspnetCoreEcommerce.Infrastructure.Services.Statistics;
using AspnetCoreEcommerce.Infrastructure.Services.User;
using AspnetCoreEcommerce.WebUI.Areas.Admin.Helpers;
using AspnetCoreEcommerce.WebUI.Helpers;
using AspnetCoreEcommerce.WebUI.Middleware;
using AspnetCoreEcommerce.WebUI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System;

namespace AspnetCoreEcommerce.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            HostingEnvironment = environment;

            MapperConfiguration = new MapperConfiguration(cfg =>
                cfg.AddProfile(new AutoMapperProrileConfiguration()));

        }

        public IConfiguration Configuration { get; }
        public MapperConfiguration MapperConfiguration { get; set; }
        private IHostingEnvironment HostingEnvironment;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Add framework services
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //Identity password requirement
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //configure admin account injectable
            services.Configure<AdminAccount>(
                Configuration.GetSection("AdminAccount"));

            services.Configure<UserAccount>(
                Configuration.GetSection("UserAccount"));


            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.Name = "aspCart";
            });

            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<IVisitorCountService>>();


            //Add application services
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IBillingAddressService, BillingAddressService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IImageManagerService, ImageManagerService>();
            services.AddTransient<IManufacturerService, ManufacturerService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IReviewService, ReviewService>();
            services.AddTransient<ISpecificationService, SpecificationService>();

            services.AddTransient<IOrderCountService, OrderCountService>();
            services.AddTransient<IVisitorCountService, VisitorCountService>();

            //singleton
            services.AddSingleton(sp => MapperConfiguration.CreateMapper());
            services.AddSingleton<ViewHelper>();
            services.AddSingleton<DataHelper>();
            services.AddSingleton<IFileProvider>(HostingEnvironment.ContentRootFileProvider);
            services.AddSingleton(typeof(ILogger), logger);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("Home/Error");
                var options = new RewriteOptions().AddRedirectToHttpsPermanent();
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }



            app.UseImageResize();
            app.UseStatusCodePages();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseSession();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            //            app.UseVisitorCounter();

            app.UseMvc();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller}/{action}/{id?}",
                    defaults: new { controller = "Dashboard", action = "Index" });

                routes.MapRoute(
                    name: "productInfo",
                    template: "Product/{seo}",
                    defaults: new { controller = "Home", action = "ProductInfo" });

                routes.MapRoute(
                    name: "category",
                    template: "Category/{category}",
                    defaults: new { controller = "Home", action = "ProductCategory" });

                routes.MapRoute(
                    name: "manufacturer",
                    template: "Manufacturer/{manufacturer}",
                    defaults: new { controller = "Home", action = "ProductManufacturer" });

                routes.MapRoute(
                    name: "productSearch",
                    template: "search/{name?}",
                    defaults: new { controller = "Home", action = "ProductSearch" });

                routes.MapRoute(
                    name: "create review",
                    template: "CreateReview/{id}",
                    defaults: new { controller = "Home", action = "CreateReview" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // apply migration
            SampleDataProvider.ApplyMigration(app.ApplicationServices);

            // seed default data

            SampleDataProvider.Seed(app.ApplicationServices, Configuration);
        }
    }
}
