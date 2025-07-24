using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc; // importa per utilizzare le funzionalità dei controller ASP.NET Core

//il controller si occupa di gestire le richieste HTTP e restituire le risposte
//questo modo specifico di inserire delle direttive tra parentesi quadre sono un attributo che in questo caso permette di definire òa classe come controller api 
[ApiController] //attributo che indica che questa classe è un controller API
[Route("api/[controller]")]// indica il percorso base per le richieste a questo controller

public class PurchasesController : ControllerBase //estendiamo la classe base controller con un controller personalizzato (PurchaseController estende ControllerBase)
//quindi la classe derivata erediterà le proprietà e i comportamenti della classe base
{
    //inizializzo l'oggetto _service nel costruttore della classe
    //cioè quello che scrivo nel costruttore viene eseguito quando creo un istanza dela classe
    private readonly PurchaseService _service;
    private readonly AlbumService _albumService;
    private readonly UserService _userService;
    //private è il modificatore di accesso
    //readonly indica che il campo può essere assegnato solo nel costruttore in pratica è di sola lettura
    //PurchaseService è il tipo del campo che rappresenta il servizio per la gestione dei prodotto
    //_service è il nome del campo, che segue la convenzione di denominazione per o campi privati

    //public PurchasesController() => _service = service;
    public PurchasesController(PurchaseService service, AlbumService albumService, UserService userService) //l'argomento del costruttore è il servizio che gestisce i prodotti in questo caso PurchaseService
    {
        //il throw serve a lanciare un'eccezione se il servizio è null
        //l'operatore di coalescenza restituisce il valore a sinistra se non è null, altrimenti restituisce il valore a destra
        _service = service ?? throw new ArgumentNullException(nameof(service)); //?? è l'operatore di coalescenza
        _albumService = albumService ?? throw new ArgumentNullException(nameof(albumService));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    //metodo per ottenere tutti i prodotti
    [HttpGet] //indica che questo metodo risponde alle richieste HTTP GET
              //ci possono essere vari tipi di richieste http come GET, POST, PUT, DELETE
              //GET indica che il metodo recupera dati dal server passandoli come risposta in formato JSON
              //POST indica che i dati vengono passati in chiaro cioè attraverso il corpo della richiesta del campo url del browser
              //public ActionResult<List<Purchase>> Get() => _service.GetAll();
    /*public List<Purchase> Get() //Metodo accessibile dal programma che restituisce la lista di Purchase che prendiamo dal metodo GetAll di PurchaseService
    {
        return _service.GetAll();
    }*/
    public ActionResult<List<PurchaseDTO>> Get()
    //ActionResult<List<Purchase>> indica che il metodo restituisce un'azione  HTTP con una lista di purchase
    {
        //elenco dei purchases
        List<Purchase> purchases = _service.GetAll();
        //elenco degli utenti
        List<User> users = _userService.GetAll();
        //elenco dei prodotti
        List<Album> albums = _albumService.GetAll();
        //creo la lista di PurchaseDTO
        List<PurchaseDTO> result = new List<PurchaseDTO>();

        //ciclo per ogni acquisto in modo da cercare lo user e il purchase associato
        foreach (Purchase p in purchases)
        {
            
            User user = null;
            Album album = null;
            foreach (User u in users)
            {
                if (u.Id == p.UserId)
                {
                    user = u;
                    break;
                }
            }
            foreach (Album b in albums)
            {
                if (b.Id == p.AlbumId) 
                {
                    album = b;
                    break;
                }
            }
            PurchaseDTO dto = new PurchaseDTO
            {
                Id = p.Id,
                UserName = user != null ? user.NomeUtente : "Sconosciuto",
                AlbumName = album != null ? album.Titolo : "Sconosciuto",
                Quantity = p.Quantity,
            };
            result.Add(dto);
        }
        return Ok(result);//Ok restituisce 200 con i dati specificati 
    }

    [HttpGet("{id}")]
    public ActionResult<PurchaseDTO> Get(int id)
    {
        Purchase p = _service.GetById(id);
        Album a = _albumService.GetById(p.AlbumId);
        User u = _userService.GetById(p.UserId);
        if (p is null)
            return NotFound();
    
        PurchaseDTO dto = new PurchaseDTO
        {
            Id = p.Id,
            UserName = u != null ? u.NomeUtente : "Sconosciuto",
            AlbumName = a != null ? a.Titolo : "Sconosciuto",
            Quantity = p.Quantity
        };
        return dto;
    }

    [HttpPost]
    public ActionResult<Purchase> Post([FromBody] Purchase purchase)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        Purchase created = _service.Add(purchase);
        return CreatedAtAction(
            actionName: nameof(Get),
            routeValues: new { id = created.Id },
            value: created
        );
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Purchase purchase)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        bool success = _service.Update(id, purchase);
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