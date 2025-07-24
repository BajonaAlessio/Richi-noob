using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]

public class UsersController : ControllerBase
{
    private readonly UserService _service;
    public UsersController(UserService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }
    [HttpGet]
    public ActionResult<List<User>> Get()
    {
        List<User> users = _service.GetAll();
        return Ok(users);
    }
    [HttpGet("{id}")]
    public ActionResult<User> Get(int id)
    {
        var p = _service.GetById(id);
        if (p is null)
            return NotFound();
        return p;
    }
    [HttpPost]
    public ActionResult<User> Post([FromBody] User user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        User created = _service.Add(user);
        return CreatedAtAction(
            actionName: nameof(Get),
            routeValues: new { id = created.Id },
            value: created
        );
    }
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] User user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        bool success = _service.Update(id, user);
        if (success == false)
            return NotFound();
        else
            return NoContent();
    }
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        bool success = _service.Delete(id);
        if (success == false)
            return NotFound();
        else
            return NoContent();
    }
}