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
import { ProductDTO } from '../models/product.model';
import { ProductService } from '../services/product';
import { SnackbarService } from '../services/snackbar';
import { ConfirmDialogComponent, ConfirmDialogData } from './confirm-dialog';

@Component({
  selector: 'app-products-list',
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
      <h2>Prodotti</h2>
      <button mat-flat-button color="primary" routerLink="/products/new">
        <mat-icon>add</mat-icon> Nuovo
      </button>
    </div>

    <mat-form-field appearance="outline" class="filter">
      <mat-label>Cerca</mat-label>
      <input matInput (keyup)="applyFilter($event)" placeholder="Nome o categoria">
    </mat-form-field>

    
      <table mat-table [dataSource]="dataSource" matSort class="full-width">

        <ng-container matColumnDef="name">
          <th mat-header-cell *matHeaderCellDef mat-sort-header>Nome</th>
          <td mat-cell *matCellDef="let el">{{ el.name }}</td>
        </ng-container>

        <ng-container matColumnDef="price">
          <th mat-header-cell *matHeaderCellDef mat-sort-header>Prezzo</th>
          <td mat-cell *matCellDef="let el">{{ el.price | currency:'EUR' }}</td>
        </ng-container>

        <ng-container matColumnDef="categoryName">
          <th mat-header-cell *matHeaderCellDef mat-sort-header>Categoria</th>
          <td mat-cell *matCellDef="let el">{{ el.categoryName }}</td>
        </ng-container>

        <ng-container matColumnDef="actions">
          <th mat-header-cell *matHeaderCellDef>Azioni</th>
          <td mat-cell *matCellDef="let el">
            <button mat-icon-button color="primary" [routerLink]="['/products', el.id]">
              <mat-icon>edit</mat-icon>
            </button>
            <button mat-icon-button color="warn" (click)="delete(el.id)">
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
export class ProductsList implements OnInit {
  cols = ['name', 'price', 'categoryName', 'actions'];
  dataSource = new MatTableDataSource<ProductDTO>([]);

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
  private service: ProductService,
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
    const v = (e.target as HTMLInputElement).value.trim().toLowerCase();
    this.dataSource.filter = v;
  }

async delete(id: number) {
  const dialogRef = this.dialog.open(ConfirmDialogComponent, {
    width: '360px',
    data: {
      title: 'Elimina prodotto',
      message: 'Sei sicuro di voler eliminare questo prodotto?',
      confirmText: 'Elimina',
      cancelText: 'Annulla'
    } as ConfirmDialogData,
    disableClose: true
  });

  const confirmed = await dialogRef.afterClosed().toPromise();
  if (!confirmed) return;

  this.service.delete(id).subscribe({
    next: () => this.snackbar.success('Prodotto eliminato con successo'),
    error: () => this.snackbar.error('Errore durante l\'eliminazione')
  });
}
}