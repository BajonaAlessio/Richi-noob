using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]

public class SquadreController : ControllerBase
{
    private readonly SquadraService _service;
    private readonly MotoService _motoService;
    public SquadreController(SquadraService service, MotoService motoService)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _motoService = motoService ?? throw new ArgumentNullException(nameof(motoService));
    }
    [HttpGet]
    public ActionResult<List<SquadraDTO>> Get()
    {
        List<Squadra> squadre = _service.GetAll();
        List<Moto> allMotos = _motoService.GetAll();
        List<SquadraDTO> result = new List<SquadraDTO>();
        foreach (Squadra squadra in squadre)
        {
            List<Moto> motosResult = new List<Moto>();
            foreach (int idMotoSquadra in squadra.MotosId)
            {
                foreach (Moto moto in allMotos)
                {
                    if (moto.Id == idMotoSquadra)
                    {
                        motosResult.Add(moto);
                    }
                }
            }
            SquadraDTO dto = new SquadraDTO
            {
                Id = squadra.Id,
                Nome = squadra.Nome,
                Paese = squadra.Paese,
                AnnoFondazione = squadra.AnnoFondazione,
                Proprietario = squadra.Proprietario,
                Motos = motosResult
            };
            result.Add(dto);
        }
        return Ok(result);
    }
    [HttpGet("{id}")]
    public ActionResult<SquadraDTO> Get(int id)
    {
        Squadra squadra = _service.GetById(id);
        if (squadra is null)
            return NotFound();

        List<Moto> allMotos = _motoService.GetAll();
        List<Moto> motosResult = new List<Moto>();
        foreach (int idMotoSquadra in squadra.MotosId)
        {
            foreach (Moto moto in allMotos)
            {
                if (moto.Id == idMotoSquadra)
                {
                    motosResult.Add(moto);
                }
            }
        }
        SquadraDTO result = new SquadraDTO
        {
            Id = squadra.Id,
                Nome = squadra.Nome,
                Paese = squadra.Paese,
                AnnoFondazione = squadra.AnnoFondazione,
                Proprietario = squadra.Proprietario,
                Motos = motosResult
        };

        return result;
    }
    [HttpPost]
    public ActionResult<Squadra> Post([FromBody] Squadra squadra)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        Squadra created = _service.Add(squadra);
        return CreatedAtAction(
            actionName: nameof(Get),
            routeValues: new { id = created.Id },
            value: created
        );
    }
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Squadra squadra)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        bool success = _service.Update(id, squadra);
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