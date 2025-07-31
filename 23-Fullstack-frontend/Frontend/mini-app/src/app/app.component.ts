import { BreakpointObserver, LayoutModule } from '@angular/cdk/layout';
import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { RouterModule } from '@angular/router';
/*
Questo file definisce il componente principale dell'applicazione Angular.
Il componente AppComponent funge da layout principale, includendo una barra di navigazione e un pannello laterale.
Utilizza Angular Material per lo stile e la struttura del layout.
Il layout è responsive, adattandosi a schermi di diverse dimensioni.
Il pannello laterale contiene i link di navigazione per le diverse pagine dell'applicazione.
Il router-outlet è usato per visualizzare i componenti delle pagine in base alla rotta corrente.
*/
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatSidenavModule,
    MatListModule,
    LayoutModule
  ],
  template: `
    <mat-toolbar color="primary">
      <button mat-icon-button (click)="drawer.toggle()" class="hide-desktop">
        <mat-icon>menu</mat-icon>
      </button>
      <span>MiniApp CRUD</span>
      <span class="spacer"></span>
    </mat-toolbar>
    <mat-sidenav-container class="main-container">
      <mat-sidenav #drawer class="side-nav" [mode]="isMobile ? 'over' : 'side'" [opened]="!isMobile">
        <mat-nav-list>
          <a mat-list-item routerLink="/products" routerLinkActive="active"><mat-icon>inventory</mat-icon> Prodotti</a>
          <a mat-list-item routerLink="/categories" routerLinkActive="active"><mat-icon>category</mat-icon> Categorie</a>
          <a mat-list-item routerLink="/users" routerLinkActive="active"><mat-icon>person</mat-icon> Utenti</a>
          <a mat-list-item routerLink="/purchases" routerLinkActive="active"><mat-icon>shopping_cart</mat-icon> Acquisti</a>
        </mat-nav-list>
      </mat-sidenav>
      <mat-sidenav-content>
        <main class="content">
          <router-outlet></router-outlet>
        </main>
      </mat-sidenav-content>
    </mat-sidenav-container>
  `,
  styles: [`
    .main-container { min-height: 100vh; }
    .side-nav { width: 210px; }
    .spacer { flex: 1 1 auto; }
    .content { padding: 1.5rem; }
    .hide-desktop { display: none; }
    @media (max-width: 800px) {
      .side-nav { width: 160px; }
      .hide-desktop { display: inline-flex; }
    }
  `]
})
export class AppComponent {
  isMobile = false;
  constructor(breakpoints: BreakpointObserver) {
    breakpoints.observe(['(max-width: 800px)']).subscribe(res => {
      this.isMobile = res.matches;
    });
  }
}