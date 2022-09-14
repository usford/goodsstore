using goodsstore_backend.Models;
using goodsstore_backend.EFCore;
using goodsstore_backend.EFCore.Repositories;
using goodsstore_backend.EFCore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

Console.OutputEncoding = System.Text.Encoding.UTF8;
CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CurrentCulture;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddDbContext<IGoodsStoreDbContext, GoodsStoreDbContext>(options =>
{
    string connection = config.GetConnectionString("LocalDb");
    options.UseSqlServer(connection);
});

builder.Services.AddTransient<ICustomersRepository, CustomersRepository>();
builder.Services.AddTransient<IOrdersRepository, OrdersRepository>();
builder.Services.AddTransient<IItemsRepository, ItemsRepository>();
builder.Services.AddTransient<IOrdersElementsRepository, OrdersElementsRepository>();

builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Customers}/{action=Index}/{id?}");

//app.UseAuthorization();


app.Run();
