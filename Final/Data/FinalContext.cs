using Final.Entities;
using Microsoft.EntityFrameworkCore;

namespace Final.Data;

public class FinalContext: DbContext
{
    public FinalContext(DbContextOptions<FinalContext> options) : base(options) //Acá estamos llamando al constructor de DbContext que es el que acepta las opciones
    {

    }
    public DbSet<User> Users { get; set; }// Esto le va a decir a Entity Framework que va a tener una tabla que guarda registros de esa entidad
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

}
