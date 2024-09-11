import { Injectable } from '@angular/core';
import { FeedbackComplaint } from '../models/feedback.model';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
    providedIn: 'root',
  })
export class FeedbackComplaintService{

    private baseUrl = 'http://localhost:5172/api';

  constructor(private httpClient:HttpClient) {
  }

   // Method to create a feedback or complaint
   createFeedbackComplaint(feedbackComplaint: FeedbackComplaint): Observable<FeedbackComplaint> {
    return this.httpClient.post<FeedbackComplaint>(`${this.baseUrl}/FeedbackComplaints`, feedbackComplaint);
  }

  // Method to get feedback or complaint by ID
  getFeedbackComplaintById(id: number): Observable<FeedbackComplaint> {
    return this.httpClient.get<FeedbackComplaint>(`${this.baseUrl}/FeedbackComplaints/${id}`);
  }

  // Method to get all feedbacks and complaints
  getAllFeedbackComplaints(): Observable<FeedbackComplaint[]> {
    return this.httpClient.get<FeedbackComplaint[]>(`${this.baseUrl}/FeedbackComplaints`);
  }

  // Method to update feedback or complaint
  updateFeedbackComplaint(id: number, feedbackComplaint: FeedbackComplaint): Observable<FeedbackComplaint> {
    return this.httpClient.put<FeedbackComplaint>(`${this.baseUrl}/FeedbackComplaints/${id}`, feedbackComplaint);
  }

  // Method to delete feedback or complaint
  deleteFeedbackComplaint(id: number): Observable<void> {
    return this.httpClient.delete<void>(`${this.baseUrl}/FeedbackComplaints/${id}`);
  }
}