using aspnetcore_api.Models;

namespace aspnetcore_api.Services;

public static class PizzaService
{
    // The absence of an explicit access modifier = private implicitly
    static List<Pizza> Pizzas { get; }
    static int nextId = 3;
    static PizzaService()
    {
        Pizzas = new List<Pizza>
        {
            new Pizza { Id = 1, Name = "Classic Italian", IsGlutenFree = false },
            new Pizza { Id = 2, Name = "Veggie", IsGlutenFree = true }
        };
    }

    public static List<Pizza> GetAll() => Pizzas;

    public static Pizza? Get(int id) => Pizzas.FirstOrDefault(p => p.Id == id);

    /*
        If you want to ensure that the list's contents cannot be modified outside the class (or at all), you'd need to use a
        collection that is explicitly immutable (like ImmutableList<T> from the System.Collections.Immutable namespace)
        or return a read-only view of the list (like the result of List<T>.AsReadOnly()).
     */
    public static void Add(Pizza pizza)
    {
        pizza.Id = nextId++;
        Pizzas.Add(pizza);
    }

    public static void Delete(int id)
    {
        var pizza = Get(id);
        if (pizza is null)
            return;

        Pizzas.Remove(pizza);
    }

    public static void Update(Pizza pizza)
    {
        var index = Pizzas.FindIndex(p => p.Id == pizza.Id);
        if (index == -1)
            return;

        Pizzas[index] = pizza;
    }
}
