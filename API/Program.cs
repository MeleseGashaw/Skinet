global using Infrastructure.Data;

global using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using Infrastructure.Data.SeedData;
using API.Helper;
using AutoMapper;
//using Microsoft.EntityFrameworkCore.UseSqlServer;
var builder = WebApplication.CreateBuilder(args);
var host = WebApplication.Create(args);
// Add services to the container..
//private readonly IConfiguration _config;
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MappingProfiles));
builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
  
});
 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//--------------------------------------------------------------
var scope= app.Services.CreateScope();
  var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
try{
using (  scope  )
{
    var dataContext = scope.ServiceProvider.GetRequiredService<StoreContext>();
     dataContext.Database.Migrate();

 
  await StoreContextSeed.SeedAsync(  dataContext,loggerFactory);

}
}
  catch(Exception ex)
            {
                    var logger =loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex.Message);
            }
//-----------------------------------------------------------------------

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();

app.Run();
 
 //public static async void Main (string [] args)
 //{
    
 //}
