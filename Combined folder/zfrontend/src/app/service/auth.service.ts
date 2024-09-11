import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import Swal from 'sweetalert2';
import { environment } from '../../environment/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
 // private baseUrl = 'http://localhost:3000/users';
 private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  signup(user: ISignupForm): Observable<User> {
    return this.http.post<User>(`${this.baseUrl}/users`, user).pipe(
      catchError(error => this.handleError('signup', error))
    );
  }

  login(credentials: ILoginForm): Observable<User> {
    return this.http.get<any[]>(`${this.baseUrl}/users?email=${credentials.email}&password=${credentials.password}`).pipe(
      map(users => users.length > 0 ? users[0] : null), 
      catchError(error => this.handleError('login', error))
    );
  }

  private handleError(operation = 'operation', error: any): Observable<any> {
    console.error(`${operation} failed: ${error.message}`);

    Swal.fire('Error', `${operation.charAt(0).toUpperCase() + operation.slice(1)} failed: ${error.message}`, 'error');

    return of(null);
  }
}

export interface ISignupForm{
  name:string,
  email: string,
  password: string,
  confirmPassword: string,
  address: string,
  pincode: string,

}
export interface ILoginForm{
  email: string,
  password: string
}
export interface User{
  name:string,
  email: string,
  password: string,
  confirmPassword: string,
  address: string,
  pincode: string,
}