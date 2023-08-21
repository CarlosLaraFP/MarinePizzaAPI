using System.ComponentModel.DataAnnotations;

namespace MarinePizza.Models;

public class Pizza
{
	public int Id { get; set; }

	[Required]
	[MaxLength(10)]
	public string? Name { get; set; }

	public Sauce? Sauce { get; set; }

	public ICollection<Topping>? Toppings { get; set; }

    public bool IsGlutenFree { get; set; }
}

/*
	dotnet ef migrations add ModelRevisions --context PizzaContext

	This message appears: An operation was scaffolded that may result in the loss of data.
	Please review the migration for accuracy. The message appears because you changed the relationship
	from Pizza to Topping from one-to-many to many-to-many, which requires that an existing foreign key
	column is dropped. Because you don't yet have any data in your database, this change isn't problematic.
	However, in general, it's a good idea to check the generated migration when this warning appears to
	make sure that no data is deleted or truncated by the migration.

	dotnet ef database update --context PizzaContext
 */

/*
	If you only need to iterate over a sequence of items, you should use IEnumerable<T>.
	If you need more capabilities like adding/removing items, checking the count, or checking for the
	existence of an item, then you should consider using ICollection<T> or another more specialized interface.
	It's worth noting that there are other specialized interfaces like IList<T>, IDictionary<TKey, TValue>,
	and more, which provide even more capabilities specific to certain types of collections.
 */