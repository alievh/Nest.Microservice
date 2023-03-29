using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;
using Nest.Services.CatalogApi.DTO_s.Category;
using Nest.Services.CatalogApi.Services.Implementations;
using Nest.Services.CatalogApi.Services.Interfaces;
using Nest.Services.CatalogApi.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// IdentityServer4 configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServerURL"];
    options.Audience = "resource_catalog";
    options.RequireHttpsMetadata = false;
});

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AuthorizeFilter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// DatabaseSettings Class
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.AddSingleton<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});

// Dependency Injection
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();


// Seed data for Category
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var categoryService = serviceProvider.GetRequiredService<ICategoryService>();

    if (!categoryService.GetAllAsync().Result.Data.Any())
    {
        categoryService.CreateAsync(new CategoryDto { Name = "Milks and Dairies" }).Wait();
        categoryService.CreateAsync(new CategoryDto { Name = "Wines and Drinks" }).Wait();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
