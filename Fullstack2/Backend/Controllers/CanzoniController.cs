using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]

public class CanzoniController : ControllerBase
{
    private readonly CanzoneService _service;
    public CanzoniController(CanzoneService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }
    [HttpGet]
    public ActionResult<List<Canzone>> Get()
    {
        List<Canzone> canzones = _service.GetAll();
        return Ok(canzones);
    }
    [HttpGet("{id}")]
    public ActionResult<Canzone> Get(int id)
    {
        var p = _service.GetById(id);
        if (p is null)
            return NotFound();
        return p;
    }
    [HttpPost]
    public ActionResult<Canzone> Post([FromBody] Canzone canzone)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        Canzone created = _service.Add(canzone);
        return CreatedAtAction(
            actionName: nameof(Get),
            routeValues: new { id = created.Id },
            value: created
        );
    }
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Canzone canzone)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        bool success = _service.Update(id, canzone);
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