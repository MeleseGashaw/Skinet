global using Infrastructure.Data;

global using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using Infrastructure.Data.SeedData;
using API.Helper;
using API.MiddleWare;
using API.Extentions;
using Microsoft.AspNetCore.Mvc;
using API.Errors;
using System.Linq;
using AutoMapper;
using StackExchange.Redis;

using Infrastructure.Data.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Core.Entities.Identity;
using Infrastructure.Identity.Migrations;
internal class Program
{
    private static async Task Main(string[] args)
    {
        //using Microsoft.EntityFrameworkCore.UseSqlServer;
        var builder = WebApplication.CreateBuilder(args);
        var host = WebApplication.Create(args);
        // Add services to the container..
        //private readonly IConfiguration _config;

        builder.Services.AddControllers();
        builder.Services.AddAutoMapper(typeof(MappingProfiles));
        builder.Services.AddDbContext<StoreContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

        });
        builder.Services.AddDbContext<AppIdentityDBContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));

        });


        builder.Services.AddSingleton<IConnectionMultiplexer>(c =>
        {
            var configration = ConfigurationOptions.Parse(builder.Configuration.
            GetConnectionString("Redis"), true);
            return ConnectionMultiplexer.Connect(configration);
        });
        builder.Services.AddApplicationServices();
        builder.Services.AddIdentityService(builder.Configuration);
        builder.Services.AddSwaggerDocumentation();
        builder.Services.AddSwaggerGen();
        builder.Services.AddCors(opt =>
        {
            opt.AddPolicy("CrosPolicy", policy =>
            {
                policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200/");

            });

        }

        );
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();


        var app = builder.Build();

        //--------------------------------------------------------------
        var scope = app.Services.CreateScope();
        var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
        try
        {
            using (scope)
            {
                var dataContext = scope.ServiceProvider.GetRequiredService<StoreContext>();
                dataContext.Database.Migrate();
                await StoreContextSeed.SeedAsync(dataContext, loggerFactory);

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var identitycontext = scope.ServiceProvider.GetRequiredService<AppIdentityDBContext>();
                identitycontext.Database.Migrate();
                await AppIdentityDbContextSeed.SeedUsersAsync(userManager);
            }
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogError(ex.Message);
        }
        //-----------------------------------------------------------------------

        // Configure the HTTP request pipeline.
        app.UseMiddleware<ExceptionMiddleware>();
        /*  if (app.Environment.IsDevelopment())
          {

          }*/
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseSwaggerDocumentation();

        app.UseStatusCodePagesWithReExecute("/errors/{0}");
        app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseCors("CrosPolicy");
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
//public static async void Main (string [] args)
//{

//}

//}
