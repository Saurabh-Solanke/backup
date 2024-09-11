import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ExamResultService {
  private baseUrl = 'http://localhost:3000/examResults';

  constructor(private http: HttpClient) { }

  saveExamResult(result: any): Observable<any> {
    return this.http.post<any>(this.baseUrl, result).pipe(
      catchError(this.handleError)
    );
  }

  private handleError(error: any): Observable<never> {
    console.error('An error occurred', error);
    return throwError(error.message || error);
  }
}
