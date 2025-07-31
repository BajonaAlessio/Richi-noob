import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';

import { User } from '../models/user.model';
import { UserService } from '../services/user';

@Component({
  selector: 'app-user-form',
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
      <mat-card-title>{{ isNew ? 'Nuovo utente' : 'Modifica utente' }}</mat-card-title>
      <form [formGroup]="form" (ngSubmit)="onSubmit()">

        <mat-form-field appearance="outline" class="full-width">
          <mat-label>Nome</mat-label>
          <input matInput formControlName="name" required>
          <mat-error *ngIf="form.get('name')?.hasError('required')">Nome obbligatorio</mat-error>
        </mat-form-field>

        <div class="address-group">
          <h4>Indirizzo</h4>

          <mat-form-field appearance="outline" class="full-width">
            <mat-label>Città</mat-label>
            <input matInput formControlName="citta" required>
            <mat-error *ngIf="form.get('citta')?.hasError('required')">Città obbligatoria</mat-error>
          </mat-form-field>

          <mat-form-field appearance="outline" class="full-width">
            <mat-label>Via</mat-label>
            <input matInput formControlName="via" required>
            <mat-error *ngIf="form.get('via')?.hasError('required')">Via obbligatoria</mat-error>
          </mat-form-field>

          <mat-form-field appearance="outline" class="full-width">
            <mat-label>CAP</mat-label>
            <input matInput formControlName="cap" required>
            <mat-error *ngIf="form.get('cap')?.hasError('required')">CAP obbligatorio</mat-error>
          </mat-form-field>
        </div>

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
    .form-card { max-width: 600px; margin: 2rem auto; padding:1rem; }
    .full-width { width:100%; margin-bottom:1rem; }
    .buttons { display:flex; gap:1rem; justify-content:flex-end; margin-top:1rem; }
    .address-group h4 { margin:0.5rem 0 0.25rem; }
  `]
})
export class UserForm implements OnInit {
  form!: FormGroup;
  isNew = true;
  userId = 0;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private service: UserService
  ) {}

  ngOnInit() {
    this.form = this.fb.group({
      name: ['', Validators.required],
      citta: ['', Validators.required],
      via: ['', Validators.required],
      cap: ['', Validators.required]
    });

    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isNew = false;
      this.userId = +id;
      this.service.get(this.userId).subscribe(u => {
        this.form.patchValue({
          name: u.name,
          citta: u.address.citta,
          via: u.address.via,
          cap: u.address.cap
        });
      });
    }
  }

  onSubmit() {
    if (this.form.invalid) return;

    const payload: User = {
      id: this.userId,
      name: this.form.value.name,
      address: {
        citta: this.form.value.citta,
        via: this.form.value.via,
        cap: this.form.value.cap
      }
    };

    if (this.isNew) {
      this.service.add(payload).subscribe(() => this.goBack());
    } else {
      this.service.update(this.userId, payload).subscribe(() => this.goBack());
    }
  }

  goBack() {
    this.router.navigate(['/users']);
  }
}