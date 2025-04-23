import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';

export const ROUTES: Routes = [
  {
    path: 'auth',
    loadChildren: () =>
      import('./features/auth/auth.routes').then((mod) => mod.AUTH_ROUTES),
  },
  {
    path: '',
    canActivate: [authGuard],
    loadChildren: () =>
      import('./features/panel/panel.routes').then((mod) => mod.PANEL_ROUTES),
  },
];
