
using Final.Data;
using Final.Repositories.Implementations;
using Final.Repositories.Interfaces;
using Final.Services;
using Final.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Final;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<FinalContext>
        (dbContextOptions => dbContextOptions.UseSqlite(builder.Configuration["ConnectionStrings:FinalDBConnectionString"]));

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();

        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<IProductService, ProductService>();

        builder.Services.AddControllers(); 
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(); 
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
