import { provideHttpClient } from '@angular/common/http';
import { MatNativeDateModule } from '@angular/material/core';
import { bootstrapApplication } from '@angular/platform-browser';
import { provideRouter } from '@angular/router';
import { AppComponent } from './app/app.component';
import { routes } from './app/app.routes';
/*
Questo file è il punto di ingresso dell'applicazione Angular.
Qui vengono importati i moduli necessari per avviare l'applicazione e configurare le rotte.
*/
// Bootstrap significa avviare l'applicazione Angular
// AppComponent è il componente principale dell'applicazione
bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(routes), // provideRouter è usato per configurare le rotte dell'applicazione
    provideHttpClient(), // provideHttpClient è usato per abilitare le richieste HTTP nell'applicazione
    MatNativeDateModule // MatNativeDateModule è usato per il supporto delle date in Angular Material
  ]
});