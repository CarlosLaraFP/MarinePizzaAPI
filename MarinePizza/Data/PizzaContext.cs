using Microsoft.EntityFrameworkCore;

using MarinePizza.Models;

namespace MarinePizza.Data;

// DbContext is a gateway through which you can interact with the database.
// When instantiated, PizzaContext will expose Pizzas, Toppings, and Sauces properties.
// Changes you make to the collections that are exposed by those properties will be propagated to the database.
public class PizzaContext : DbContext
{
    // The constructor allows external code to pass in the configuration so that the same DbContext
    // can be shared between test and production code, and even be used with different providers.
    public PizzaContext(DbContextOptions<PizzaContext> options) : base(options)
    {
    }

    // DbSet<T> properties correspond to tables to create in the database.
    public DbSet<Pizza> Pizzas => Set<Pizza>();
    public DbSet<Topping> Toppings => Set<Topping>();
    public DbSet<Sauce> Sauces => Set<Sauce>();
}

/*
    Tables that correspond to each entity were created.
    Table names were taken from the names of the DbSet properties on the PizzaContext.
    Properties named Id were inferred to be autoincrementing primary key fields.
    The EF Core primary key and foreign key constraint naming conventions are PK_<primary key property> and
    FK_<dependent entity>_<principal entity>_<foreign key property>, respectively. The <dependent entity>
    and <principal entity> placeholders correspond to the entity class names.
 */

/*
    The method AddSqlite<TContext>() is an extension method provided by the Entity Framework Core (EF Core) library to configure the use of SQLite as the database provider for a given DbContext (PizzaContext in this case).

    When you use AddSqlite<TContext>() or similar methods for other database providers like AddSqlServer<TContext>(), AddNpgsql<TContext>(), etc., EF Core sets up the DbContext (PizzaContext in your example) with a scoped lifetime by default.

    This means that for each HTTP request in an ASP.NET Core application, a new instance of the PizzaContext is created and is reused wherever that context is required within the same request. Once the request is completed, the instance is disposed of.

    In general, a scoped lifetime is appropriate for a DbContext because:

    It ensures that the same context instance is used throughout a single request, providing a consistent view of the database within that request.
    It helps in managing resources efficiently, especially with database connections which are opened and closed with the context's lifetime.
    It avoids potential concurrency issues that could arise if a singleton context were used across multiple requests.
 */