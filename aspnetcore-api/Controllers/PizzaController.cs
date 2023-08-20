using System.Reflection.Emit;
using aspnetcore_api.Models;
using aspnetcore_api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
namespace aspnetcore_api.Controllers;

// TODO: Dependency injection and global singletons like ZIO

// The controller-level [Route] attribute defines the /pizza pattern.
[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    public PizzaController()
    {
    }

    // The ActionResult type is the base class for all action results in ASP.NET Core
    // GET all action (responds only to the HTTP GET verb)
    [HttpGet]
    public ActionResult<IEnumerable<Pizza>> GetAll() => PizzaService.GetAll();
    // Queries the database (via the service [interface]) for all pizza and automatically returns data with a Content-Type value of application/json

    // Requires that the id parameter's value is included in the URL segment after pizza/
    // GET by Id action
    [HttpGet("{id}")]
    public ActionResult<Pizza> Get(int id)
    {
        var pizza = PizzaService.Get(id);

        if (pizza is null) return NotFound();

        return pizza;
    }
    // Queries the database (via the service [interdace]) for a pizza that matches the provided id parameter.

    // IActionResult lets the client know if the request succeeded and provides the ID of the newly created pizza.
    // POST action: CreatedAtAction or BadRequest
    [HttpPost]
    public IActionResult Create(Pizza pizza)
    {
        PizzaService.Add(pizza);
        // nameof(Get) because the response shows how to access the newly created object via full URL
        return CreatedAtAction(nameof(Get), new { id = pizza.Id }, pizza);
    }

    // Returns IActionResult, because the ActionResult return type isn't known until runtime.
    // The BadRequest, NotFound, and NoContent methods return BadRequestResult, NotFoundResult, and NoContentResult types, respectively.
    // PUT action: NoContent (successfully updated) or BadRequest (either parameter)
    [HttpPut("{id}")]
    public IActionResult Update(int id, Pizza newPizza)
    {
        if (id != newPizza.Id) return BadRequest();

        var currentPizza = PizzaService.Get(id);

        if (currentPizza is null) return NotFound();

        PizzaService.Update(newPizza);

        return NoContent();
    }

    // DELETE action: NoContent (successfully deleted) or NotFound  
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var pizza = PizzaService.Get(id);

        if (pizza is null) return NotFound();

        PizzaService.Delete(id);

        return NoContent();
    }
}