using goodsstore_backend.EFCore;
using goodsstore_backend.EFCore.Repositories;
using goodsstore_backend.EFCore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddDbContext<GoodsStoreDbContext>(options =>
{
    string connection = config.GetConnectionString("LocalDb");
    options.UseSqlServer(connection);
});

builder.Services.AddTransient<ICustomersRepository, CustomersRepository>();

//builder.Services.AddControllers();

//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();


app.MapGet("/", (ICustomersRepository customersRepository) =>
{
    //return userRepository.SingleOrDefaultAsync(u => u.Id == 3);
    return "asd";
});

app.Run();
