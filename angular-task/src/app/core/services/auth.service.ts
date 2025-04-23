import { inject, Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs';

import {
  type ILoginPayload,
  type IFetchUserResponse,
  type IloginResponse,
  type ISignupPayload,
} from '../../features/auth/auth.model';
import { type IUser } from '../../shared/models/user.model';

const JWT_TOKEN_KEY = 'authToken';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly http = inject(HttpClient);
  private readonly _user = signal<IUser | null>(null);
  readonly user = this._user.asReadonly();

  fetchAuthUser() {
    return this.http.get<IFetchUserResponse>('/users/me').pipe(
      tap((res) => {
        this._user.set(res.data);
      })
    );
  }

  private setToken(token: string) {
    localStorage.setItem(JWT_TOKEN_KEY, token);
  }

  private removeToken() {
    localStorage.removeItem(JWT_TOKEN_KEY);
  }

  login(payload: ILoginPayload) {
    return this.http.post<IloginResponse>('/api/Account/login', payload).pipe(
      tap((res) => {
        this.setToken(res.token);
        this._user.set(res.data);
      })
    );
  }

  signup(payload: ISignupPayload) {
    return this.http
      .post<IloginResponse>('/api/Account/register', payload)
      .pipe(
        tap((res) => {
          this.setToken(res.token);
          this._user.set(res.data);
        })
      );
  }

  logout() {
    return this.http.delete(`/users/logout`).pipe(
      tap(() => {
        this.removeToken();
      })
    );
  }
}
