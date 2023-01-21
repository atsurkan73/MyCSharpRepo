using WebApiApplication;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApiApplication.WebService;
using WebApiApplication.DAO;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Net.Mime.MediaTypeNames;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddDbContext<NewDbContext>(options => options.UseSqlServer(Settings.ConnectionString));

builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
SetMappings(app).Run();

app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 400)
    {
        context.Request.Path = "/";

        await next();
    }
});

app.UseAuthorization();

app.UseRouting();

app.MapControllers();

app.UseDeveloperExceptionPage();

static WebApplication SetMappings(WebApplication application)
{
    application.MapGet("/category", async (IProductRepository productRep) => JsonSerializer.Serialize(await productRep.GetCategoryAsync(),
        Settings.SerializerOptions))
    .WithName("GetCategory")
    .WithOpenApi();

    application.MapPost("/addProduct", async (Product product, IProductRepository productRepo) =>
        {
            await productRepo.AddProductAsync(product);
           
            return Results.Created($"/products/{product.ProductName}", product);
        });

    application.MapDelete("deleteProduct", async (int id, IProductRepository productRepo) =>
        Results.Ok(await productRepo.DeleteProductAsync(id)));
    return application;

    application.MapControllerRoute(
    name: "default",
    pattern: "{controller=ProductController}/{action=Index}/{id?}");
}






