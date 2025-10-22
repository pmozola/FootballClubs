import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { apiUrls } from '../http/urls';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient, private router: Router) { }

  login(request: LoginRequestModel) {
    return this.http.post(apiUrls.auth.login, request).pipe(
      tap((res: any) => {
        localStorage.setItem('token', res.token);
        this.router.navigate(['folder/inbox']);
      })
    );
  }

  register(request: RegisterRequestModel) {
    return this.http.post(apiUrls.auth.register, request);
  }

  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }
}
export interface LoginRequestModel{
    email:string,
    password:string
}

export interface RegisterRequestModel{
    email:string,
    password:string
}