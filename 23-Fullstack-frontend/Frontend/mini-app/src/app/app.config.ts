import { ApplicationConfig, provideBrowserGlobalErrorListeners, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
/*
Questo file definisce la configurazione dell'applicazione Angular.
La configurazione include i provider necessari per gestire gli errori globali e il rilevamento dei cambiamenti di zona ed la configurazione delle rotte dell'applicazione.
I provider sono funzioni che forniscono servizi o configurazioni all'interno dell'applicazione.
*/
export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(), // provideBrowserGlobalErrorListeners è usato per gestire gli errori globali nell'applicazione
    provideZoneChangeDetection({ eventCoalescing: true }), // provideZoneChangeDetection è usato per ottimizzare il rilevamento dei cambiamenti di zona nell'applicazione
    provideRouter(routes) // provideRouter è usato per configurare le rotte dell'applicazione
  ]
};
