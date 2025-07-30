using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]

public class MotosController : ControllerBase
{
    private readonly MotoService _service;

    public MotosController(MotoService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    [HttpGet]
    public ActionResult<List<MotoDTO>> Get()
    {
        var motos = _service.GetAll();
        var result = new List<MotoDTO>();

        foreach (var m in motos)
        {
            var dto = new MotoDTO
            {
                Id = m.Id,
                Marca = m.Marca,
                Modello = m.Modello,
                Potenza = m.Potenza,
                Grip = m.Grip,
                Inclinazione = m.Inclinazione,
                VelocitaMassima = m.VelocitaMassima,
                Accelerazione = m.Accelerazione
            };

            result.Add(dto);
        }

        return Ok(result);
    }

    [HttpGet("{id}")]
    public ActionResult<MotoDTO> Get(int id)
    {
        var m = _service.GetById(id);
        if (m is null)
            return NotFound();

        MotoDTO dto = new MotoDTO();
        dto.Id = m.Id;
        dto.Marca = m.Marca;
        dto.Modello = m.Modello;
        dto.Potenza = m.Potenza;
        dto.Grip = m.Grip;
        dto.Inclinazione = m.Inclinazione;
        dto.VelocitaMassima = m.VelocitaMassima;
        dto.Accelerazione = m.Accelerazione;
        
        return dto;
    }

    [HttpPost]
    public ActionResult<Moto> Post([FromBody] Moto moto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        Moto created = _service.Add(moto);
        return CreatedAtAction(
            actionName: nameof(Get),
            routeValues: new { id = created.Id },
            value: created
        );
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Moto moto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        bool success = _service.Update(id, moto);
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