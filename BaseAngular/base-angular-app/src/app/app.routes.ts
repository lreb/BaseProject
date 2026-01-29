import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/home',
    pathMatch: 'full'
  },
  {
    path: 'home',
    component: HomeComponent
  },
  // Agregar aquí más rutas de features
  // Ejemplo de lazy loading:
  // {
  //   path: 'users',
  //   loadComponent: () => import('./features/users/users.component').then(m => m.UsersComponent)
  // }
];
