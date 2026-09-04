using Microsoft.EntityFrameworkCore;
using OrderEntrySystem.Api.Repositories;
using OrderEntrySystem.Core;
using OrderEntrySystem.Core.Interfaces;
using OrderEntrySystem.Data.DataAccess;
using OrderEntrySystem.Data.Repositories;
using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddDbContext<OESContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

builder.Services.AddOpenApi();

builder.Services.AddScoped<IProductRepository, EfProductRepository>();

builder.Services.AddScoped<ICategoryRepository, EfCategoryRepository>();

builder.Services.AddScoped<ICustomerRepository, EfCustomerRepository>();

builder.Services.AddScoped<ILocationRepository, EfLocationRepository>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
