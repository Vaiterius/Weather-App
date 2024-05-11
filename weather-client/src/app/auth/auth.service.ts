import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { BehaviorSubject, Observable, tap } from 'rxjs';
import { LoginRequest } from './login-request';
import { LoginResult } from './login-result';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public tokenKey: string = 'tokenKey';

  private _authStatus = new BehaviorSubject<boolean>(false);
  public authStatus = this._authStatus.asObservable();

  constructor(protected http: HttpClient) { }

  public init(): void {
    if (this.isAuthenticated()) {
      this.setAuthStatus(true);
    }
  }

  private setAuthStatus(isAuthenticated: boolean): void {
    this._authStatus.next(isAuthenticated);
  }

  public isAuthenticated(): boolean {
    return this.getToken() !== null;
  }
  
  public login(loginRequest: LoginRequest): Observable<LoginResult> {
    let url = `${environment.baseUrl}/api/admin/login`;
    return this.http.post<LoginResult>(url, loginRequest)
      .pipe(tap(loginResult => {
        if (loginResult.success) {
          localStorage.setItem(this.tokenKey, loginResult.token);
          this.setAuthStatus(true);
        }
      }));
  }

  public getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  public logout(): void {
    localStorage.removeItem(this.tokenKey);
    this.setAuthStatus(false);
  }
}
