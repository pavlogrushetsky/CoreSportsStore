using CoreSportsStore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CoreSportsStore
{
    public class Startup
    {
        private readonly IConfigurationRoot Configuration;

        public Startup(IHostingEnvironment environment)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    null,
                    "{category}/Page{page:int}",
                    new {controller = "Product", action = "List"}
                );

                routes.MapRoute(
                    null,
                    "Page{page:int}",
                    new { controller = "Product", action = "List", page = 1 }
                );

                routes.MapRoute(
                    null,
                    "{category}",
                    new { controller = "Product", action = "List", page = 1 }
                );

                routes.MapRoute(
                    null,
                    "",
                    new { controller = "Product", action = "List", page = 1 }
                );

                routes.MapRoute(
                    null,
                    "{controller}/{action}/{id?}"
                );
            });
            SeedData.EnsurePopulated(app);
        }
    }
}
