import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { FeedbackComplaint } from "../models/feedback.model";
import { ApplicationForm } from "../models/application.model";
import { Payment } from "../models/payment.model";
import { User } from "../models/user.model";

@Injectable({
    providedIn:'root',
})
export class AdminService{
    private baseUrl = 'http://localhost:5172/api';

  constructor(private http:HttpClient) {
  }

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${this.baseUrl}/users`);
  }

  getApplications(): Observable<ApplicationForm[]> {
    return this.http.get<ApplicationForm[]>(`${this.baseUrl}/MasterDetailsTables`);
  }

  getRenewalApplications(): Observable<ApplicationForm[]> {
    return this.http.get<ApplicationForm[]>(`${this.baseUrl}/MasterDetailsTables`);
  }

  getComplaints(): Observable<FeedbackComplaint[]> {
    return this.http.get<FeedbackComplaint[]>(`${this.baseUrl}/FeedbackComplaints`);
  }

  getFeedbacks(): Observable<FeedbackComplaint[]> {
    return this.http.get<FeedbackComplaint[]>(`${this.baseUrl}/FeedbackComplaints`);
  }

  getPayments(): Observable<Payment[]> {
    return this.http.get<Payment[]>(`${this.baseUrl}/payments`);
  }

  updateApplication(application: ApplicationForm): Observable<number> {
    return this.http.put<number>(`${this.baseUrl}/MasterDetailsTables/${application.applicationNo}`, application);
  }

  updateRenewalApplication(renewalApplication: ApplicationForm): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/MasterDetailsTables/${renewalApplication.applicationNo}`, renewalApplication);
  }

  updateComplaint(complaint: FeedbackComplaint): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/FeedbackComplaints/${complaint.id}`, complaint);
  }

  updateFeedback(feedback: FeedbackComplaint): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/FeedbackComplaints/${feedback.id}`, feedback);
  }

  deleteUser(userId: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/users/${userId}`);
  }

  deleteApplication(applicationNo: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/MasterDetailsTables/${applicationNo}`);
  }

  deleteRenewalApplication(applicationNo: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/MasterDetailsTables/${applicationNo}`);
  }

  deleteComplaint(complaintId: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/FeedbackComplaints/${complaintId}`);
  }

  deleteFeedback(feedbackId: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/FeedbackComplaints/${feedbackId}`);
  }
}