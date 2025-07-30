using Backend.Models;
using Backend.Services;
using Backend.Utils;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class RacesController : ControllerBase
{
    private readonly RaceService _service;
    private readonly PlayerService _playerService;
    private readonly PilotaService _pilotaService;
    private readonly MotoService _motoService;
    private readonly SquadraService _squadraService;
    private readonly GaraService _garaService;
    private readonly CircuitoService _circuitoService;

    public RacesController(
        RaceService service,
        PlayerService playerService,
        PilotaService pilotaService,
        MotoService motoService,
        SquadraService squadraService,
        GaraService garaService,
        CircuitoService circuitoService)
    {
        _service = service;
        _playerService = playerService;
        _pilotaService = pilotaService;
        _motoService = motoService;
        _squadraService = squadraService;
        _garaService = garaService;
        _circuitoService = circuitoService;
    }

    [HttpGet]
    public ActionResult<List<RaceDTO>> Get()
    {
        List<Race> races = _service.GetAll();
        List<Player> players = _playerService.GetAll();
        List<Pilota> piloti = _pilotaService.GetAll();
        List<Moto> motos = _motoService.GetAll();
        List<Squadra> squadre = _squadraService.GetAll();
        List<Gara> gare = _garaService.GetAll();
        List<Circuito> circuiti = _circuitoService.GetAll();

        List<RaceDTO> result = new List<RaceDTO>();

        foreach (Race race in races)
        {
            Player playerResult = null;
            foreach (Player p in players)
            {
                if (p.Id == race.PlayerId)
                {
                    playerResult = p;
                    break;
                }
            }

            Pilota pilotaResult = null;
            if (playerResult != null)
            {
                foreach (Pilota pilota in piloti)
                {
                    if (pilota.Id == playerResult.PilotaId)
                    {
                        pilotaResult = pilota;
                        break;
                    }
                }
            }

            Moto motoResult = null;
            if (playerResult != null)
            {
                foreach (Moto moto in motos)
                {
                    if (moto.Id == playerResult.MotoId)
                    {
                        motoResult = moto;
                        break;
                    }
                }
            }

            Squadra squadraResult = null;
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

            Gara garaResult = null;
            foreach (Gara gara in gare)
            {
                if (gara.Id == race.GaraId)
                {
                    garaResult = gara;
                    break;
                }
            }

            Circuito circuitoResult = null;
            if (garaResult != null && garaResult.CircuitiId != null && garaResult.CircuitiId.Count > 0)
            {
                List<Circuito> allCircuiti = _circuitoService.GetAll();
                foreach (int idCircuito in garaResult.CircuitiId)
                {
                    foreach (Circuito c in allCircuiti)
                    {
                        if (c.Id == idCircuito)
                        {
                            circuitoResult = c;
                            break;
                        }
                    }
                    if (circuitoResult != null) break; 
                }
            }

            RaceDTO dto = new RaceDTO
            {
                Id = race.Id,
                PlayerNome = pilotaResult != null ? pilotaResult.Nome : "N/A",
                SquadraNome = squadraResult != null ? squadraResult.Nome : "N/A",
                MotoModello = motoResult != null ? motoResult.Modello : "N/A",
                GaraNome = garaResult != null ? garaResult.Paese : "N/A",
                CircuitoNome = circuitoResult != null ? circuitoResult.Nome : "N/A",
                TempoFinale = race.TempoFinale,
                Posizione = race.Posizione
            };

            result.Add(dto);
        }

        return Ok(result);
    }

    [HttpGet("{id}")]
    public ActionResult<RaceDTO> Get(int id)
    {
        Race race = _service.GetById(id);
        if (race is null)
            return NotFound();

        Player playerResult = _playerService.GetById(race.PlayerId);
        Pilota pilotaResult = playerResult != null ? _pilotaService.GetById(playerResult.PilotaId) : null;
        Moto motoResult = playerResult != null ? _motoService.GetById(playerResult.MotoId) : null;
        Squadra squadraResult = pilotaResult != null ? _squadraService.GetById(pilotaResult.SquadraId) : null;
        Gara garaResult = _garaService.GetById(race.GaraId);

        Circuito circuitoResult = null;
        if (garaResult != null && garaResult.CircuitiId != null && garaResult.CircuitiId.Count > 0)
        {
            List<Circuito> allCircuiti = _circuitoService.GetAll();
            foreach (int idCircuito in garaResult.CircuitiId)
            {
                foreach (Circuito c in allCircuiti)
                {
                    if (c.Id == idCircuito)
                    {
                        circuitoResult = c;
                        break;
                    }
                }
            }
        }

        RaceDTO dto = new RaceDTO
        {
            Id = race.Id,
            PlayerNome = pilotaResult != null ? pilotaResult.Nome : "Sconosciuto",
            SquadraNome = squadraResult != null ? squadraResult.Nome : "Sconosciuto",
            MotoModello = motoResult != null ? motoResult.Modello : "Sconosciuto",
            GaraNome = garaResult != null ? garaResult.Paese : "Sconosciuto",
            CircuitoNome = circuitoResult != null ? circuitoResult.Nome : "Sconosciuto",
            TempoFinale = race.TempoFinale,
            Posizione = race.Posizione
        };

        return dto;
    }

    [HttpPost]
    public ActionResult<List<Race>> Post([FromBody] List<Race> races)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    List<Race> createdRaces = new List<Race>();

    foreach (var race in races)
    {
        var player = _playerService.GetById(race.PlayerId);
        var moto = _motoService.GetById(player.MotoId);
        var gara = _garaService.GetById(race.GaraId);

        Circuito circuito = null;
        if (gara != null && gara.CircuitiId != null && gara.CircuitiId.Count > 0)
        {
            circuito = _circuitoService.GetById(gara.CircuitiId[0]);
        }

        race.TempoFinale = RaceCalculator.CalcolaTempo(moto, circuito, circuito.Curve);

        createdRaces.Add(_service.Add(race));
    }

    for (int i = 0; i < createdRaces.Count - 1; i++)
    {
        for (int j = i + 1; j < createdRaces.Count; j++)
        {
            if (createdRaces[j].TempoFinale < createdRaces[i].TempoFinale)
            {
                Race temp = createdRaces[i];
                createdRaces[i] = createdRaces[j];
                createdRaces[j] = temp;
            }
        }
    }

    int posizione = 1;
    foreach (var race in createdRaces)
    {
        race.Posizione = posizione;
        _service.Update(race.Id, race);
        posizione++;
    }

    return Ok(createdRaces);
}

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Race race)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        bool success = _service.Update(id, race);
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
