using MarinePizza.Models;

namespace MarinePizza.Services;

public class PizzaService
{
    // The absence of an explicit access modifier = private implicitly
    private List<Pizza> Pizzas { get; }

    private int nextId = 3;

    public PizzaService()
    {
        Pizzas = new List<Pizza>
        {
            new Pizza { Id = 1, Name = "Classic Italian", IsGlutenFree = false },
            new Pizza { Id = 2, Name = "Veggie", IsGlutenFree = true }
        };
    }

    public IEnumerable<Pizza> GetAll() => Pizzas;

    public Pizza? GetById(int id) => Pizzas.FirstOrDefault(p => p.Id == id);

    /*
        If you want to ensure that the list's contents cannot be modified outside the class (or at all), you'd need to use a
        collection that is explicitly immutable (like ImmutableList<T> from the System.Collections.Immutable namespace)
        or return a read-only view of the list (like the result of List<T>.AsReadOnly()).
     */
    public Pizza? Create(Pizza newPizza)
    {
        newPizza.Id = nextId++;
        Pizzas.Add(newPizza);
        return newPizza;
    }

    public void AddTopping(int PizzaId, int ToppingId)
    {
        throw new NotImplementedException();
    }

    public void UpdateSauce(int PizzaId, int SauceId)
    {
        throw new NotImplementedException();
    }

    public void DeleteById(int id)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        var pizza = GetById(id);

        if (pizza is null) return;

        Pizzas.Remove(pizza);
    }

    public void Update(Pizza pizza)
    {
        var index = Pizzas.FindIndex(p => p.Id == pizza.Id);

        if (index == -1) return;

        Pizzas[index] = pizza;
    }
}
