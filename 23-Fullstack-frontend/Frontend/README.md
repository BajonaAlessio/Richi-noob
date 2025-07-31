# Angular

- Angular ha sempre usato i moduli (NgModule) fin dalla sua prima versione (Angular 2).
- Da Angular 14 in poi, e soprattutto in Angular 16-20, è arrivata la modalità standalone che non richiede più la dichiarazione di moduli per ogni gruppo di componenti.
- Vediamo le differenze pratiche e cosa cambia nello sviluppo:

## Approccio "classico": NgModules

- Ogni funzionalità (feature) ha un suo modulo (es: ProductsModule, AppModule, ecc.).
- Un modulo dichiara componenti, pipe, directive, importa altri moduli, fornisce servizi.

Per ogni nuovo componente devi:

- Creare il file (ng generate component ...)
- Aggiungerlo a declarations del modulo giusto.
- I servizi si dichiarano in providers (a livello di modulo o root).
- L'app parte sempre da un AppModule che fa da "root" di tutto.

**Esempio:**

```typescript
@NgModule({
  declarations: [ProductsListComponent, ProductFormComponent],
  imports: [CommonModule, FormsModule, RouterModule],
  providers: [ProductService],
  bootstrap: [AppComponent]
})
export class ProductsModule {}
```
**Vantaggi:**

- Organizzazione "a pacchetto", classica.
- Compatibilità assoluta con tutto l’ecosistema Angular e librerie legacy.
- Più controllo su provider, lazy loading avanzato, ecc.

**Svantaggi:**

- Verboso: bisogna sempre ricordarsi di "declarare" e "importare".
- Più passaggi per aggiungere/rimuovere componenti.
- Boilerplate maggiore e più difficile da automatizzare.

## Approccio Standalone (Angular 16+)

Ogni componente può vivere da solo, senza modulo: si crea così:

```typescript
@Component({
  selector: 'app-example',
  standalone: true,
  template: `...`,
  imports: [CommonModule, FormsModule]
})
export class ExampleComponent {}
```
Niente più NgModule! Per comporre, basta importare componenti uno nell'altro.

Le rotte possono puntare direttamente a componenti standalone:

```typescript
export const routes: Routes = [
  { path: 'products', component: ProductsListComponent }
];
```
Bootstrap diretto di un componente root con bootstrapApplication(AppComponent, { ... }).

**Vantaggi:**

- Zero boilerplate: scrivi solo i componenti e li usi subito.
- Più veloce: meno codice da scrivere e mantenere.
- Più semplice per progetti piccoli e medi (ma anche scalabile!).

Meno rischi di errori tipo "Component X is not declared in any module".

- Lazy loading semplificato, più componibilità.
- Nuove librerie Angular (Material, Forms, ecc.) sono già pronte per standalone.

Svantaggi:

- Le librerie molto vecchie (Angular <14) o custom potrebbero non supportare standalone (ma ormai sono rarità).

Se hai un’app enorme già modulare, la migrazione va pianificata.

## In pratica:
Progetto piccolo/medio:
- Standalone è più semplice, veloce, diretto.

Progetto enterprise, legacy o che usa librerie non aggiornate:
- Forse meglio restare su NgModule o fare transizione graduale.

## Esempio diretto
Standalone
```typescript
@Component({
  selector: 'app-users-list',
  standalone: true,
  imports: [CommonModule],
  template: `...`
})
export class UsersListComponent {}
```
e nel router:

```typescript
export const routes: Routes = [
  { path: 'users', component: UsersListComponent }
];
```
Non devi dichiarare niente in un modulo!

**Moduli classici**
Devi:

- Dichiarare in declarations: [] nel modulo.
- Importare il modulo dove serve.

**Conclusione**

- Standalone = Sviluppo moderno, rapido, semplice.
- NgModule = Più controllo, più compatibilità con progetti legacy/complessi.

# 🚀 Comandi Base per Creare l’Archetipo Standalone Angular 20

**1. Installa Angular CLI**

Se non hai ancora Angular CLI:

```bash
npm install -g @angular/cli
```
**2. Crea un nuovo progetto standalone**

Dal 2024, Angular CLI ti permette di creare progetti standalone con un flag dedicato (--standalone):

```bash
ng new nome-progetto --standalone --routing --style=css
```
--standalone: crea il progetto senza NgModule (tutto in standalone).

--routing: abilita il router (consigliato).

--style=css|scss: scegli css o scss per i fogli di stile.

Esempio:

```bash
ng new mini-app-frontend --standalone --routing --style=css
```
**3. Avvia il server di sviluppo**

```bash
cd mini-app-frontend
ng serve
```

Di default gira su http://localhost:4200

## Comandi Utili per Sviluppare

**4. Genera componenti Standalone**
Aggiungi il flag --standalone a ogni nuovo componente:

```bash
ng generate component nome-componente --standalone
# oppure
ng g c nome-componente --standalone
```
Esempio:

```bash
ng g c products-list --standalone
```
**5. Genera servizi**
```bash
ng generate service services/product
# oppure (abbreviato)
ng g s services/product
```
**6. Genera interfacce (model)**
```bash
ng generate interface models/product --type=model
# oppure
ng g i models/product --type=model
```
**7. Genera una pagina (con routing)**
```bash
ng g c pages/products-page --standalone
```
**8. Aggiungi un nuovo percorso (rotta)**
Modifica src/app/app.routes.ts (in standalone), ad esempio:

```typescript
import { ProductsListComponent } from './products-list/products-list.component';
export const routes: Routes = [
  { path: 'products', component: ProductsListComponent }
];
```
**9. Aggiungi una libreria esterna**
Ad esempio Bootstrap:

```bash
npm install bootstrap
```
Poi importa in angular.json (se vuoi usare CSS di bootstrap):

**10. Build produzione**
Per generare i file statici ottimizzati:

```bash
ng build
```
I file verranno messi in dist/nome-progetto/

**11. Test**
Lancia i test (se hai scritto test unitari):

```bash
ng test
```
**12. Lint (controllo qualità codice)**
```bash
ng lint
```
# 🔥 Tip Utili
**Aggiornare Angular CLI**

```bash
npm install -g @angular/cli@latest
```
**Aggiornare un progetto Angular già esistente**

```bash
ng update @angular/core @angular/cli
```
**Aiuto e lista comandi**

```bash
ng help
ng generate --help
```
# 🏁 Checklist tipica (da zero):
**1. Crea progetto**

```bash
ng new mini-app --standalone --routing --style=css
cd mini-app
ng serve
```
**2. Genera componenti**

```bash
ng g c products-list --standalone
ng g c product-form --standalone
# ...altri
```
**3. Genera servizi**

```bash
ng g s services/product
# ...altri
```
**4. Genera model**

```bash
ng g i models/product --type=model
```
- Modifica rotte (app.routes.ts)
- Collega tutto in main.ts e app.component.ts
- Stila con CSS

**5. Build produzione quando pronto**

```bash
ng build
```
# Scaffolding

I comandi Angular 20 da copiare/incollare per generare tutti i componenti, servizi e modelli necessari per le 4 entità: products, users, purchases, categories in modalità standalone

# 🟩 1. CREA I MODELLI (interfacce TypeScript)
```bash
ng g i models/product --type=model
ng g i models/user --type=model
ng g i models/category --type=model
ng g i models/purchase --type=model
```
(Crea i file product.model.ts, user.model.ts, ecc. sotto src/app/models/)

# 🟦 2. CREA I SERVIZI HTTP
```bash
ng g s services/product
ng g s services/user
ng g s services/category
ng g s services/purchase
```
(Crea i file dei servizi in src/app/services/)

# 🟧 3. CREA I COMPONENTI CRUD (LISTA + FORM) per ogni entità
Products
```bash
ng g c products-list --standalone
ng g c product-form --standalone
```
Users
```bash
ng g c users-list --standalone
ng g c user-form --standalone
```
Categories
```bash
ng g c categories-list --standalone
ng g c category-form --standalone
```
Purchases
```bash
ng g c purchases-list --standalone
ng g c purchase-form --standalone
```
# 🟨 4. MODIFICA IL ROUTER
Aggiungi le rotte nel file src/app/app.routes.ts
(Dopo aver generato i componenti, importa i componenti nei routes!)

```typescript
import { Routes } from '@angular/router';
import { CategoriesList } from './categories-list/categories-list';
import { CategoryForm } from './category-form/category-form';
import { ProductForm } from './product-form/product-form';
import { ProductsList } from './products-list/products-list';
import { PurchaseForm } from './purchase-form/purchase-form';
import { PurchasesList } from './purchases-list/purchases-list';
import { UserForm } from './user-form/user-form';
import { UsersList } from './users-list/users-list';

export const routes: Routes = [
  { path: '', redirectTo: 'products', pathMatch: 'full' },

  { path: 'products', component: ProductsList },
  { path: 'products/new', component: ProductForm },
  { path: 'products/:id', component: ProductForm },

  { path: 'users', component: UsersList },
  { path: 'users/new', component: UserForm },
  { path: 'users/:id', component: UserForm },

  { path: 'categories', component: CategoriesList },
  { path: 'categories/new', component: CategoryForm },
  { path: 'categories/:id', component: CategoryForm },

  { path: 'purchases', component: PurchasesList },
  { path: 'purchases/new', component: PurchaseForm },
  { path: 'purchases/:id', component: PurchaseForm }
];
```
# 🟪 5. AVVIA IL SERVER DI SVILUPPO
```bash
ng serve
```
# 📋 Ricapitolando — Copia/Incolla questa sequenza per scaffolding CRUD standalone Angular:
```bash
# MODELLI
ng g i models/product --type=model
ng g i models/user --type=model
ng g i models/category --type=model
ng g i models/purchase --type=model

# SERVIZI
ng g s services/product
ng g s services/user
ng g s services/category
ng g s services/purchase

# COMPONENTI CRUD
ng g c products-list --standalone
ng g c product-form --standalone

ng g c users-list --standalone
ng g c user-form --standalone

ng g c categories-list --standalone
ng g c category-form --standalone

ng g c purchases-list --standalone
ng g c purchase-form --standalone
```
TIP: Dopo ogni comando, Angular CLI aggiorna già i percorsi; tu dovrai solo collegare e importare tutto nel router, e scrivere la logica nei componenti.

# 📦 MODELLI
— src/app/models/

**1. product.model.ts**
```typescript

export interface Product {
  id: number;
  name: string;
  price: number;
  categoryId: number;
}

export interface ProductDTO {
  id: number;
  name: string;
  price: number;
  categoryName: string;
}
```
**2. user.model.ts**
```typescript

export interface Address {
  citta: string;
  via: string;
  cap: string;
}

export interface User {
  id: number;
  name: string;
  address: Address;
}
```
**3. category.model.ts**
```typescript

export interface Category {
  id: number;
  name: string;
}
```
**4. purchase.model.ts**
```typescript

export interface Purchase {
  id: number;
  userId: number;
  productId: number;
  quantity: number;
  purchaseDate: string;
}

export interface PurchaseDTO {
  id: number;
  userName: string;
  productName: string;
  productCategory: string;
  quantity: number;
  purchaseDate: string;
}
```
# 🛠️ SERVIZI — src/app/services/
Tutti i servizi usano HttpClient di Angular (assicurati di aver importato provideHttpClient() in main.ts).

**1. product.service.ts**
```typescript

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Product, ProductDTO } from '../models/product.model';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class ProductService {
  private apiUrl = 'http://localhost:5296/api/products'; // Modifica porta se diversa!

  constructor(private http: HttpClient) {}

  getAll(): Observable<ProductDTO[]> {
    return this.http.get<ProductDTO[]>(this.apiUrl);
  }

  get(id: number): Observable<ProductDTO> {
    return this.http.get<ProductDTO>(`${this.apiUrl}/${id}`);
  }

  add(product: Product): Observable<Product> {
    return this.http.post<Product>(this.apiUrl, product);
  }

  update(id: number, product: Product): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, product);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
```
**2. user.service.ts**
```typescript

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../models/user.model';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class UserService {
  private apiUrl = 'http://localhost:5296/api/users'; // Modifica porta se diversa!

  constructor(private http: HttpClient) {}

  getAll(): Observable<User[]> {
    return this.http.get<User[]>(this.apiUrl);
  }

  get(id: number): Observable<User> {
    return this.http.get<User>(`${this.apiUrl}/${id}`);
  }

  add(user: User): Observable<User> {
    return this.http.post<User>(this.apiUrl, user);
  }

  update(id: number, user: User): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, user);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
```
**3. category.service.ts**
```typescript

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Category } from '../models/category.model';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class CategoryService {
  private apiUrl = 'http://localhost:5296/api/categories'; // Modifica porta se diversa!

  constructor(private http: HttpClient) {}

  getAll(): Observable<Category[]> {
    return this.http.get<Category[]>(this.apiUrl);
  }

  get(id: number): Observable<Category> {
    return this.http.get<Category>(`${this.apiUrl}/${id}`);
  }

  add(category: Category): Observable<Category> {
    return this.http.post<Category>(this.apiUrl, category);
  }

  update(id: number, category: Category): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, category);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
```
**4. purchase.service.ts**
```typescript

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Purchase, PurchaseDTO } from '../models/purchase.model';
import { Observable } from 'rxjs';

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
```
Cambia l’apiUrl nei servizi se il backend gira su una porta diversa da 5296.

# 🛠️ componenti CRUD standalone Angular 20
i componenti CRUD standalone Angular 20 per tutte le entità (products, users, categories, purchases).

Ogni entità avrà:

- ListComponent (lista/visualizzazione + elimina)
- FormComponent (aggiungi/modifica)

# 1. Products
products-list.component.ts
```typescript

import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { ProductService } from '../services/product';
import { ProductDTO } from '../models/product.model';

@Component({
  selector: 'app-products-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <h2>Prodotti</h2>
    <button routerLink="/products/new">Aggiungi prodotto</button>
    <table>
      <tr>
        <th>Nome</th><th>Prezzo</th><th>Categoria</th><th>Azioni</th>
      </tr>
      <tr *ngFor="let p of products">
        <td>{{p.name}}</td>
        <td>{{p.price | currency:'EUR'}}</td>
        <td>{{p.categoryName}}</td>
        <td>
          <button [routerLink]="['/products', p.id]">Modifica</button>
          <button (click)="delete(p.id)">Elimina</button>
        </td>
      </tr>
    </table>
  `
})
export class ProductsList implements OnInit {
  products: ProductDTO[] = [];
  constructor(private service: ProductService) {}
  ngOnInit() { this.load(); }
  load() {
    this.service.getAll().subscribe(res => this.products = res);
  }
  delete(id: number) {
    if (confirm('Sicuro di eliminare?')) {
      this.service.delete(id).subscribe(() => this.load());
    }
  }
}
```
product-form.component.ts
```typescript

import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { Category } from '../models/category.model';
import { Product } from '../models/product.model';
import { CategoryService } from '../services/category';
import { ProductService } from '../services/product';

@Component({
  selector: 'app-product-form',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  template: `
    <h2>{{isNew ? 'Aggiungi' : 'Modifica'}} prodotto</h2>
    <form (ngSubmit)="save()" *ngIf="categories.length">
      <label>Nome: <input [(ngModel)]="model.name" name="name" required></label><br>
      <label>Prezzo: <input [(ngModel)]="model.price" name="price" type="number" required></label><br>
      <label>Categoria:
        <select [(ngModel)]="model.categoryId" name="categoryId" required>
          <option *ngFor="let cat of categories" [value]="cat.id">{{cat.name}}</option>
        </select>
      </label><br>
      <button type="submit">Salva</button>
      <button type="button" (click)="goBack()">Annulla</button>
    </form>
  `
})
export class ProductForm implements OnInit {
  model: Product = { id: 0, name: '', price: 0, categoryId: 0 };
  categories: Category[] = [];
  isNew = true;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private service: ProductService,
    private catService: CategoryService
  ) {}

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    this.catService.getAll().subscribe(cats => {
      this.categories = cats;
      if (id) {
        this.isNew = false;
        this.service.get(+id).subscribe(prod => {
          this.model = {
            id: prod.id,
            name: prod.name,
            price: prod.price,
            categoryId: this.categories.find(c => c.name === prod.categoryName)?.id || 0
          };
        });
      }
    });
  }

  save() {
    if (this.isNew) {
      this.service.add(this.model).subscribe(() => this.goBack());
    } else {
      this.service.update(this.model.id, this.model).subscribe(() => this.goBack());
    }
  }

  goBack() {
    this.router.navigate(['/products']);
  }
}
```
# 2. Users
users-list.component.ts
```typescript

import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { User } from '../models/user.model';
import { UserService } from '../services/user';

@Component({
  selector: 'app-users-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <h2>Utenti</h2>
    <button routerLink="/users/new">Aggiungi utente</button>
    <table>
      <tr>
        <th>Nome</th><th>Indirizzo</th><th>Azioni</th>
      </tr>
      <tr *ngFor="let u of users">
        <td>{{u.name}}</td>
        <td>{{u.address}}</td>
        <td>
          <button [routerLink]="['/users', u.id]">Modifica</button>
          <button (click)="delete(u.id)">Elimina</button>
        </td>
      </tr>
    </table>
  `
})
export class UsersList implements OnInit {
  users: User[] = [];
  constructor(private service: UserService) {}
  ngOnInit() { this.load(); }
  load() {
    this.service.getAll().subscribe(res => this.users = res);
  }
  delete(id: number) {
    if (confirm('Sicuro di eliminare?')) {
      this.service.delete(id).subscribe(() => this.load());
    }
  }
}
```
user-form.component.ts
```typescript

import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { User } from '../models/user.model';
import { UserService } from '../services/user';

@Component({
  selector: 'app-user-form',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  template: `
    <h2>{{isNew ? 'Aggiungi' : 'Modifica'}} utente</h2>
    <form (ngSubmit)="save()">
      <label>Nome: <input [(ngModel)]="model.name" name="name" required></label><br>
      <label>Città: <input [(ngModel)]="model.address.citta" name="addressCitta" required></label><br>
      <label>Via: <input [(ngModel)]="model.address.via" name="addressVia" required></label><br>
      <label>CAP: <input [(ngModel)]="model.address.cap" name="addressCap" required></label><br>
      <button type="submit">Salva</button>
      <button type="button" (click)="goBack()">Annulla</button>
    </form>
  `
})
export class UserForm implements OnInit {
  model: User = {
  id: 0,
  name: '',
  address: { citta: '', via: '', cap: '' }
  };
  isNew = true;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private service: UserService
  ) {}

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isNew = false;
      this.service.get(+id).subscribe(u => this.model = u);
    }
  }

  save() {
    if (this.isNew) {
      this.service.add(this.model).subscribe(() => this.goBack());
    } else {
      this.service.update(this.model.id, this.model).subscribe(() => this.goBack());
    }
  }

  goBack() {
    this.router.navigate(['/users']);
  }
}
```
3. Categories
categories-list.component.ts
```typescript

import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Category } from '../models/category.model';
import { CategoryService } from '../services/category';

@Component({
  selector: 'app-categories-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <h2>Categorie</h2>
    <button routerLink="/categories/new">Aggiungi categoria</button>
    <table>
      <tr>
        <th>Nome</th><th>Azioni</th>
      </tr>
      <tr *ngFor="let c of categories">
        <td>{{c.name}}</td>
        <td>
          <button [routerLink]="['/categories', c.id]">Modifica</button>
          <button (click)="delete(c.id)">Elimina</button>
        </td>
      </tr>
    </table>
  `
})
export class CategoriesList implements OnInit {
  categories: Category[] = [];
  constructor(private service: CategoryService) {}
  ngOnInit() { this.load(); }
  load() {
    this.service.getAll().subscribe(res => this.categories = res);
  }
  delete(id: number) {
    if (confirm('Sicuro di eliminare?')) {
      this.service.delete(id).subscribe(() => this.load());
    }
  }
}
```
category-form.component.ts
```typescript

import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { Category } from '../models/category.model';
import { CategoryService } from '../services/category';

@Component({
  selector: 'app-category-form',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  template: `
    <h2>{{isNew ? 'Aggiungi' : 'Modifica'}} categoria</h2>
    <form (ngSubmit)="save()">
      <label>Nome: <input [(ngModel)]="model.name" name="name" required></label><br>
      <button type="submit">Salva</button>
      <button type="button" (click)="goBack()">Annulla</button>
    </form>
  `
})
export class CategoryForm implements OnInit {
  model: Category = { id: 0, name: '' };
  isNew = true;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private service: CategoryService
  ) {}

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isNew = false;
      this.service.get(+id).subscribe(c => this.model = c);
    }
  }

  save() {
    if (this.isNew) {
      this.service.add(this.model).subscribe(() => this.goBack());
    } else {
      this.service.update(this.model.id, this.model).subscribe(() => this.goBack());
    }
  }

  goBack() {
    this.router.navigate(['/categories']);
  }
}
```
4. Purchases
purchases-list.component.ts
```typescript

import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { PurchaseDTO } from '../models/purchase.model';
import { PurchaseService } from '../services/purchase';

@Component({
  selector: 'app-purchases-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <h2>Acquisti</h2>
    <button routerLink="/purchases/new">Aggiungi acquisto</button>
    <table>
      <tr>
        <th>Utente</th><th>Prodotto</th><th>Categoria</th><th>Quantità</th><th>Data</th><th>Azioni</th>
      </tr>
      <tr *ngFor="let p of purchases">
        <td>{{p.userName}}</td>
        <td>{{p.productName}}</td>
        <td>{{p.productCategory}}</td>
        <td>{{p.quantity}}</td>
        <td>{{p.purchaseDate | date:'shortDate'}}</td>
        <td>
          <button [routerLink]="['/purchases', p.id]">Modifica</button>
          <button (click)="delete(p.id)">Elimina</button>
        </td>
      </tr>
    </table>
  `
})
export class PurchasesList implements OnInit {
  purchases: PurchaseDTO[] = [];
  constructor(private service: PurchaseService) {}
  ngOnInit() { this.load(); }
  load() {
    this.service.getAll().subscribe(res => this.purchases = res);
  }
  delete(id: number) {
    if (confirm('Sicuro di eliminare?')) {
      this.service.delete(id).subscribe(() => this.load());
    }
  }
}
```
purchase-form.component.ts
```typescript

import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
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
  imports: [CommonModule, FormsModule, RouterModule],
  template: `
    <h2>{{isNew ? 'Aggiungi' : 'Modifica'}} acquisto</h2>
    <form (ngSubmit)="save()" *ngIf="users.length && products.length">
      <label>Utente:
        <select [(ngModel)]="model.userId" name="userId" required>
          <option *ngFor="let u of users" [value]="u.id">{{u.name}}</option>
        </select>
      </label><br>
      <label>Prodotto:
        <select [(ngModel)]="model.productId" name="productId" required>
          <option *ngFor="let p of products" [value]="p.id">{{p.name}}</option>
        </select>
      </label><br>
      <label>Quantità: <input [(ngModel)]="model.quantity" name="quantity" type="number" required></label><br>
      <label>Data: <input [(ngModel)]="model.purchaseDate" name="purchaseDate" type="date" required></label><br>
      <button type="submit">Salva</button>
      <button type="button" (click)="goBack()">Annulla</button>
    </form>
  `
})
export class PurchaseForm implements OnInit {
  model: Purchase = { id: 0, userId: 0, productId: 0, quantity: 1, purchaseDate: '' };
  users: User[] = [];
  products: Product[] = [];
  isNew = true;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private service: PurchaseService,
    private userService: UserService,
    private productService: ProductService
  ) {}

  ngOnInit() {
    this.userService.getAll().subscribe(u => this.users = u);
    this.productService.getAll().subscribe(p => this.products = p.map(pr => ({
      id: pr.id, name: pr.name, price: pr.price, categoryId: 0 // categoryId non necessario qui
    })));

    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isNew = false;
      this.service.get(+id).subscribe(a => this.model = a);
    }
  }

  save() {
    if (this.isNew) {
      this.service.add(this.model).subscribe(() => this.goBack());
    } else {
      this.service.update(this.model.id, this.model).subscribe(() => this.goBack());
    }
  }

  goBack() {
    this.router.navigate(['/purchases']);
  }
}
```
# 📁 STRUTTURA SUGGERITA (nella cartella src/app/)
```bash
src/
  app/
    models/
      product.model.ts
      user.model.ts
      category.model.ts
      purchase.model.ts
    services/
      product.service.ts
      user.service.ts
      category.service.ts
      purchase.service.ts
    products-list.component.ts
    product-form.component.ts
    users-list.component.ts
    user-form.component.ts
    categories-list.component.ts
    category-form.component.ts
    purchases-list.component.ts
    purchase-form.component.ts
    app.routes.ts
    app.component.ts
  main.ts
  styles.css
```
**1. src/app/app.routes.ts**
```typescript
import { Routes } from '@angular/router';
import { CategoriesList } from './categories-list/categories-list';
import { CategoryForm } from './category-form/category-form';
import { ProductForm } from './product-form/product-form';
import { ProductsList } from './products-list/products-list';
import { PurchaseForm } from './purchase-form/purchase-form';
import { PurchasesList } from './purchases-list/purchases-list';
import { UserForm } from './user-form/user-form';
import { UsersList } from './users-list/users-list';

export const routes: Routes = [
  { path: '', redirectTo: 'products', pathMatch: 'full' },

  { path: 'products', component: ProductsList },
  { path: 'products/new', component: ProductForm },
  { path: 'products/:id', component: ProductForm },

  { path: 'users', component: UsersList },
  { path: 'users/new', component: UserForm },
  { path: 'users/:id', component: UserForm },

  { path: 'categories', component: CategoriesList },
  { path: 'categories/new', component: CategoryForm },
  { path: 'categories/:id', component: CategoryForm },

  { path: 'purchases', component: PurchasesList },
  { path: 'purchases/new', component: PurchaseForm },
  { path: 'purchases/:id', component: PurchaseForm }
];
```
**2. src/app/app.component.ts**
```typescript
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterModule],
  template: `
    <nav>
      <a routerLink="/products" routerLinkActive="active">Prodotti</a>
      <a routerLink="/categories" routerLinkActive="active">Categorie</a>
      <a routerLink="/users" routerLinkActive="active">Utenti</a>
      <a routerLink="/purchases" routerLinkActive="active">Acquisti</a>
    </nav>
    <main>
      <router-outlet></router-outlet>
    </main>
  `,
  styles: [`
    nav {
      background: #eee;
      padding: 1em;
      margin-bottom: 2em;
      display: flex;
      gap: 2em;
    }
    nav a.active {
      font-weight: bold;
      color: #007bff;
      text-decoration: underline;
    }
    main { padding: 1em; }
  `]
})
export class AppComponent {}
```
**3. src/main.ts**
```typescript
import { provideHttpClient } from '@angular/common/http';
import { bootstrapApplication } from '@angular/platform-browser';
import { provideRouter } from '@angular/router';
import { AppComponent } from './app/app.component';
import { routes } from './app/app.routes';

bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(routes),
    provideHttpClient()
  ]
});
```
**4. src/styles.css**
```css
body {
  font-family: 'Segoe UI', Arial, sans-serif;
  background: #fafbfc;
  margin: 0;
}

table {
  width: 100%;
  border-collapse: collapse;
  margin: 1em 0;
  background: #fff;
}

th, td {
  border: 1px solid #e3e3e3;
  padding: 0.5em 1em;
  text-align: left;
}

th {
  background: #f5f5f5;
}

button, input, select {
  margin: 0.25em;
  padding: 0.25em 0.5em;
}
```
Il backend deve rispondere da http://localhost:5296 (modifica l’URL nei servizi se usi altra porta).

**5. Comando per lanciare tutto:**
```bash
ng serve
```
(Se hai tutto nella cartella giusta, l’app funziona subito.)

7. Debug/Check
Se ti esce qualche errore tipo:

"Can't bind to 'routerLink'..."
→ Assicurati che ogni componente standalone abbia tra gli imports almeno RouterModule.

"FormsModule missing"
→ Ogni form standalone deve avere FormsModule tra gli imports.

# Import

Per usare i componenti standalone in altri componenti o moduli, devi importarli esplicitamente.
Quando sposti i componenti in cartelle diverse, i percorsi di importazione dovrebbero adattarsi automaticamente.

- Prova a spostare le pagine in una cartella `pages/` e aggiorna gli import nei router.

# Miglioramenti grafici, nuove funzionalità

# 🟢 Suggerimenti Grafici (UI/UX)
**1. Usa una libreria di componenti**

Angular Material: la scelta più semplice e integrata per Angular.

Comando:

```bash
ng add @angular/material
```
Ottieni: bottoni carini, tabelle sortable, card, snackbar, dialog, input form avanzati, icone, ecc.

Alternative: PrimeNG, NG-ZORRO, Bootstrap.

**2. Rendi responsive il layout**

Usa un container centrale, padding e media queries nel tuo CSS o tramite le griglie/flessbox di Material.

**3. Migliora le tabelle**

Tabelle con ricerca (search bar), ordinamento per colonne e paginazione.

Material Table lo fa già nativamente

Alterna righe colorate (“zebra”) per lettura facile.

**4. Dialog di conferma**

 posto dei semplici confirm('Sicuro?'), usa un dialog moderno di conferma per eliminazioni (Angular Material MatDialog).

**5. Snackbar/Toast di successo/errore**

Mostra messaggi in basso ("Salvataggio riuscito", "Eliminazione eseguita", ecc.) dopo ogni azione, non solo alert.

**6. Form Validati**

Aggiungi validazione real-time, messaggi di errore sotto ai campi, highlight rosso se invalidi.

Disabilita il submit se il form non è valido.

**7. Visualizza lo stato di caricamento**

Metti uno spinner/loader durante le chiamate HTTP, per UX migliore.

**8. Dark Mode**

Permetti il cambio tema chiaro/scuro!

**9. Icone**

Aggiungi icone agli action button, titoli, ecc.
(Material ha un set enorme!)

**10. Header/Sidebar**

Metti una barra in alto (con titolo/app name) o una sidebar con navigazione più elegante.

# 🟡 Suggerimenti di Funzionalità
**1. Filtri avanzati**

Filtra prodotti per categoria/prezzo.

Filtra acquisti per utente/data.

**2. Export CSV/Excel**

Permetti di esportare la tabella attuale in CSV o Excel.

**3. Statistiche/Dashboard**

Grafici (con ng2-charts o [Chart.js]) su:

Spesa totale per utente

Numero acquisti per prodotto/categoria

Grafico a torta prodotti più venduti

**4. Autenticazione**

Login base (falso o vero): accesso riservato ad alcune funzionalità solo da utenti “admin”.

**5. Gestione dettagliata dell’indirizzo**

Mostra la mappa (Google Maps/OpenStreetMap) dell’indirizzo utente.

Autocomplete sulla città/via.

**6. CRUD inline**

Modifica prodotti o utenti direttamente dalla tabella (senza entrare nella form separata).

**7. Cronologia operazioni**

Log operazioni CRUD per ogni entità, magari anche con “undo”.

**8. Modalità mobile**

UI adattiva (Material lo fa, ma puoi migliorare con breakpoints personalizzati).

# Starter packIntegrare Angular Material nel tuo progetto

Rendere responsive il layout

Migliorare le tabelle usando Material (<mat-table>), con ordinamento, ricerca e paginazione

## 🟢 1. Installa Angular Material
Esegui nel terminale:

```bash
ng add @angular/material
```
Scegli un tema (anche il default va benissimo).

## 🟢 2. Aggiorna AppComponent per layout Material responsive
Modifica il file src/app/app.component.ts:

```typescript
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { BreakpointObserver, LayoutModule } from '@angular/cdk/layout';

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
```
## 🟢 1. Prodotti
Crea/aggiorna src/app/products-list/products-list.ts (standalone):

```typescript
import { CommonModule } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { RouterModule } from '@angular/router';
import { ProductDTO } from '../models/product.model';
import { ProductService } from '../services/product';

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

    <div class="table-wrapper mat-elevation-z2">
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

  constructor(private service: ProductService) {}

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

  delete(id: number) {
    if (!confirm('Eliminare prodotto?')) return;
    this.service.delete(id).subscribe(() => this.load());
  }
}
```
src/app/product-form/product-form.ts

```typescript
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RouterModule, ActivatedRoute, Router } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule }     from '@angular/material/input';
import { MatSelectModule }    from '@angular/material/select';
import { MatButtonModule }    from '@angular/material/button';
import { MatIconModule }      from '@angular/material/icon';
import { MatCardModule }      from '@angular/material/card';

import { ProductService } from '../services/product.service';
import { CategoryService } from '../services/category.service';
import { Category }        from '../models/category.model';
import { Product }         from '../models/product.model';

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
```
## 2. Users
users-list/users-list.ts
```typescript
import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RouterModule } from '@angular/router';
import { UserService } from '../services/user.service';
import { User } from '../models/user.model';

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

    <div class="table-wrapper mat-elevation-z2">
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

  constructor(private service: UserService) {}

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

  delete(id: number) {
    if (!confirm('Eliminare utente?')) return;
    this.service.delete(id).subscribe(() => this.load());
  }
}
```
user-form/user-form.ts
```typescript
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators, FormGroup } from '@angular/forms';
import { RouterModule, ActivatedRoute, Router } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule }     from '@angular/material/input';
import { MatButtonModule }    from '@angular/material/button';
import { MatIconModule }      from '@angular/material/icon';
import { MatCardModule }      from '@angular/material/card';

import { UserService } from '../services/user.service';
import { User } from '../models/user.model';

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
```
## 3. Categories
categories-list/categories-list.ts
```typescript
import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RouterModule } from '@angular/router';
import { CategoryService } from '../services/category.service';
import { Category } from '../models/category.model';

@Component({
  selector: 'app-categories-list',
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
    <div class="header">
      <h2>Categorie</h2>
      <button mat-flat-button color="primary" routerLink="/categories/new">
        <mat-icon>add</mat-icon> Nuova
      </button>
    </div>

    <mat-form-field appearance="outline" class="filter">
      <mat-label>Cerca categoria</mat-label>
      <input matInput (keyup)="applyFilter($event)" placeholder="Nome">
    </mat-form-field>

    <div class="table-wrapper mat-elevation-z2">
      <table mat-table [dataSource]="dataSource" matSort class="full-width">
        <ng-container matColumnDef="name">
          <th mat-header-cell *matHeaderCellDef mat-sort-header>Nome</th>
          <td mat-cell *matCellDef="let c">{{ c.name }}</td>
        </ng-container>

        <ng-container matColumnDef="actions">
          <th mat-header-cell *matHeaderCellDef>Azioni</th>
          <td mat-cell *matCellDef="let c">
            <button mat-icon-button color="primary" [routerLink]="['/categories', c.id]">
              <mat-icon>edit</mat-icon>
            </button>
            <button mat-icon-button color="warn" (click)="delete(c.id)">
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
export class CategoriesList implements OnInit {
  cols = ['name', 'actions'];
  dataSource = new MatTableDataSource<Category>([]);

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private service: CategoryService) {}

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

  delete(id: number) {
    if (!confirm('Eliminare categoria?')) return;
    this.service.delete(id).subscribe(() => this.load());
  }
}
```
category-form/category-form.ts
```typescript
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
```
## 4. Purchases
purchases-list/purchases-list.ts
```typescript
import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RouterModule } from '@angular/router';
import { PurchaseService } from '../services/purchase.service';
import { PurchaseDTO } from '../models/purchase.model';

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

    <div class="table-wrapper mat-elevation-z2">
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

  constructor(private service: PurchaseService) {}

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

  delete(id: number) {
    if (!confirm('Eliminare acquisto?')) return;
    this.service.delete(id).subscribe(() => this.load());
  }
}
```
purchase-form/purchase-form.ts
```typescript
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators, FormGroup } from '@angular/forms';
import { RouterModule, ActivatedRoute, Router } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule }     from '@angular/material/input';
import { MatSelectModule }    from '@angular/material/select';
import { MatButtonModule }    from '@angular/material/button';
import { MatIconModule }      from '@angular/material/icon';
import { MatCardModule }      from '@angular/material/card';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';

import { PurchaseService } from '../services/purchase.service';
import { UserService } from '../services/user.service';
import { ProductService } from '../services/product.service';
import { Purchase } from '../models/purchase.model';
import { User } from '../models/user.model';
import { Product } from '../models/product.model';

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
```
**Spiegazione rapida**

ReactiveFormsModule + FormBuilder per gestire i form group e le validazioni.

<mat-form-field> con appearance="outline" per ogni campo.

matInput, mat-select, mat-button, mat-icon per la UI Material.

Validators.required e Validators.min abilitano le mat-error.

Responsive: la card è centrata e larga al massimo 500px, si adatta ai dispositivi.

Puoi replicare esattamente questo schema per Users, Categories e Purchases cambiando i campi del form e i servizi usati.

## 🟢 5. CSS globale
Aggiungi in src/styles.css:

```css
html, body { height: 100%; }
body { margin: 0; background: #f7f8fa; }
mat-toolbar { position: sticky; top: 0; z-index: 10; }
.mat-elevation-z2 { margin: 0 auto 1.5em auto; max-width: 1100px; }
@media (max-width: 800px) {
  .main-container, .mat-elevation-z2 { padding: 0; }
}
```
## 🟢 6. Altri dettagli
- SnackbarService riutilizzabile per feedback.
- Un ConfirmationDialogComponent con MatDialog.
- Un sistema di toggle light/dark persistente (localStorage) con Angular Material.

**1. SnackbarService Matdialog**
Import comuni da mettere in cima a ciascun componente list (es. products-list.ts, users-list.ts, ecc.)
```ts
import { MatDialog } from '@angular/material/dialog';
import { firstValueFrom } from 'rxjs';
import { ConfirmDialogComponent, ConfirmDialogData } from './confirm-dialog';
import { SnackbarService } from '../services/snackbar';
```
Adatta il path ../ se il list component è in una sottocartella diversa (es. ./pages/...).

## **1. ProductsListComponent – delete**
Nel costruttore aggiungi:

```ts
constructor(
  private service: ProductService,
  private dialog: MatDialog,
  private snackbar: SnackbarService
) {}
```
Metodo delete:

```ts
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

  const confirmed: boolean | undefined = await firstValueFrom(dialogRef.afterClosed());
  if (!confirmed) return;

  this.service.delete(id).subscribe({
    next: () => this.snackbar.success('Prodotto eliminato con successo'),
    error: () => this.snackbar.error('Errore durante l\'eliminazione')
  });
}
```
## **2. UsersListComponent – delete**
Costruttore:

```ts
constructor(
  private service: UserService,
  private dialog: MatDialog,
  private snackbar: SnackbarService
) {}
```
Metodo:

```ts
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
```
## **3. CategoriesListComponent – delete**
Costruttore:

```ts
constructor(
  private service: CategoryService,
  private dialog: MatDialog,
  private snackbar: SnackbarService
) {}
```
Metodo:

```ts
async delete(id: number) {
  const dialogRef = this.dialog.open(ConfirmDialogComponent, {
    width: '360px',
    data: {
      title: 'Elimina categoria',
      message: 'Sei sicuro di voler eliminare questa categoria?',
      confirmText: 'Elimina',
      cancelText: 'Annulla'
    } as ConfirmDialogData,
    disableClose: true
  });

  const confirmed: boolean | undefined = await firstValueFrom(dialogRef.afterClosed());
  if (!confirmed) return;

  this.service.delete(id).subscribe({
    next: () => this.snackbar.success('Categoria eliminata'),
    error: () => this.snackbar.error('Errore durante eliminazione categoria')
  });
}
```
## **4. PurchasesListComponent – delete**
Costruttore:

```ts
constructor(
  private service: PurchaseService,
  private dialog: MatDialog,
  private snackbar: SnackbarService
) {}
```
Metodo:

```ts
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
```
## **3. Tema Light / Dark Toggle**
A. Service per tema (src/app/services/theme.service.ts)
```typescript
import { Injectable } from '@angular/core';
import { OverlayContainer } from '@angular/cdk/overlay';

const STORAGE_KEY = 'theme-mode';

@Injectable({ providedIn: 'root' })
export class ThemeService {
  private darkClass = 'dark-theme';

  constructor(private overlay: OverlayContainer) {
    const stored = localStorage.getItem(STORAGE_KEY);
    if (stored === 'dark') {
      this.enableDark();
    } else {
      this.enableLight();
    }
  }

  isDark(): boolean {
    return document.body.classList.contains(this.darkClass);
  }

  toggle() {
    if (this.isDark()) this.enableLight();
    else this.enableDark();
  }

  enableDark() {
    document.body.classList.add(this.darkClass);
    this.overlay.getContainerElement().classList.add(this.darkClass);
    localStorage.setItem(STORAGE_KEY, 'dark');
  }

  enableLight() {
    document.body.classList.remove(this.darkClass);
    this.overlay.getContainerElement().classList.remove(this.darkClass);
    localStorage.setItem(STORAGE_KEY, 'light');
  }
}
```
B. Stili tema (aggiungi a src/styles.css o meglio in un file SCSS se usi)
```css
/* default light variables se usi Material theming via classi */
.dark-theme {
  --bg: #1f2530;
  --surface: #2a2f44;
  --on-surface: #f0f2f7;
  --card: #2f364f;
}

/* sovrascrivi qualche cosa per contrasto */
body {
  background: white;
  color: #1e1e1e;
}
body.dark-theme {
  background: #1f2530;
  color: var(--on-surface);
}

.mat-toolbar {
  position: sticky;
  top: 0;
  z-index: 10;
}

/* se usi componenti overlay bisogna anche gestire sfondo */
.dark-theme .mat-card {
  background: var(--card);
  color: var(--on-surface);
}
```
Nota: se usi un tema Material precompilato, puoi applicare la classe .dark-theme e i CSS si adatteranno se il tema è configurato; per soluzioni più avanzate usa @use '@angular/material' con scss e due schemi.

C. Pulsante toggle in AppComponent
Aggiorna app.component.ts (aggiungi modulo icone e bottoni già importati):

```html
<mat-toolbar color="primary">
  <button mat-icon-button (click)="drawer.toggle()" class="hide-desktop">
    <mat-icon>menu</mat-icon>
  </button>
  <span>MiniApp CRUD</span>
  <span class="spacer"></span>
  <button mat-icon-button (click)="theme.toggle()" [attr.aria-label]="'Toggle tema'">
    <mat-icon>{{ theme.isDark() ? 'light_mode' : 'dark_mode' }}</mat-icon>
  </button>
</mat-toolbar>
```
E nel TypeScript:

```ts
import { ThemeService } from '../services/theme.service';

constructor(breakpoints: BreakpointObserver, public theme: ThemeService) {
  breakpoints.observe(['(max-width: 800px)']).subscribe(res => {
    this.isMobile = res.matches;
  });
}
```
## **4. Assicurati di importare nei componenti / bootstrap**
Nel main.ts hai già:

```ts
bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(routes),
    provideHttpClient(),
    importProvidersFrom(MatSnackBarModule, MatDialogModule) // se serve globalmente
  ]
});
```
Oppure importa localmente nei componenti che usano snack/dialog.

5. Esempio rapido di flusso
Eliminazione: apri ConfirmDialogComponent, se confermato chiami ProductService.delete, poi SnackbarService.success(...).

Creazione/modifica: form valida con Material, al salvataggio mostra snackbar.success("Salvato") o error.

Tema: l’utente clicca l’icona nel toolbar e fa toggle persistente.

Se vuoi ti preparo:

Varianti del ConfirmDialog con input (es. “scrivi DELETE per confermare”)

Un wrap service che unifica dialog+snackbar per pattern standard

Integrazione nei componenti già esistenti (ti genero i diff)

Quale preferisci dopo?