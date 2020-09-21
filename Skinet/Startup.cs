using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Skiner.Data.Contexts;
using Skiner.Infrastructure.Repositories;
using Skinet.Core.Interfaces;

namespace Skinet
{
    public class Startup
    {
        private IConfiguration _Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // DI cont
        public void ConfigureServices(IServiceCollection services)
        {
            AddDbContext(services);
            AddServices(services);
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // Middleware
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AddDbContext(IServiceCollection services)
        {
            services.AddDbContext<StoreContext>(x =>
                x.UseSqlite(_Configuration.GetConnectionString("SkinetConnectionStr")));
        }

        private void AddServices(IServiceCollection service)
        {
            service.AddScoped<IProductRepository, ProductRepository>();
            // Generic repository injection
            service.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }
    }
}
