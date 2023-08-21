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