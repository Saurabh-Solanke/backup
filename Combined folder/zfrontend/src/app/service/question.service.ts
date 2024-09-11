import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Question } from '../Components/exam/exam.component'; 
import { environment } from '../../environment/environment';



@Injectable({
  providedIn: 'root'
})
export class QuestionService {
  //private apiUrl = 'http://localhost:3000/questions';
  private apiUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}

  getQuestions(): Observable<Question[]> {
    return this.http.get<Question[]>(`${this.apiUrl}/questions`);
  }
}
