using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PlayersController : ControllerBase
{
    private readonly PlayerService _service;
    private readonly PilotaService _pilotaService;
    private readonly MotoService _motoService;
    private readonly SquadraService _squadraService;

    public PlayersController(PlayerService service, PilotaService pilotaService, MotoService motoService, SquadraService squadraService)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _pilotaService = pilotaService ?? throw new ArgumentNullException(nameof(pilotaService));
        _motoService = motoService ?? throw new ArgumentNullException(nameof(motoService));
        _squadraService = squadraService ?? throw new ArgumentNullException(nameof(squadraService));
    }

    [HttpGet]
    public ActionResult<List<PlayerDTO>> Get()
    {
        List<Player> players = _service.GetAll();
        List<Pilota> piloti = _pilotaService.GetAll();
        List<Moto> motos = _motoService.GetAll();
        List<Squadra> squadre = _squadraService.GetAll();

        List<PlayerDTO> result = new List<PlayerDTO>();

        foreach (Player player in players)
        {
            Pilota? pilotaResult = null;
            foreach (Pilota pilota in piloti)
            {
                if (pilota.Id == player.PilotaId)
                {
                    pilotaResult = pilota;
                    break;
                }
            }

            Moto? motoResult = null;
            foreach (Moto moto in motos)
            {
                if (moto.Id == player.MotoId)
                {
                    motoResult = moto;
                    break;
                }
            }

            Squadra? squadraResult = null;
            if (pilotaResult != null)
            {
                foreach (Squadra squadra in squadre)
                {
                    if (squadra.Id == pilotaResult.SquadraId)
                    {
                        squadraResult = squadra;
                        break;
                    }
                }
            }

            PlayerDTO dto = new PlayerDTO
            {
                Id = player.Id,
                PilotaNome = pilotaResult != null ? pilotaResult.Nome : "Sconosciuto",
                MotoModello = motoResult != null ? motoResult.Modello : "SCONOSCIUTO",
                SquadraNome = squadraResult != null ? squadraResult.Nome : "SCONOSCIUTO"
            };

            result.Add(dto);
        }

        return Ok(result);
    }

    [HttpGet("{id}")]
    public ActionResult<PlayerDTO> Get(int id)
    {
        Player player = _service.GetById(id);
        if (player is null)
            return NotFound();

        List<Pilota> piloti = _pilotaService.GetAll();
        List<Moto> motos = _motoService.GetAll();
        List<Squadra> squadre = _squadraService.GetAll();

        Pilota? pilotaResult = null;
        foreach (Pilota pilota in piloti)
        {
            if (pilota.Id == player.PilotaId)
            {
                pilotaResult = pilota;
                break;
            }
        }

        Moto? motoResult = null;
        foreach (Moto moto in motos)
        {
            if (moto.Id == player.MotoId)
            {
                motoResult = moto;
                break;
            }
        }

        Squadra? squadraResult = null;
        if (pilotaResult != null)
        {
            foreach (Squadra squadra in squadre)
            {
                if (squadra.Id == pilotaResult.SquadraId)
                {
                    squadraResult = squadra;
                    break;
                }
            }
        }

        PlayerDTO dto = new PlayerDTO
        {
            Id = player.Id,
            PilotaNome = pilotaResult != null ? pilotaResult.Nome : "SCONOSCIUTO",
            MotoModello = motoResult != null ? motoResult.Modello : "SCONOSCIUTO",
            SquadraNome = squadraResult != null ? squadraResult.Nome : "SCONOSCIUTO"
        };

        return dto;
    }

    [HttpPost]
    public ActionResult<Player> Post([FromBody] Player player)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Player created = _service.Add(player);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Player player)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        bool success = _service.Update(id, player);
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
