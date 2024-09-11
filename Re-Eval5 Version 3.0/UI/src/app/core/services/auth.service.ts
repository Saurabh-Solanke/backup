import { Injectable } from '@angular/core';
import { environment } from '../environment/environment';
import { HttpClient } from '@angular/common/http';
import { ILoggedInUser, ILoginForm, ISignupForm } from '../interfaces/user';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private baseUrl = environment.BaseUrl;
  constructor(private http: HttpClient) { }

  signup(user: ISignupForm): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/Auth/RegisterUser`, user);
  }

  login(credentials: ILoginForm): Observable<ILoggedInUser> {
    return this.http.post<ILoggedInUser>(`${this.baseUrl}/Auth/Login`, credentials);
  }
}
