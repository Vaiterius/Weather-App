import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable, tap } from 'rxjs';
import { LoginRequest } from './login-request';
import { LoginResult } from './login-result';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public tokenKey: string = 'tokenKey';

  constructor(protected http: HttpClient) { }
  
  public login(loginRequest: LoginRequest): Observable<LoginResult> {
    let url = `${environment.baseUrl}/api/admin/login`;
    return this.http.post<LoginResult>(url, loginRequest)
      .pipe(tap(loginResult => {
        if (loginResult.success) {
          localStorage.setItem(this.tokenKey, loginResult.token);
        }
      }));
  }

  public getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }
}
