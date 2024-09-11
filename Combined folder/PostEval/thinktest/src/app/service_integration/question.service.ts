import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environment/environment';
import { Question } from '../interfaces_integration/question';
import { IExamResult, IExamSubmission, IQuestion, ISub, ISubject } from '../interfaces_integration/exam';
import { IResultData, IUserResultData } from '../interfaces_integration/result';



@Injectable({
  providedIn: 'root'
})
export class QuestionService {
  private apiUrl = environment.BaseURL;
  constructor(private http: HttpClient) {}

  getQuestions(): Observable<IQuestion[]> {
    return this.http.get<IQuestion[]>(`${this.apiUrl}/Exam/GetExamQuestionAndAnswer`);
  }
  getSubjectById(subjectId: number): Observable<ISubject> {
    return this.http.get<ISubject>(`${this.apiUrl}/Exam/GetQuestionsBySubject?subjectId=${subjectId}`);
  }
  
  submitExam(examSubmission: IExamSubmission): Observable<IExamResult>{
    return this.http.post<IExamResult>(`${this.apiUrl}/Exam/submitExam`, examSubmission); 
  }

  GetAllSubject = () : Observable<ISub[]> =>{
    return this.http.get<ISub[]>(`${this.apiUrl}/Exam/GetAllSubjects`);
   }
  
   SaveResult (resultData:IResultData):Observable<IResultData>{
    return this.http.post<IResultData>(`${this.apiUrl}/Exam/SaveExamDataInDB`,resultData);
  }

  getUserResult(userId : number):Observable<IUserResultData[]>
  {
      return this.http.get<IUserResultData[]>(`${this.apiUrl}/Result/getResultbyUserId/${userId}`);
  }



}
