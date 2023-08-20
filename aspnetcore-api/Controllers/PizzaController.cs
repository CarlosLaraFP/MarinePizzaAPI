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

    // POST action

    // PUT action

    // DELETE action
}