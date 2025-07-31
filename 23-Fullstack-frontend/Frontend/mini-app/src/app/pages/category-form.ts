import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';

import { Category } from '../models/category.model';
import { CategoryService } from '../services/category';

@Component({
  selector: 'app-category-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule
  ],
  template: `
    <mat-card class="form-card">
      <mat-card-title>{{ isNew ? 'Nuova categoria' : 'Modifica categoria' }}</mat-card-title>
      <form [formGroup]="form" (ngSubmit)="onSubmit()">

        <mat-form-field appearance="outline" class="full-width">
          <mat-label>Nome</mat-label>
          <input matInput formControlName="name" required />
          <mat-error *ngIf="form.get('name')?.hasError('required')">
            Nome obbligatorio
          </mat-error>
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
    .form-card { max-width: 500px; margin:2rem auto; padding:1rem; }
    .full-width { width:100%; margin-bottom:1rem; }
    .buttons { display:flex; gap:1rem; justify-content:flex-end; }
  `]
})
export class CategoryForm implements OnInit {
  form!: FormGroup;
  isNew = true;
  categoryId = 0;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private service: CategoryService
  ) {}

  ngOnInit() {
    this.form = this.fb.group({
      name: ['', Validators.required]
    });

    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isNew = false;
      this.categoryId = +id;
      this.service.get(this.categoryId).subscribe(c => {
        this.form.patchValue({ name: c.name });
      });
    }
  }

  onSubmit() {
    if (this.form.invalid) return;

    const name = this.form.value.name as string; // safe perché validato
    const payload: Category = { id: this.categoryId, name };

    if (this.isNew) {
      this.service.add(payload).subscribe(() => this.goBack());
    } else {
      this.service.update(this.categoryId, payload).subscribe(() => this.goBack());
    }
  }

  goBack() {
    this.router.navigate(['/categories']);
  }
}