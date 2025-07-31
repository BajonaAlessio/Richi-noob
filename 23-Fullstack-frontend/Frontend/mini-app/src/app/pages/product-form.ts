import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';

import { Category } from '../models/category.model';
import { Product } from '../models/product.model';
import { CategoryService } from '../services/category';
import { ProductService } from '../services/product';

@Component({
  selector: 'app-product-form',
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
    MatCardModule
  ],
  template: `
    <mat-card class="form-card">
      <mat-card-title>{{ isNew ? 'Nuovo prodotto' : 'Modifica prodotto' }}</mat-card-title>
      <form [formGroup]="form" (ngSubmit)="onSubmit()">

        <!-- Name -->
        <mat-form-field appearance="outline" class="full-width">
          <mat-label>Nome</mat-label>
          <input matInput formControlName="name" placeholder="Es. Penna" required>
          <mat-error *ngIf="form.get('name')?.hasError('required')">
            Il nome è obbligatorio
          </mat-error>
        </mat-form-field>

        <!-- Price -->
        <mat-form-field appearance="outline" class="full-width">
          <mat-label>Prezzo (€)</mat-label>
          <input matInput type="number" formControlName="price" placeholder="Es. 1.50" required>
          <mat-error *ngIf="form.get('price')?.hasError('required')">
            Il prezzo è obbligatorio
          </mat-error>
          <mat-error *ngIf="form.get('price')?.hasError('min')">
            Il prezzo deve essere maggiore di zero
          </mat-error>
        </mat-form-field>

        <!-- Category -->
        <mat-form-field appearance="outline" class="full-width">
          <mat-label>Categoria</mat-label>
          <mat-select formControlName="categoryId" required>
            <mat-option *ngFor="let c of categories" [value]="c.id">
              {{ c.name }}
            </mat-option>
          </mat-select>
          <mat-error *ngIf="form.get('categoryId')?.hasError('required')">
            Scegli una categoria
          </mat-error>
        </mat-form-field>

        <!-- Buttons -->
        <div class="buttons">
          <button mat-raised-button color="primary" type="submit" [disabled]="form.invalid">
            <mat-icon *ngIf="isNew">add</mat-icon>
            <mat-icon *ngIf="!isNew">save</mat-icon>
            {{ isNew ? 'Crea' : 'Salva' }}
          </button>
          <button mat-stroked-button type="button" (click)="goBack()">
            <mat-icon>arrow_back</mat-icon> Annulla
          </button>
        </div>

      </form>
    </mat-card>
  `,
  styles: [`
    .form-card {
      max-width: 500px;
      margin: 2rem auto;
      padding: 1rem;
    }
    .full-width {
      width: 100%;
      margin-bottom: 1rem;
    }
    .buttons {
      display: flex;
      gap: 1rem;
      justify-content: flex-end;
      margin-top: 1.5rem;
    }
  `]
})
export class ProductForm implements OnInit {
  form!: FormGroup;
  categories: Category[] = [];
  isNew = true;
  private productId!: number;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private service: ProductService,
    private catService: CategoryService
  ) {}

  ngOnInit() {
    // Carica categorie
    this.catService.getAll().subscribe(cats => this.categories = cats);

    // Costruisci form
    this.form = this.fb.group({
      name:       ['', Validators.required],
      price:      [0, [Validators.required, Validators.min(0.01)]],
      categoryId: [null, Validators.required]
    });

    // Se c'è un ID in URL => modifica
    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam) {
      this.isNew = false;
      this.productId = +idParam;
      this.service.get(this.productId).subscribe(dto => {
        // trova l'ID categoria corrispondente
        const cat = this.categories.find(c => c.name === dto.categoryName);
        this.form.patchValue({
          name:       dto.name,
          price:      dto.price,
          categoryId: cat?.id ?? null
        });
      });
    }
  }

  onSubmit() {
    if (this.form.invalid) return;

    const payload: Product = {
      id: this.productId,
      name: this.form.value.name,
      price: this.form.value.price,
      categoryId: this.form.value.categoryId
    };

    if (this.isNew) {
      this.service.add(payload).subscribe(() => this.goBack());
    } else {
      this.service.update(this.productId, payload).subscribe(() => this.goBack());
    }
  }

  goBack() {
    this.router.navigate(['/products']);
  }
}