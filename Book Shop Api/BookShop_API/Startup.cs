using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop_API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

//using BookShop_Repostory.Models;
using Microsoft.EntityFrameworkCore;
using BookShop_API.BookShop_Repostory.Context;
using BookShop_API.BookShop_Repostory.Repostory;
using BookShop_API.BookShopServices.Interface;
using BookShop_API.BookShopServices.Implementation;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Localization.Routing;
using BookShop_API.Extentions;
using BookShop_API.MappingProfile;
using Microsoft.Extensions.Options;

namespace BookShop_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("en-US"),
                        new CultureInfo("az-Latn-AZ"),
                        new CultureInfo("ru-RU")
                    };

                    options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;
                    options.RequestCultureProviders = new[] { new Extentions.RouteDataRequestCultureProvider { IndexOfCulture = 1, IndexofUICulture = 1 } };
                });
          

            services.Configure<RouteOptions>(options =>
            {
                options.ConstraintMap.Add("culture", typeof(LanguageRouteConstraint));
            });
            services.Configure<IdentityOptions>(option =>
            {
                option.Password.RequireDigit = false;
            });
            services.AddControllersWithViews().AddNewtonsoftJson();
            services.AddAutoMapper(x => x.AddProfile(new MappingEntity()));
            services.ConfigureCors();
            services.ConfigureJWTService();
            services.AddControllers();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAuthorService, AuthorService>();
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ILanguageService, LanguageService>();
            services.AddSwaggerGen();

            services.AddDbContext<IdentityAppContext>(x => x.UseSqlServer(Configuration["ConnectionString:DefaultConnectionString"]));

            services.AddIdentity<AppUser, Role>().AddDefaultTokenProviders().AddEntityFrameworkStores<IdentityAppContext>();
                                      

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthorization();
            app.UseAuthentication();

            var localizeOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(localizeOptions.Value);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Book Shop API v1");
            });
            app.UseCookiePolicy();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
        }
    }
}
