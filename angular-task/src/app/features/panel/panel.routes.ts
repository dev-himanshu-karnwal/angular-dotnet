import { Routes } from '@angular/router';
import { PanelLayoutComponent } from '../../layouts/panel-layout/panel-layout.component';
import { DashboardPage } from './pages/dashboard/dashboard.page';

export const PANEL_ROUTES: Routes = [
  {
    path: '',
    component: PanelLayoutComponent,
    children: [
      {
        path: '',
        redirectTo: 'dashboard',
        pathMatch: 'full',
      },
      {
        path: 'dashboard',
        component: DashboardPage,
        title: 'Dashboard',
      },
    ],
  },
];
