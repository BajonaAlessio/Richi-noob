import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';

import { Product } from '../models/product.model';
import { Purchase } from '../models/purchase.model';
import { User } from '../models/user.model';
import { ProductService } from '../services/product';
import { PurchaseService } from '../services/purchase';
import { UserService } from '../services/user';

@Component({
  selector: 'app-purchase-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  template: `
    <mat-card class="form-card">
      <mat-card-title>{{ isNew ? 'Nuovo acquisto' : 'Modifica acquisto' }}</mat-card-title>
      <form [formGroup]="form" (ngSubmit)="onSubmit()">

        <mat-form-field appearance="outline" class="full-width">
          <mat-label>Utente</mat-label>
          <mat-select formControlName="userId" required>
            <mat-option *ngFor="let u of users" [value]="u.id">{{ u.name }}</mat-option>
          </mat-select>
          <mat-error *ngIf="form.get('userId')?.hasError('required')">Seleziona un utente</mat-error>
        </mat-form-field>

        <mat-form-field appearance="outline" class="full-width">
          <mat-label>Prodotto</mat-label>
          <mat-select formControlName="productId" required>
            <mat-option *ngFor="let p of products" [value]="p.id">{{ p.name }}</mat-option>
          </mat-select>
          <mat-error *ngIf="form.get('productId')?.hasError('required')">Seleziona un prodotto</mat-error>
        </mat-form-field>

        <mat-form-field appearance="outline" class="full-width">
          <mat-label>Quantità</mat-label>
          <input matInput type="number" formControlName="quantity" required>
          <mat-error *ngIf="form.get('quantity')?.hasError('required')">Obbligatorio</mat-error>
          <mat-error *ngIf="form.get('quantity')?.hasError('min')">Deve essere almeno 1</mat-error>
        </mat-form-field>

        <mat-form-field appearance="outline" class="full-width">
          <mat-label>Data</mat-label>
          <input matInput [matDatepicker]="picker" formControlName="purchaseDate" required>
          <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
          <mat-datepicker #picker></mat-datepicker>
          <mat-error *ngIf="form.get('purchaseDate')?.hasError('required')">Obbligatorio</mat-error>
        </mat-form-field>

        <div class="buttons">
          <button mat-raised-button color="primary" type="submit" [disabled]="form.invalid">
            <mat-icon>save</mat-icon> {{ isNew ? 'Crea' : 'Salva' }}
          </button>
          <button mat-stroked-button type="button" (click)="goBack()">
            <mat-icon>arrow_back</mat-icon> Annulla
          </button>
        </div>
      </form>
    </mat-card>
  `,
  styles: [`
    .form-card { max-width: 600px; margin:2rem auto; padding:1rem; }
    .full-width { width:100%; margin-bottom:1rem; }
    .buttons { display:flex; gap:1rem; justify-content:flex-end; margin-top:1rem; }
  `]
})
export class PurchaseForm implements OnInit {
  form!: FormGroup;
  isNew = true;
  purchaseId = 0;
  users: User[] = [];
  products: Product[] = [];

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private service: PurchaseService,
    private userService: UserService,
    private productService: ProductService
  ) {}

  ngOnInit() {
    this.form = this.fb.group({
      userId: [null, Validators.required],
      productId: [null, Validators.required],
      quantity: [1, [Validators.required, Validators.min(1)]],
      purchaseDate: [new Date(), Validators.required]
    });

    this.userService.getAll().subscribe(u => this.users = u);
    this.productService.getAll().subscribe(p => this.products = p.map(pr => ({
      id: pr.id, name: pr.name, price: pr.price, categoryId: 0
    } as Product)));

    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam) {
      this.isNew = false;
      this.purchaseId = +idParam;
      this.service.get(this.purchaseId).subscribe(p => {
        this.form.patchValue({
          userId: p.userId,
          productId: p.productId,
          quantity: p.quantity,
          purchaseDate: new Date(p.purchaseDate)
        });
      });
    }
  }

  onSubmit() {
    if (this.form.invalid) return;
    const raw = this.form.value;
    const payload: Purchase = {
      id: this.purchaseId,
      userId: raw.userId,
      productId: raw.productId,
      quantity: raw.quantity,
      purchaseDate: (raw.purchaseDate as Date).toISOString()
    };
    if (this.isNew) {
      this.service.add(payload).subscribe(() => this.goBack());
    } else {
      this.service.update(this.purchaseId, payload).subscribe(() => this.goBack());
    }
  }

  goBack() {
    this.router.navigate(['/purchases']);
  }
}