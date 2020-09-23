using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Skiner.Data.Contexts;
using Skiner.Infrastructure.Repositories;
using Skinet.Core.Interfaces;
using Skinet.Errors;
using Skinet.Middleware;
using System.Linq;

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
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .SelectMany(y => y.Value.Errors)
                    .Select(z => z.ErrorMessage)
                    .ToArray();

                    var errorResponse = new ApiValidationError{
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // Middleware
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Exception middleware
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseStatusCodePagesWithReExecute("error/{0}");
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseStaticFiles();

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
            service.AddAutoMapper(typeof(Startup));
            service.AddScoped<IProductRepository, ProductRepository>();
            // Generic repository injection
            service.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }
    }
}
