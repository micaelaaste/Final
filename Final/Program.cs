
using Final.Data;
using Final.Repositories.Implementations;
using Final.Repositories.Interfaces;
using Final.Services;
using Final.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

namespace Final;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<FinalContext>
        (dbContextOptions => dbContextOptions.UseSqlite(builder.Configuration["ConnectionStrings:FinalDBConnectionString"]));

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();

        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<IProductService, ProductService>();

        builder.Services.AddAuthentication("Bearer")
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,            // ¿lo emitió mi API?
                ValidateAudience = true,          // ¿es para mi API?
                ValidateIssuerSigningKey = true,  // ¿la firma es válida? (nadie lo alteró)
                ValidIssuer = builder.Configuration["Authentication:Issuer"],
                ValidAudience = builder.Configuration["Authentication:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]!))
            };
        });

        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen(setupAction =>
        {
            setupAction.AddSecurityDefinition("ApiBearerAuth", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                Description = "Acá pegar el token generado al loguearse."
            });

            setupAction.AddSecurityRequirement(document => new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference("ApiBearerAuth", document)] = []
            });
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
