using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Services;
using DTO;
using Microsoft.AspNetCore.Builder;
using Serilog;
using NLog.Web;

var logger = NLogBuilder.ConfigureNLog(".nlog.config").GetCurrentClassLogger();
var builder = WebApplication.CreateBuilder(args);
//logger.Info("app start");

//Log.Logger = new LoggerConfiguration()
//    .MinimumLevel.Information()
//    .WriteTo.File("mylog.txt", rollingInterval: RollingInterval.Day)
//    .CreateLogger();

builder.Host.UseNLog();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICatgoryService, CatgoryService>();;
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

//builder.Services.AddDbContext<_326059268_ShopApiContext>(options =>
//    options.UseSqlServer("Data Source=SRV2\\PUPILS;Initial Catalog=326059268_ShopApi;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=True"));
builder.Services.AddDbContext<_326059268_ShopApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//builder.Services.AddControllers();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "My API V1");
    });
}


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
