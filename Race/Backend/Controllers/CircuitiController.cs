using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CircuitiController : ControllerBase
{
    private readonly CircuitoService _service;

    public CircuitiController(CircuitoService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    [HttpGet]
    public ActionResult<List<Circuito>> Get()
    {
        List<Circuito> circuiti = _service.GetAll();
        List<CircuitoDTO> result = new List<CircuitoDTO>();

        foreach (Circuito c in circuiti)
        {
            CircuitoDTO dto = new CircuitoDTO
            {
                Id = c.Id,
                Nome = c.Nome,
                Localita = c.Localita,
                Lunghezza = c.Lunghezza,
                Giri = c.Giri,
                Curve = c.Curve
            };

            result.Add(dto);
        }

        return Ok(result);
    }

    [HttpGet("{id}")]
    public ActionResult<CircuitoDTO> Get(int id)
    {
        Circuito circuito = _service.GetById(id);
        if (circuito is null)
            return NotFound();

        CircuitoDTO dto = new CircuitoDTO
        {
            Id = circuito.Id,
            Nome = circuito.Nome,
            Localita = circuito.Localita,
            Lunghezza = circuito.Lunghezza,
            Giri = circuito.Giri,
            Curve = circuito.Curve
        };

        return dto;
    }

    [HttpPost]
    public ActionResult<Circuito> Post([FromBody] Circuito circuito)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Circuito created = _service.Add(circuito);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Circuito circuito)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        bool success = _service.Update(id, circuito);
        if (!success)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        bool success = _service.Delete(id);
        if (!success)
            return NotFound();

        return NoContent();
    }
}
