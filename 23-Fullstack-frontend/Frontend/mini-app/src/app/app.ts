import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
/*
Questo file definisce il componente principale dell'applicazione Angular.
Il componente App è il punto di ingresso dell'applicazione e contiene la struttura principale.
*/
@Component({ // Il decoratore @Component definisce le proprietà del componente App
  selector: 'app-root', // Il selettore 'app-root' è usato per identificare il componente nell'HTML dell'applicazione
  imports: [RouterOutlet], // RouterOutlet è usato per visualizzare i componenti delle rotte cioè le pagine dell'applicazione
  templateUrl: './app.html', // Il templateUrl specifica il file HTML che definisce la struttura del componente dell'applicazione
  styleUrl: './app.css' // Il styleUrl specifica il file CSS che contiene gli stili per il componente dell'applicazione
})
/*
l esport class App definisce il componente principale dell'applicazione Angular.
Questo componente è responsabile della visualizzazione della struttura principale dell'applicazione
Si usa export per rendere il componente disponibile per l'importazione in altri file.
*/
export class App {
  protected title = 'mini-app'; // La proprietà title è usata per definire il titolo dell'applicazione
}
