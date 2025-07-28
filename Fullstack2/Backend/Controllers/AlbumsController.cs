using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]

public class AlbumsController : ControllerBase
{
    private readonly AlbumService _service;
    private readonly CanzoneService _canzoneService;
    public AlbumsController(AlbumService service, CanzoneService canzoneService)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _canzoneService = canzoneService ?? throw new ArgumentNullException(nameof(canzoneService));
    }
    [HttpGet]
    public ActionResult<List<AlbumDTO>> Get()
    {
        List<Album> albums = _service.GetAll();
        List<Canzone> allCanzoni = _canzoneService.GetAll();
        List<AlbumDTO> result = new List<AlbumDTO>();
        foreach (Album album in albums)
        {
            List<Canzone> canzoniResult = new List<Canzone>();
            foreach (int idCanzoneAlbum in album.CanzoniId)
            {
                foreach (Canzone canzone in allCanzoni)
                {
                    if (canzone.Id == idCanzoneAlbum)
                    {
                        canzoniResult.Add(canzone);
                    }
                }
            }
            AlbumDTO dto = new AlbumDTO
            {
                Id = album.Id,
                Titolo = album.Titolo,
                Anno = album.Anno,
                Autore = album.Autore,
                Canzoni = canzoniResult,
                Genere = album.Genere,
                Ascoltato = album.Ascoltato
            };
            result.Add(dto);
        }
        return Ok(result);
    }
    [HttpGet("{id}")]
    public ActionResult<AlbumDTO> Get(int id)
    {
        Album album = _service.GetById(id);
        if (album is null)
            return NotFound();

        List<Canzone> allCanzoni = _canzoneService.GetAll();
        List<Canzone> canzoniResult = new List<Canzone>();
        foreach (int idCanzoneAlbum in album.CanzoniId)
        {
            foreach (Canzone canzone in allCanzoni)
            {
                if (canzone.Id == idCanzoneAlbum)
                {
                    canzoniResult.Add(canzone);
                }
            }
        }
        AlbumDTO result = new AlbumDTO
        {
            Id = album.Id,
            Titolo = album.Titolo,
            Anno = album.Anno,
            Autore = album.Autore,
            Canzoni = canzoniResult,
            Genere = album.Genere,
            Ascoltato = album.Ascoltato
        };

        return result;
    }
    [HttpPost]
    public ActionResult<Album> Post([FromBody] Album album)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        Album created = _service.Add(album);
        return CreatedAtAction(
            actionName: nameof(Get),
            routeValues: new { id = created.Id },
            value: created
        );
    }
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Album album)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        bool success = _service.Update(id, album);
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