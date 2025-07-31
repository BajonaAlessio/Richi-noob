import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Purchase, PurchaseDTO } from '../models/purchase.model';

@Injectable({ providedIn: 'root' })
export class PurchaseService {
  private apiUrl = 'http://localhost:5296/api/purchases'; // Modifica porta se diversa!

  constructor(private http: HttpClient) {}

  getAll(): Observable<PurchaseDTO[]> {
    return this.http.get<PurchaseDTO[]>(this.apiUrl);
  }

  get(id: number): Observable<Purchase> {
    return this.http.get<Purchase>(`${this.apiUrl}/${id}`);
  }

  add(purchase: Purchase): Observable<Purchase> {
    return this.http.post<Purchase>(this.apiUrl, purchase);
  }

  update(id: number, purchase: Purchase): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, purchase);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}