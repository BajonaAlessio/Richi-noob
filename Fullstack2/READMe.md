dotnet new webapi -o backend

# FULLSTACK v2

- In questa versione l'obbiettivo è implementare un servizio che prenda i contenuti da un file json invece che da una lista interna.
- i dati vengono presi da product service, quindi ogni volta che lanciamo l'applicazione, le modifiche e inserimenti vengono resettati. obbiettivo perdurare i dati.
- il file json che contiene i dati è organizzato come una lista/elenco di oggetti, simili a dizionari.
- il file json verra deserializzato usando una classe album con le seguenti proprietà: Id(generato automaticamente) - Titolo(string) - Anno(int) - Autore(string) - canzoni(lista string) - Genere(string) - Ascoltato(bool)
- in questa versione non è necessario aveere una gestione dell'input dell'utente, ma dare info al front tramite servizio HTTP: Get Album (tutti gli album) - Get Albums(album specifico) - Post album (aggiunge un nuovo album) - PUT album(aggiorna album esistente) - Delete albums(elimina album esistente)
- l'applicazione devere essere in grado di generare un id progressivo, basandosi sugli album dentro il file json (ultimo id +1)
- CURL - devono essere implementati i commandi principali per testare il servizio
Esempio di commando CURL : 
 ```csharp
"Content-Type:application/json"\
'{
"id":
"titolo": "Album",
"anno": 2023,
"autore": "Cantante",
}'
 ```




      
