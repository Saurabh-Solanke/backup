import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ResolvedStatusPipe } from '../Pipes/resolved-status.pipe';
import { FeedbackComplaintTypePipe } from '../Pipes/feedback-complaint-type.pipe';
import { AdminNavbarComponent } from "../admin-navbar/admin-navbar.component";
@Component({
  selector: 'app-feedback-complaints',
  templateUrl: './admin-feedback.component.html',
  styleUrls: ['./admin-feedback.component.css'],
  standalone: true,
  imports: [CommonModule, ResolvedStatusPipe, FeedbackComplaintTypePipe, AdminNavbarComponent]
})
export class AdminFeedbackComponent implements OnInit {
  feedbackComplaints$!: Observable<FeedbackComplaintDTO[]>;
  feedbackComplaintForm!: FormGroup;
  editingComplaint: FeedbackComplaintDTO | null = null;

  private apiUrl = 'http://localhost:5172/api/FeedbackComplaints';

  constructor(private http: HttpClient, private fb: FormBuilder) { }

  ngOnInit(): void {
    this.loadFeedbackComplaints();
    this.initializeForm();
  }

  initializeForm(): void {
    this.feedbackComplaintForm = this.fb.group({
      id: [null],
      type: ['', Validators.required],
      title: ['', Validators.required],
      description: ['', Validators.required],
      status: [''],
      createdOn: [''],
      updatedOn: [''],
      userID: ['']
    });
  }

  loadFeedbackComplaints(): void {
    this.feedbackComplaints$ = this.http.get<FeedbackComplaintDTO[]>(this.apiUrl).pipe(
      catchError(error => {
        console.error('Error fetching feedback complaints', error);
        return [];
      })
    );
  }

  editFeedbackComplaint(complaint: FeedbackComplaintDTO): void {
    this.editingComplaint = complaint;
    this.feedbackComplaintForm.patchValue(complaint);
  }

  saveFeedbackComplaint(): void {
    if (this.feedbackComplaintForm.invalid) return;

    const complaint: FeedbackComplaintDTO = this.feedbackComplaintForm.value;
    if (complaint.id) {
      this.http.put(`${this.apiUrl}/${complaint.id}`, complaint).subscribe(() => {
        this.loadFeedbackComplaints();
        this.resetForm();
      });
    } else {
      this.http.post(this.apiUrl, complaint).subscribe(() => {
        this.loadFeedbackComplaints();
        this.resetForm();
      });
    }
  }

  deleteFeedbackComplaint(id: number): void {
    this.http.delete(`${this.apiUrl}/${id}`).subscribe(() => {
      this.loadFeedbackComplaints();
    });
  }

  resetForm(): void {
    this.feedbackComplaintForm.reset();
    this.editingComplaint = null;
  }
}

interface FeedbackComplaintDTO {
  id: number;
  feedbackComplaintType: number;
  title: string;
  description: string;
  complaintStatus: number;
  createdOn: Date;
  email: string;
  userID: number;
}
