# Fullstack v2
in questa versione l'obbiettivo è implementare un servizio che prenda i contenuti da un file Json invece che da una lista interna
(attualmenete i dati vengono presi da product service quindi ogni volta che lanciamo l'applicazione le modifiche vengono resettati)
il file Json che contiene i dati è organizzato come una lista di oggetti (simili a una lista di dizionari)
il file json verrà deserializzato usando una classe album con le seguenti proprietà:
- Id (int, generato automaticamente)
- titolo (string)
- anno (int)
- autore (string)
- Canzoni (List`<string>`)
- genere (string)
- ascoltato (bool)

in questa verione non è necessario avere una gestione dell'input dell'utente dato che lo scopo è quello di servire i dati al frontend tramite un servizio http

l'applicazione deve essere in grado di generare un id progressivo basandosi sugli album inseriti sul file json (ultimo id + 1)

in servizio dovrà implementare i seguenti endpoint:
- Get /albums
- Get /albums/{id}
- POST /albums
- PUT /albums/{id}
- DELETE /albums/{id}
devono essere implementati i comandi principali per testare il servizio
- POST
- GET
- PUT
- DELETE
esempio di comandi CURL
curl -X POST http://localhost:5000/albums \
-H "Content-Type: application/json \
-d '{
    "id": 1,
    "titolo":
}

# Validazione alternativa con Decorators
si possono validare i dati di input utilizzando il pattern Decorator
i decoratori sono integrati in .NET
si possono usare così:
```csharp
using System.ComponentModel.DataAnnotations;
```
esempio di decorator di dato obbligatorio
```csharp
[Required]
public string Name { get; set; }
```
si può aggiungere un messaggio da trasmettere all'utente
```csharp
[Required(ErrorMessage = "il nome della categoria è obbligatorio")]
public string Name { get; set; }
```
## Decorators piu usati
Nome | Descrizione
--- | ---
[required] | Campo obbligatorio
[StringLength] | Lunghezza massima di una stringa
[MaxLength]/[MinLength] | Lunghezza massima/minima di una collezione
[Range] | Valore numerico compreso in un intervallo
[EmailAddress] | Verifica che una stringa sia un indirizzo email valido
[Url] | Verifica che una stringa sia un URL valido
[CreditCard] | verifica che una stringa sia una carta di credito valida
[DataType] | specifica il tipo di dato
[Compare] | confronta due proprietà
[Phone] | verifica che una stringa sia un numero telefonico
[Key] | indica che una proprietà è una chiave primaria
[RegularExpression] | Verifica che una stringa rispetti un espressione specifica

## Aggiungi i decoratori nei controller
```csharp
if (!ModelState.IsValid)
{
    return BadRequest(ModelState);
}
```


# Versione completa

## Program cs

Creiamo l'app con le varie impostazioni ed inezioni di classi da lanciare:
1) Creiamo il builder in cui andranno inseriti le varie impostazioni
2) Aggiungio i controller responsabili della gestione delle richieste HTTP
3) Inietto la classe AlbumService
4) Configura CORS che è una politica di sicurezza che permette o blocca le richieste tra domini diversi
5) Costruisco l'applicazione
6) Aggiungo le varie impostazioni
7) Lancio l'app

```csharp
var builder = WebApplication.CreateBuilder(args); 

builder.Services.AddControllers();

builder.Services.AddSingleton<AlbumService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .AllowAnyOrigin() 
            .AllowAnyHeader() 
            .AllowAnyMethod(); 
    });
});

var app = builder.Build(); 
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); 
}

app.UseHttpsRedirection(); 

app.UseCors(); 

app.MapControllers();

app.Run();

```

## Classe Album

Classe che contine i vari campi di un album 

```csharp
public class Album
{
    public int Id { get; set; }
    public string Titolo { get; set; }
    public int Anno { get; set; }
    public string Autore { get; set; }
    public List<Canzone> Canzoni { get; set; }
    public string Genere { get; set; }
    public bool Ascoltato { get; set; }

}
```

## Classe Canzone

Classe che contine i vari campi di una canzone

```csharp
public class Canzone
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Durata { get; set; }
}
```


## AlbumService

Classe che contiene tutti i servizi che vogliamo siano disponibili per gli oggetti album:

La classe mette a disposizione una lista di album privata.
Nel costruttore desirializziamo gli album contenuti in un file Json.
La classe contine anche il path della cartella in cui vengono salvati gli album 

```csharp
 public class AlbumService
{   
        
    private readonly List<Album> _albums = new();
        
    public string path;
        

    public AlbumService()
    {
        ReadConfig();
        DeserializeAlbums();
    }

    // ... resto dei metodi ...

}
```

### ReadConfig

Metodo che legge tutte le righe dentro Config.txt e salva gli elementi che servono nelle varie variabili del programma

```csharp
public void ReadConfig()
{
    string[] lines = File.ReadAllLines(@"Config.txt");
    //riga 1 di config: path salvataggio albums
    path = lines[0].Replace("path:", "");
}

```
### DeserializeAlbums

Metodo che deserializza gli album nella lista di album della classe.
Gestione degli errori che puo dare il metodo.
- "DirectoryNotFoundException": errore che si verifica quando non viene trovata la cartella
- "JsonException": errore che si verifica quando un file json non rispetta il formato 
Ogni singolo album viene gestito in un file Json specifico per sè.

```csharp
public void DeserializeAlbums()
{

    if (!Directory.Exists(path))
        throw new DirectoryNotFoundException($"Cartella {path} non trovata");

    List<string> filesNames = Directory.GetFiles(path).ToList();

    foreach (string fileName in filesNames)
    {
        string filesContent = File.ReadAllText(fileName);
        Album album = JsonConvert.DeserializeObject<Album>(filesContent);
        if (album == null)
            throw new JsonException($"contenuto di {fileName} non valido.");
        _albums.Add(album);
    }
}
```

### SerializeAlbums

Metodo che serializza il singolo album(oggetto) nella cartella sul path della classe.
Vuole come parametro l'oggetto da salvare
Gestione degli errori che puo dare il metodo:
- "DirectoryNotFoundException": errore che si verifica quando non viene trovata la cartella

```csharp

public void SerializeAlbum(Album album)
{
    if (!Directory.Exists(path))
        throw new DirectoryNotFoundException($"Cartella {path} non trovata");
    string fileContent = JsonConvert.SerializeObject(album, Formatting.Indented);
    string pathJson = $@"{path}/{album.Titolo}({album.Id}).json";
    string correctPath = pathJson.Replace(" ", "_");
    File.WriteAllText(correctPath, fileContent);
}
```
### GetAll

Metodo che restituisce la lista degli album contenuti nella lista privata (album) della classe


```csharp        

public List<Album> GetAll()
{
    List<Album> result = new List<Album>();
    foreach (var album in _albums)
    {
        result.Add(album);
    }
    return result;
}
```        
### GetById 

Metodo che restituisce l'album per id datogli come parametro

```csharp 
public Album? GetById(int id)
{
    foreach (var album in _albums)
    {
        if (album.Id == id)
        {
            return album;
        }
    }
    return null;
}
``` 

### CalcolaID

Metodo che calcola l'id successivo da assegnare in base a quelli esistenti e lo restituisce

```csharp 
public int CalcolaId()
{
    int id = 0;
    foreach (var album in _albums)
    {
        if (id <= album.Id)
            id = album.Id + 1;
    }
    return id;
}
``` 
### Add

Metodo che aggiunge un album pasatogli come parametro alla lista privata e invoca la funzione `SerializeAlbum` che serializza il contenuto

```csharp 
public Album Add(Album newAlbum)
{
    newAlbum.Id = CalcolaId();
    int idCanzone = 0;
    foreach (Canzone c in newAlbum.Canzoni)
    {
        c.Id = idCanzone;
        idCanzone++;
    }
    _albums.Add(newAlbum);
    SerializeAlbum(newAlbum);
    return newAlbum;
}
``` 
### IdCanzoni
calcola l'id massimo delle canzoni dalla lista degli album e lo restituisce + 1
```csharp 
public int IdCanzoni()
{
    int id = 0;
    foreach (var album in _albums)
    {
        foreach (var canzone in album.Canzoni)
        {
            if (id <= canzone.Id)
                id = canzone.Id + 1;
        }
    }
    return id;
}
```

### Update

metodo che aggiorna un album dato il suo id.
Restiruisce false se non riesce, altrimenti true.

```csharp 
public bool Update(int id, Album updatedAlbum)
{
    Album? existing = null;
    foreach (var a in _albums)
    {
        if (a.Id == id)
        {
            existing = a;
            break;
        }
    }
    if (existing == null)
        return false;
    existing.Titolo = updatedAlbum.Titolo;
    existing.Anno = updatedAlbum.Anno;
    existing.Autore = updatedAlbum.Autore;
    existing.Canzoni = updatedAlbum.Canzoni;
    existing.Genere = updatedAlbum.Genere;
    existing.Ascoltato = updatedAlbum.Ascoltato;
    SerializeAlbum(updatedAlbum);
    return true;
} 
``` 
### Delete

Metodo che elimina il file Json del album dato il suo id(parametro) e lo cancella anche dalla lista privata.
Restiruisce false se non riesce, altrimenti true.

```csharp 
public bool Delete(int id)
{
    Album album = GetById(id);
    if (album is null)
        return false;
    string pathJson = $@"{path}/{album.Titolo}({album.Id}).json";
    string correctPath = pathJson.Replace(" ", "_");
    File.Delete(correctPath);
    _albums.Remove(album);
    return true;
}
``` 

## AlbumController

Iniziamo definendo un controller API con gli atributi presenti fra parentesi quadre.
La classe AlbumController eredita dalla classe ControllerBase quindi riceve le proprieta ed i comportamenti di essa.
La classe contiene un oggetto privato _service di tipo ALbumService
La classe viene costruita salvando in _service (privato) il service pubblico che passiamo come parametro

```csharp
[ApiController]
[Route("api/[controller]")]

public class AlbumController : ControllerBase
{
    private readonly AlbumService _service;
    public AlbumController(AlbumService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    // ... Resto dei metodi ...
}   
```
### Get

Metodo del controller che gestisce le richieste HttpGet richiamando il metodo GetAll di AlbumService.
permette di visualizzare il contenuto.

```csharp
[HttpGet]
public ActionResult<List<Album>> Get()
{
    List<Album> albums = _service.GetAll();
    return Ok(albums);
}
```
### Get(id)

Metodo del controller che gestisce le richieste HttpGet sullo specifico id per far visualizzare lo specifico Album di quell'id (usiamo il metodo GetById di AlbumService)

```csharp
[HttpGet("{id}")]
public ActionResult<Album> Get(int id)
{
    var p = _service.GetById(id);
    if (p is null)
        return NotFound();
    return p;
}
```
### Post

Metodo del controller che gestisce le richieste HttpPost richiamando il metodo Add di AlbumService.
Permette di aggiungere elementi agli album

```csharp
[HttpPost]
public ActionResult<Album> Post([FromBody] Album album)
{
    Album created = _service.Add(album);
    return CreatedAtAction(
        actionName: nameof(Get),
        routeValues: new { id = created.Id },
        value: created
    );
}
```
### Put

Metodo del controller che gestisce le richieste HttpPut sullo specifico id per modificare lo specifico Album di quell'id (usiamo il metodo Update di AlbumService)

```csharp
[HttpPut("{id}")]
public IActionResult Put(int id, [FromBody] Album album)
{
    bool success = _service.Update(id, album);
    if (success == false)
        return NotFound();
    else
        return NoContent();
}
```
### Delete

Metodo del controller che gestisce le richieste HttpDelete richiamando il metodo Delete di AlbumService.
Permette di eliminare il singolo album dato il suo id.

```csharp
[HttpDelete("{id}")]
public IActionResult Delete(int id)
{
    bool success = _service.Delete(id);
    if (success == false)
        return NotFound();
    else
        return NoContent();
}
```