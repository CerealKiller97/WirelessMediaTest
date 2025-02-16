using System;
using Contracts.Products;
using DataAccess;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Products;
using Transfer;

namespace WirelessMedia
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WirelessDbContext>(options =>
            {
                options.UseNpgsql(
                    Configuration.GetConnectionString("Default"),
                    builder => builder.MigrationsAssembly("")
                );
                options.UseSnakeCaseNamingConvention();
            });
            services.AddMediatR(typeof(Startup));
            services.AddControllersWithViews();

            var storage = Configuration.GetValue<string>("ProductService");

            switch (storage)
            {
                case "json":
                    services.AddSingleton<IProductService<JsonProductDto>>((sp) =>
                        new JsonProductService(Configuration.GetValue<string>("JsonPath")));
                    break;
                case "db":
                    services.AddTransient<IProductService<DbProductDto>>(sp => sp.GetService<DbProductService>());
                    break;
                default:
                    throw new ArgumentException($"Storage {storage} is not valid. Choose json or db");
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}