using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
  using Infrastructure.Data;

  using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using Infrastructure.Data.SeedData;
using API.Helper;
using API.MiddleWare;
using Microsoft.AspNetCore.Mvc;
using API.Errors;
using Infrastructure.Services;

namespace API.Extentions
{
    public static class ApplicationServicesExtentions
    { 
        
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var builder = WebApplication.CreateBuilder();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBasketRepository,BasketRepository>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.Configure<ApiBehaviorOptions>(options =>
        {
          options.InvalidModelStateResponseFactory = actionContext=>
          {
            var errors = actionContext.ModelState
              .Where (e=>e.Value.Errors.Count > 0)
              .SelectMany(x=>x.Value.Errors)
              .Select(x=>x.ErrorMessage).ToArray();

              var errorResponse = new ApiValidationErrorResponse 
              {
                errors=errors
              };

              return new BadRequestObjectResult(errorResponse);
          };
         });
        return services;
        }
        
    }
}