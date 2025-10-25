import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { applicationRoutes } from '../app.routes';
import { MenuController } from '@ionic/angular/standalone';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(
    private router: Router, private menuCtrl: MenuController,
  ) {}
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {

      const token = localStorage.getItem('token')
      if(token){
        this.menuCtrl.enable(true)
        return true;
      }

      this.router.navigate([applicationRoutes.auth.landing]);
      return false;
  }
}