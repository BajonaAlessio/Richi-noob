import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({ providedIn: 'root' })
export class SnackbarService {
  constructor(private snack: MatSnackBar) {}

  success(message: string, action = 'OK', durationMs = 2500) {
    this.snack.open(message, action, {
      duration: durationMs,
      panelClass: ['snackbar-success'],
    });
  }

  error(message: string, action = 'CHIUDI', durationMs = 4000) {
    this.snack.open(message, action, {
      duration: durationMs,
      panelClass: ['snackbar-error'],
    });
  }

  info(message: string, action = 'OK', durationMs = 2000) {
    this.snack.open(message, action, {
      duration: durationMs,
      panelClass: ['snackbar-info'],
    });
  }
}