import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import Swal from 'sweetalert2';
import { environment } from '../environment/environment';
import { ILoggedInUser, ILoginForm, ISignupForm, User } from '../interfaces_integration/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
 private baseUrl = environment.BaseURL;

  constructor(private http: HttpClient) { }

  signup(user: ISignupForm): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/Auth/register`, user);
  }

  login(credentials: ILoginForm): Observable<ILoggedInUser> {
    return this.http.post<ILoggedInUser>(`${this.baseUrl}/Auth/login`, credentials);
  }
}
