using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]

public class AscoltiController : ControllerBase
{
    private readonly AscoltoService _service;
    private readonly UserService _userService;
    private readonly CanzoneService _canzoneService;
    public AscoltiController(AscoltoService service, UserService userService, CanzoneService canzoneService)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _canzoneService = canzoneService ?? throw new ArgumentNullException(nameof(canzoneService));
    }
    [HttpGet]
    public ActionResult<List<AscoltoDTO>> Get()
    {
        List<Ascolto> ascolti = _service.GetAll();
        List<User> users = _userService.GetAll();
        List<Canzone> canzoni = _canzoneService.GetAll();
        List<AscoltoDTO> result = new List<AscoltoDTO>();
        foreach (Ascolto ascolto in ascolti)
        {
            User user = null;
            Canzone canzone = null;
            foreach (User u in users)
            {
                if (u.Id == ascolto.UtenteId)
                {
                    user = u;
                    break;
                }
            }
            foreach (Canzone c in canzoni)
            {
                if (c.Id == ascolto.CanzoneId)
                {
                    canzone = c;
                    break;
                }
            }
            AscoltoDTO dto = new AscoltoDTO
            {
                Id = ascolto.Id,
                Utente = user != null ? user.NomeUtente : "Sconosciuto",
                Canzone = canzone != null ? canzone.Nome : "Sconosciuto"
            };
            result.Add(dto);
        }
        return Ok(result);
    }

    [HttpGet("{id}")]
    public ActionResult<AscoltoDTO> Get(int id)
    {
        Ascolto a = _service.GetById(id);
        User u = _userService.GetById(id);
        Canzone c = _canzoneService.GetById(id);
        if (a is null)
            return NotFound();

        AscoltoDTO dto = new AscoltoDTO
        {
            Id = a.Id,
            Utente = u != null ? u.NomeUtente : "Sconosciuto",
            Canzone = c != null ? c.Nome : "Sconosciuto"
        };
        return dto;
    }

    [HttpPost]
    public ActionResult<Ascolto> Post([FromBody] Ascolto ascolto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        Ascolto created = _service.Add(ascolto);
        return CreatedAtAction(
            actionName: nameof(Get),
            routeValues: new { id = created.Id },
            value: created
        );
    }
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Ascolto ascolto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        bool success = _service.Update(id, ascolto);
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