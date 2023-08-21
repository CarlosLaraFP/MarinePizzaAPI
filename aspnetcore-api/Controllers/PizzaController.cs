using aspnetcore_api.Models;
using aspnetcore_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace aspnetcore_api.Controllers;

// TODO: Dependency injection and global singletons like ZIO

// The controller-level [Route] attribute defines the /pizza pattern.
[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    PizzaService _service;

    // Dependency injection
    public PizzaController(PizzaService service)
    {
        _service = service;
    }

    // The ActionResult type is the base class for all action results in ASP.NET Core
    // GET all action (responds only to the HTTP GET verb)
    [HttpGet]
    public IEnumerable<Pizza> GetAll() => _service.GetAll();
    // Queries the database (via the service [interface]) for all pizza and automatically returns data with a Content-Type value of application/json

    // Requires that the id parameter's value is included in the URL segment after pizza/
    // GET by Id action
    [HttpGet("{id}")]
    public ActionResult<Pizza> GetById(int id)
    {
        var pizza = _service.GetById(id);

        return (pizza is not null) ? pizza : NotFound();
    }
    // Queries the database (via the service [interdace]) for a pizza that matches the provided id parameter.

    // IActionResult lets the client know if the request succeeded and provides the ID of the newly created pizza.
    // POST action: CreatedAtAction or BadRequest
    [HttpPost]
    public IActionResult Create(Pizza newPizza)
    {
        var pizza = _service.Create(newPizza);
        // nameof(Get) because the response shows how to access the newly created object via full URL
        // If you're sure that the reference is not null in a specific context, you can use the null-forgiving postfix ! to tell the compiler to skip the null warning.
        return CreatedAtAction(nameof(GetById), new { id = pizza!.Id }, pizza);
    }

    [HttpPut("{id}/addtopping")]
    public IActionResult AddTopping(int id, int toppingId)
    {
        var pizzaToUpdate = _service.GetById(id);

        if (pizzaToUpdate is not null)
        {
            _service.AddTopping(id, toppingId);
            return NoContent();
        }

        return NotFound();
    }

    [HttpPut("{id}/updatesauce")]
    public IActionResult UpdateSauce(int id, int sauceId)
    {
        var pizzaToUpdate = _service.GetById(id);

        if (pizzaToUpdate is not null)
        {
            _service.UpdateSauce(id, sauceId);
            return NoContent();
        }

        return NotFound();
    }

    // Returns IActionResult because the ActionResult return type isn't known until runtime.
    // The NotFound and NoContent methods return NotFoundResult and NoContentResult types, respectively.
    // DELETE action: NoContent (successfully deleted) or NotFound  
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var pizza = _service.GetById(id);

        if (pizza is not null)
        {
            _service.DeleteById(id);
            return Ok();
        }

        return NotFound();
    }

    // Returns IActionResult, because the ActionResult return type isn't known until runtime.
    // The BadRequest, NotFound, and NoContent methods return BadRequestResult, NotFoundResult, and NoContentResult types, respectively.
    // PUT action: NoContent (successfully updated) or BadRequest (either parameter)
    [HttpPut("{id}")]
    public IActionResult Update(int id, Pizza newPizza)
    {
        if (id != newPizza.Id) return BadRequest();

        var currentPizza = _service.GetById(id);

        if (currentPizza is null) return NotFound();

        _service.Update(newPizza);

        return NoContent();
    }
}