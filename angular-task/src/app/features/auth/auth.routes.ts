import { Routes } from '@angular/router';
import { AuthLayoutComponent } from '../../layouts/auth-layout/auth-layout.component';
import { LoginPage } from './pages/login/login.page';
import { SignupPage } from './pages/signup/signup.page';

export const AUTH_ROUTES: Routes = [
  {
    path: '',
    component: AuthLayoutComponent,
    children: [
      {
        path: '',
        redirectTo: 'login',
        pathMatch: 'full',
      },
      {
        path: 'login',
        component: LoginPage,
        title: 'Login',
      },
      {
        path: 'signup',
        component: SignupPage,
        title: 'Login',
      },
    ],
  },
];
