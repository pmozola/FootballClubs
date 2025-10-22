import { Routes } from '@angular/router';
import { AuthGuard } from './guards/auth.guard';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'folder/inbox',
    pathMatch: 'full',
  },
  {
    path: 'folder/:id',
    canActivate:  [AuthGuard],
    loadComponent: () =>
      import('./folder/folder.page').then((m) => m.FolderPage),
  },
  {
    path: 'auth/login',
    loadComponent: () => import('./auth/login/login.page').then( m => m.LoginPage)
  },
  {
    path: 'auth/register',
    loadComponent: () => import('./auth/register/register.page').then( m => m.RegisterPage)
  },
  {
    path: 'auth/landing',
    loadComponent: () => import('./auth/landing/landing.page').then( m => m.LandingPage)
  },
];

export const applicationRoutes = {
    auth:{
        register: 'auth/register',
        login: 'auth/login',
        landing: 'auth/landing'
    },
    main:'',

}