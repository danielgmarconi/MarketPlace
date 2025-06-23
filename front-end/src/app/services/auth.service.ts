import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { Authentication } from '../models/authentication';
import { environment } from '../../environments/environment';
import { MethodResponse } from '../models/method-response';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private tokenKey = 'token';
  constructor(private http: HttpClient) { }

  login(model : Authentication): Observable<any> {
    return this.http.post(environment.webapi + '/Authorization/Authentication', model).pipe(
      tap((res: any) => {
        const methodResponse :MethodResponse = res;
        localStorage.setItem(this.tokenKey, methodResponse.response);
      })
    );
  }
  logout() {
    localStorage.removeItem(this.tokenKey);
  }
  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }
  isTokenExpired(): boolean {
    try
    {
      const token = this.getToken() ?? '';
      const { exp } = JSON.parse(atob(token.split('.')[1]));
      return Math.floor(Date.now() / 1000) >= exp;
    }
    catch
    {
      return true;
    }
  }
  isAuthenticated(): boolean {
    const token = this.getToken();
    return token !== null && !this.isTokenExpired();
  }
}
