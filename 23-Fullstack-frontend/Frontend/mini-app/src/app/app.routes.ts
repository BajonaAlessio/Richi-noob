import { Routes } from '@angular/router';
import { CategoriesList } from './pages/categories-list';
import { CategoryForm } from './pages/category-form';
import { ProductForm } from './pages/product-form';
import { ProductsList } from './pages/products-list';
import { PurchaseForm } from './pages/purchase-form';
import { PurchasesList } from './pages/purchases-list';
import { UserForm } from './pages/user-form';
import { UsersList } from './pages/users-list';
/*
Questo file definisce le rotte dell'applicazione Angular.
Le rotte sono usate per navigare tra le diverse pagine dell'applicazione.
Ogni rotta è associata a un componente specifico che viene visualizzato quando l'utente naviga verso quella rotta.
Le rotte sono definite come un array di oggetti, dove ogni oggetto rappresenta una rotta con un percorso (path) e un componente associato.
*/
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