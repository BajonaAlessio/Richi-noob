using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]

public class PilotiController : ControllerBase
{
    private readonly PilotaService _service;
    private readonly SquadraService _squadraService;

    public PilotiController(PilotaService service, SquadraService squadraService)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _squadraService = squadraService ?? throw new ArgumentNullException(nameof(squadraService));
    }

    [HttpGet]
    public ActionResult<List<PilotaDTO>> Get()
    {
        var piloti = _service.GetAll();
        var result = new List<PilotaDTO>();
        foreach (var p in piloti)
        {
            var squadra = _squadraService.GetById(p.SquadraId);
            var dto = new PilotaDTO
            {
                Id = p.Id,
                Nome = p.Nome,
                Nazionalita = p.Nazionalita,
                Eta = p.Eta,
                SquadraNome = (squadra != null) ? squadra.Nome : "Sconosciuto"
            };
                result.Add(dto);
            }
        ;

    return Ok(result);
    }

    [HttpGet("{id}")]
    public ActionResult<PilotaDTO> Get(int id)
    {
        var p = _service.GetById(id);
        if (p is null)
            return NotFound();
        var squadra = _squadraService.GetById(p.SquadraId);    
        var dto = new PilotaDTO
        {
            Id = p.Id,
            Nome = p.Nome,
            Nazionalita = p.Nazionalita,
            Eta = p.Eta,
            SquadraNome = squadra.Nome
        };
        
        return dto;
    }

    [HttpPost]
    public ActionResult<Pilota> Post([FromBody] Pilota pilota)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        Pilota created = _service.Add(pilota);
        return CreatedAtAction(
            actionName: nameof(Get),
            routeValues: new { id = created.Id },
            value: created
        );
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Pilota pilota)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        bool success = _service.Update(id, pilota);
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