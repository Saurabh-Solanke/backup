import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { LoggedInUser } from '../models/loggedinuser.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl = 'http://localhost:5172/api/Auth';
  private token: string | null = null;

  constructor(private http: HttpClient, private router: Router) {
    this.token = localStorage.getItem('passport_token');
  }

  get isLoggedIn(): boolean {
    return this.token !== null;
  }

  login(email: string, password: string): Observable<LoggedInUser> {
    return this.http.post<LoggedInUser>(`${this.baseUrl}/login`, { email, password }).pipe(
      tap(response => {
        // Save the token and user details in localStorage and in the service
        this.token = response.token ? response.token : '';
        localStorage.setItem('passport_token', `${this.token}`);
        localStorage.setItem('currentUser', JSON.stringify(response));
      })
    );
  }

  logout(): void {
    this.token = null;
    localStorage.removeItem('passport_token');
    localStorage.removeItem('currentUser');
    this.router.navigate(['/login']);
  }

  register(user: any): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/register`, user);
  }

  // Ensure the token is always returned as a string, even if null
  getToken(): string {
    return this.token ? this.token : '';
  }

  checkEmailExists(email: string): Observable<boolean> {
    return this.http.get<boolean>(`${this.baseUrl}/check-email?email=${email}`);
  }

  // Add a method to retrieve the logged-in user details from localStorage
  getLoggedInUser(): LoggedInUser | null {
    const userJson = localStorage.getItem('currentUser');
    return userJson ? JSON.parse(userJson) : null;
  }
}