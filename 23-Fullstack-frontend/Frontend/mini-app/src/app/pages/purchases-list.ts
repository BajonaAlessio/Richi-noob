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
import { PurchaseDTO } from '../models/purchase.model';
import { PurchaseService } from '../services/purchase';
import { SnackbarService } from '../services/snackbar';
import { ConfirmDialogComponent, ConfirmDialogData } from './confirm-dialog';

@Component({
  selector: 'app-purchases-list',
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
      <h2>Acquisti</h2>
      <button mat-flat-button color="primary" routerLink="/purchases/new">
        <mat-icon>shopping_cart</mat-icon> Nuovo
      </button>
    </div>

    <mat-form-field appearance="outline" class="filter">
      <mat-label>Cerca</mat-label>
      <input matInput (keyup)="applyFilter($event)" placeholder="Utente, prodotto, categoria">
    </mat-form-field>

    
      <table mat-table [dataSource]="dataSource" matSort class="full-width">

        <ng-container matColumnDef="userName">
          <th mat-header-cell *matHeaderCellDef mat-sort-header>Utente</th>
          <td mat-cell *matCellDef="let p">{{ p.userName }}</td>
        </ng-container>

        <ng-container matColumnDef="productName">
          <th mat-header-cell *matHeaderCellDef mat-sort-header>Prodotto</th>
          <td mat-cell *matCellDef="let p">{{ p.productName }}</td>
        </ng-container>

        <ng-container matColumnDef="productCategory">
          <th mat-header-cell *matHeaderCellDef mat-sort-header>Categoria</th>
          <td mat-cell *matCellDef="let p">{{ p.productCategory }}</td>
        </ng-container>

        <ng-container matColumnDef="quantity">
          <th mat-header-cell *matHeaderCellDef mat-sort-header>Quantità</th>
          <td mat-cell *matCellDef="let p">{{ p.quantity }}</td>
        </ng-container>

        <ng-container matColumnDef="purchaseDate">
          <th mat-header-cell *matHeaderCellDef mat-sort-header>Data</th>
          <td mat-cell *matCellDef="let p">{{ p.purchaseDate | date:'shortDate' }}</td>
        </ng-container>

        <ng-container matColumnDef="actions">
          <th mat-header-cell *matHeaderCellDef>Azioni</th>
          <td mat-cell *matCellDef="let p">
            <button mat-icon-button color="primary" [routerLink]="['/purchases', p.id]">
              <mat-icon>edit</mat-icon>
            </button>
            <button mat-icon-button color="warn" (click)="delete(p.id)">
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
  `]
})
export class PurchasesList implements OnInit {
  cols = ['userName', 'productName', 'productCategory', 'quantity', 'purchaseDate', 'actions'];
  dataSource = new MatTableDataSource<PurchaseDTO>([]);

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
  private service: PurchaseService,
  private dialog: MatDialog,
  private snackbar: SnackbarService
) {}

  ngOnInit() {
    this.load();
  }

  load() {
    this.service.getAll().subscribe(data => {
      this.dataSource.data = data;
      setTimeout(() => {
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      });
    });
  }

  applyFilter(e: Event) {
    this.dataSource.filter = (e.target as HTMLInputElement).value.trim().toLowerCase();
  }

  async delete(id: number) {
  const dialogRef = this.dialog.open(ConfirmDialogComponent, {
    width: '360px',
    data: {
      title: 'Elimina acquisto',
      message: 'Vuoi davvero rimuovere questo acquisto?',
      confirmText: 'Elimina',
      cancelText: 'Annulla'
    } as ConfirmDialogData,
    disableClose: true
  });

  const confirmed: boolean | undefined = await firstValueFrom(dialogRef.afterClosed());
  if (!confirmed) return;

  this.service.delete(id).subscribe({
    next: () => this.snackbar.success('Acquisto eliminato'),
    error: () => this.snackbar.error('Errore durante eliminazione acquisto')
  });
}
}