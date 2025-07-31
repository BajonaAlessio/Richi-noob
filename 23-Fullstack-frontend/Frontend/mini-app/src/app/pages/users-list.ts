import { CommonModule } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { RouterModule } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { User } from '../models/user.model';
import { SnackbarService } from '../services/snackbar';
import { UserService } from '../services/user';
import { ConfirmDialogComponent, ConfirmDialogData } from './confirm-dialog';

@Component({
  selector: 'app-users-list',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule
  ],
  template: `
  <div class="table-wrapper mat-elevation-z2">
  <div class="header">
      <h2>Utenti</h2>
      <button mat-flat-button color="primary" routerLink="/users/new">
        <mat-icon>person_add</mat-icon> Nuovo
      </button>
    </div>

    <mat-form-field appearance="outline" class="filter">
      <mat-label>Cerca utente</mat-label>
      <input matInput (keyup)="applyFilter($event)" placeholder="Nome o città">
    </mat-form-field>

    
      <table mat-table [dataSource]="dataSource" matSort class="full-width">

        <ng-container matColumnDef="name">
          <th mat-header-cell *matHeaderCellDef mat-sort-header>Nome</th>
          <td mat-cell *matCellDef="let u">{{ u.name }}</td>
        </ng-container>

        <ng-container matColumnDef="address">
          <th mat-header-cell *matHeaderCellDef>Indirizzo</th>
          <td mat-cell *matCellDef="let u">
            {{ u.address.citta }}, {{ u.address.via }} ({{ u.address.cap }})
          </td>
        </ng-container>

        <ng-container matColumnDef="actions">
          <th mat-header-cell *matHeaderCellDef>Azioni</th>
          <td mat-cell *matCellDef="let u">
            <button mat-icon-button color="primary" [routerLink]="['/users', u.id]">
              <mat-icon>edit</mat-icon>
            </button>
            <button mat-icon-button color="warn" (click)="delete(u.id)">
              <mat-icon>delete</mat-icon>
            </button>
          </td>
        </ng-container>

        <tr mat-header-row *matHeaderRowDef="cols"></tr>
        <tr mat-row *matRowDef="let row; columns: cols;"></tr>
      </table>

      <mat-paginator [pageSize]="10" showFirstLastButtons></mat-paginator>
    </div>
  `,
  styles: [`
    .header { display:flex; justify-content: space-between; align-items:center; flex-wrap:wrap; gap:1rem; }
    .filter { width:100%; max-width:360px; margin:1rem 0; }
    .table-wrapper { overflow:auto; border-radius:6px; }
    .full-width { width:100%; }
  `]
})
export class UsersList implements OnInit {
  cols = ['name', 'address', 'actions'];
  dataSource = new MatTableDataSource<User>([]);

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
  private service: UserService,
  private dialog: MatDialog,
  private snackbar: SnackbarService
) {}

  ngOnInit() {
    this.load();
  }

  load() {
    this.service.getAll().subscribe(data => {
      this.dataSource.data = data;
      // filtro custom per città/nome
      this.dataSource.filterPredicate = (u: User, filter: string) => {
        const f = filter.trim().toLowerCase();
        return u.name.toLowerCase().includes(f) ||
               u.address.citta.toLowerCase().includes(f) ||
               u.address.via.toLowerCase().includes(f) ||
               u.address.cap.toLowerCase().includes(f);
      };
      setTimeout(() => {
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      });
    });
  }

  applyFilter(e: Event) {
    const v = (e.target as HTMLInputElement).value;
    this.dataSource.filter = v.trim().toLowerCase();
  }

  async delete(id: number) {
  const dialogRef = this.dialog.open(ConfirmDialogComponent, {
    width: '360px',
    data: {
      title: 'Elimina utente',
      message: 'Vuoi davvero cancellare questo utente?',
      confirmText: 'Elimina',
      cancelText: 'Annulla'
    } as ConfirmDialogData,
    disableClose: true
  });

  const confirmed: boolean | undefined = await firstValueFrom(dialogRef.afterClosed());
  if (!confirmed) return;

  this.service.delete(id).subscribe({
    next: () => this.snackbar.success('Utente eliminato'),
    error: () => this.snackbar.error('Errore durante eliminazione utente')
  });
}
}