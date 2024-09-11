import { Injectable } from '@angular/core';
import { environment } from '../environment/environment';
import { HttpClient } from '@angular/common/http';
import { exhaustMap, Observable } from 'rxjs';
import { CreateExamDto, Exam, QuestionDto, QuestionPostDTO, Section, SectionGetDTO, SectionPostDTO, UpdateExamDto } from '../interfaces/exam.model';
import { Question, User, UserDetailsInUserManager } from '../interfaces/user';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private baseUrl = environment.BaseUrl;
  constructor(private http: HttpClient) { }

  // Exam related services ------------------------------------->

  // Fetch all exams
  getExams(): Observable<Exam[]> {
    return this.http.get<Exam[]>(`${this.baseUrl}/exam`);
  }

  // Fetch specific exam by ID
  getExamById(examId: number): Observable<Exam> {
    return this.http.get<Exam>(`${this.baseUrl}/exam/${examId}`);
  }

  // Create a new exam
  createExam(examData: CreateExamDto): Observable<Exam> {
    return this.http.post<Exam>(`${this.baseUrl}/exam`, examData);
  }

  // Update an existing exam
  updateExam(examId: number, examData: UpdateExamDto): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/exam/${examId}`, examData);
  }

  // Delete an exam
  deleteExam(examId: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/exam/${examId}`);
  }

  //Section related services ------------------------------------->

  // Updated method to fetch sections by examId using the provided API
  getSectionsByExamId(examId: number): Observable<Section[]> {
    return this.http.get<Section[]>(`${this.baseUrl}/Section/Exam/${examId}`);
  }

  // Method to fetch questions by sectionId
  getQuestionsBySectionId(sectionId: number): Observable<QuestionDto[]> {
    return this.http.get<QuestionDto[]>(`${this.baseUrl}/Question?sectionId=${sectionId}`);
  }

  // Question related Service ------------------------------------->

  // Method to fetch all questions
  getAllQuestions(): Observable<Question[]> {
    return this.http.get<Question[]>(`${this.baseUrl}/Question`);  // Make sure this points to the correct API
  }

  // Report related Service ------------------------------------->

  // 1. Get number of tests taken per day
  getTestsPerDay(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/Reports/TestsPerDay`);
  }

  // 2. Get tests finished before 20% of the time
  getTestsFinishedBeforeTime(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/Reports/FinishedBeforeTime`);
  }

  // 3. Get auto-submitted tests after 30 minutes and less than 50% of questions attempted
  getAutoSubmittedAfter30Mins(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/Reports/AutoSubmittedAfter30Mins`);
  }

  // 4. Get tests where 50% or more questions were marked for review
  getTestsMarkedForReview(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/Reports/MarkedForReview`);
  }

  // Exam-Result related services ------------------------------------->

  // Function to submit the exam
  submitExam(examData: any): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/ExamResult/Submit`, examData);
  }


  // Function to get results by user ID
  getResultsByUser(userId: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/ExamResult/GetResultsByUser/${userId}`);
  }

  // Fetch all results
  getAllResults(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/ExamResult/GetAllResults`);  // Corrected URL
  }


  // Section-Action related services ------------------------------------->

  // Get sections by ExamId
  getSectionsByExamIdE(examId: number): Observable<SectionGetDTO[]> {
    return this.http.get<SectionGetDTO[]>(`${this.baseUrl}/SectionAction/exam/${examId}`);
  }

  // Get section by SectionId
  getSectionById(sectionId: number): Observable<SectionGetDTO> {
    return this.http.get<SectionGetDTO>(`${this.baseUrl}/SectionAction/${sectionId}`);
  }

  // Create a new section
  createSection(sectionData: SectionPostDTO): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/SectionAction`, sectionData);
  }

  // Update a section by SectionId
  updateSection(sectionId: number, sectionData: SectionPostDTO): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/SectionAction/${sectionId}`, sectionData);
  }

  // Delete a section by SectionId
  deleteSection(sectionId: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/SectionAction/${sectionId}`);
  }

  //Add a new question to a section
  addQuestion(sectionId: number, questionData: QuestionPostDTO): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/SectionAction/${sectionId}/questions`, questionData);
  }

  // Delete a question by QuestionId
  deleteQuestion(questionId: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/SectionAction/questions/${questionId}`);
  }

  addQuestionToSection(questionData: { sectionId: number, questionText: string, isMultipleChoice: boolean, options: { optionText: string, isCorrect: boolean }[] }): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/Question`, questionData);
  }

  // User details fetch API

  // Fetch user details by ID
  getUserById(userId: string): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/User/GetUserById/${userId}`);
  }

  getAllUsers():Observable<UserDetailsInUserManager[]>{
    return this.http.get<UserDetailsInUserManager[]>(`${this.baseUrl}/User/GetAllUsers`);
  }
}

