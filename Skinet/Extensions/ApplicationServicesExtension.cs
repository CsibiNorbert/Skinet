using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Skiner.Infrastructure.Repositories;
using Skinet.Core.Interfaces;
using Skinet.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skinet.Extensions
{
    public static class ApplicationServicesExtension
    {
        // When exdending class don`t forget to use the class as static and pass 'this' with the class that will be extended
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            // Generic repository injection
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .SelectMany(y => y.Value.Errors)
                    .Select(z => z.ErrorMessage)
                    .ToArray();

                    var errorResponse = new ApiValidationError
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            return services;
        }
    }
}
