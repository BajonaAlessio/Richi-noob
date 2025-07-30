using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class GareController : ControllerBase
{
    private readonly GaraService _service;
    private readonly CircuitoService _circuitoService;

    public GareController(GaraService service, CircuitoService circuitoService)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _circuitoService = circuitoService ?? throw new ArgumentNullException(nameof(circuitoService));
    }

    [HttpGet]
    public ActionResult<List<GaraDTO>> Get()
    {
        List<Gara> gare = _service.GetAll();
        List<Circuito> circuiti = _circuitoService.GetAll();
        List<GaraDTO> result = new List<GaraDTO>();

        foreach (Gara gara in gare)
        {
            List<Circuito> circuitiResult = new List<Circuito>();
            foreach (int circuitoId in gara.CircuitiId)
            {
                foreach (Circuito circuito in circuiti)
                {
                    if (circuito.Id == circuitoId)
                    {
                        circuitiResult.Add(circuito);
                    }
                }
            }

            GaraDTO dto = new GaraDTO
            {
                Id = gara.Id,
                Paese = gara.Paese,
                Wheater = gara.Wheater,
                Circuiti = circuitiResult
            };

            result.Add(dto);
        }

        return Ok(result);
    }

    [HttpGet("{id}")]
    public ActionResult<GaraDTO> Get(int id)
    {
        Gara gara = _service.GetById(id);
        if (gara is null)
            return NotFound();

        List<Circuito> circuiti = _circuitoService.GetAll();
        List<Circuito> circuitiResult = new List<Circuito>();

        foreach (int circuitoId in gara.CircuitiId)
        {
            foreach (Circuito circuito in circuiti)
            {
                if (circuito.Id == circuitoId)
                {
                    circuitiResult.Add(circuito);
                }
            }
        }

        GaraDTO dto = new GaraDTO
        {
            Id = gara.Id,
            Paese = gara.Paese,
            Wheater = gara.Wheater,
            Circuiti = circuitiResult
        };

        return dto;
    }

    [HttpPost]
    public ActionResult<Gara> Post([FromBody] Gara gara)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Gara created = _service.Add(gara);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Gara gara)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        bool success = _service.Update(id, gara);
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
