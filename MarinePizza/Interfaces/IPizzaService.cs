using System;
using MarinePizza.Models;

namespace MarinePizza.Interfaces;

public interface IPizzaService
{
    IEnumerable<Pizza> GetAll();
    Pizza? GetById(int pizzaId);
    Pizza? Create(Pizza newPizza);
    void AddTopping(int pizzaId, int toppingId);
    void UpdateSauce(int pizzaId, int sauceId);
    void DeleteById(int pizzaId);
}

/*
    public: Access is not restricted.
    protected: Access is limited to the containing class and types derived from the containing class.

    When designing classes:

    Use public for members that need to be accessed from outside the class and its derived classes. This is your public API.
    Use protected for members that should be accessible to derived classes but not outside of the inheritance hierarchy.
    Additionally, always consider the principle of least privilege: expose only what's necessary and hide internal details to make your classes more maintainable and reduce potential errors.

    In C#, access modifiers are not allowed on interface members. By default, all members of an interface are public.
 */