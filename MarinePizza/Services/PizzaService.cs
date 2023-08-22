using MarinePizza.Interfaces;
using MarinePizza.Models;
using MarinePizza.Data;
using Microsoft.EntityFrameworkCore;

namespace MarinePizza.Services;

// In this specific case we have a scoped service because each client connection has its own state (i.e. nextId).
public class PizzaService : IPizzaService
{
    private readonly PizzaContext _context;

    public PizzaService(PizzaContext context)
    {
        _context = context;
    }

    public IEnumerable<Pizza> GetAll()
    {
        return _context
            .Pizzas
            //.Include(p => p.Toppings)
            //.Include(p => p.Sauce)
            .AsNoTracking() // extension method instructs EFC to disable change tracking. Because this operation is read-only, AsNoTracking can optimize performance.
            .ToList();
    }

    /*
        The Include extension method takes a lambda expression to specify that the Toppings and Sauce navigation properties are to be included in the result by using eager loading. Without this expression, EF Core returns null for those properties.
        The SingleOrDefault method returns a pizza that matches the lambda expression.
        If no records match, null is returned.
        If multiple records match, an exception is thrown.
        The lambda expression describes records where the Id property is equal to the id parameter.
     */

    public Pizza? GetById(int pizzaId)
    {
        return _context
            .Pizzas
            .Include(p => p.Toppings)
            .Include(p => p.Sauce)
            .AsNoTracking()
            .SingleOrDefault(p => p.Id == pizzaId);
    }

    /*
        If you want to ensure that the list's contents cannot be modified outside the class (or at all), you'd need to use a
        collection that is explicitly immutable (like ImmutableList<T> from the System.Collections.Immutable namespace)
        or return a read-only view of the list (like the result of List<T>.AsReadOnly()).
     */
    public Pizza? Create(Pizza newPizza)
    {
        _context.Pizzas.Add(newPizza); // adds the newPizza entity to the EF Core object graph
        _context.SaveChanges(); // method instructs EF Core to persist the object changes to the database.

        // EF Core doesn't do data validation, so any validation must be handled by the ASP.NET Core runtime or user code.
        return newPizza;
    }

    public void UpdateSauce(int pizzaId, int sauceId)
    {
        // Find is an optimized method to query records by their primary key. It searches the local entity graph first before it queries the database.
        var pizza = _context.Pizzas.Find(pizzaId);
        var sauce = _context.Sauces.Find(sauceId);

        if (pizza is null || sauce is null)
        {
            throw new InvalidOperationException("Pizza or Sauce does not exist.");
        }

        // An Update method call is unnecessary because EF Core detects that you set the Sauce property on Pizza.
        pizza.Sauce = sauce;

        _context.SaveChanges();
    }

    public void AddTopping(int pizzaId, int toppingId)
    {
        var pizza = _context.Pizzas.Find(pizzaId);
        var topping = _context.Toppings.Find(toppingId);

        if (pizza is null || topping is null)
        {
            throw new InvalidOperationException("Pizza or Topping does not exist.");
        }

        // The null-coalescing assignment operator checks if the left-hand operand is null. If it is, it assigns the right-hand operand to the left-hand operand.
        pizza.Toppings ??= new List<Topping>();

        pizza.Toppings.Add(topping);

        _context.SaveChanges();
    }

    public void DeleteById(int pizzaId)
    {
        var pizza = _context.Pizzas.Find(pizzaId);

        if (pizza is not null)
        {
            _context.Pizzas.Remove(pizza);

            _context.SaveChanges();
        }
    }
}
