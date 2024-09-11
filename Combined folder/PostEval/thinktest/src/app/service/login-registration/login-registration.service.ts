import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl = 'http://localhost:3000/users';

  constructor(private http: HttpClient) { }

  signup(user: any): Observable<any> {
    return this.http.post<any>(this.baseUrl, user).pipe(
      catchError(error => this.handleError('register', error))
    );
  }

  login(credentials: any): Observable<any> {
    return this.http.get<any[]>(`${this.baseUrl}?email=${credentials.email}&password=${credentials.password}`).pipe(
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
