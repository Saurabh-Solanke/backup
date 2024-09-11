import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../../core/services/api.service';
import { forkJoin, Observable } from 'rxjs';

@Component({
  selector: 'app-test-history',
  templateUrl: './test-history.component.html',
  styleUrl: './test-history.component.css'
})
export class TestHistoryComponent implements OnInit {
  userId: string | null = null;
  examResults: any[] = [];
  isLoading: boolean = true;
  errorMessage: string | null = null;

  constructor(
    private examResultService: ApiService,
    private examService: ApiService,
  ) {}

  ngOnInit(): void {
    // Fetch userId from the JWT token
    this.userId = sessionStorage.getItem('userId');

    if (this.userId) {
      this.loadExamResults();
    }
  }

  // Load exam results for the user and fetch the exam names
  loadExamResults(): void {
    this.examResultService.getResultsByUser(this.userId!).subscribe(
      (results: any[]) => {
        if (results.length > 0) {
          this.fetchExamNames(results);
        } else {
          this.examResults = results;
          this.isLoading = false;
        }
      },
      (error: any) => {
        console.error('Error loading exam results:', error);
        this.errorMessage = error.error ? error.error : 'Failed to load exam results';
        this.isLoading = false;
      }
    );
  }

  // Fetch exam names for each result and add to exam results array
  fetchExamNames(results: any[]): void {
    const examRequests: Observable<any>[] = [];

    // Create an array of observables for fetching exam details
    results.forEach((result) => {
      examRequests.push(this.examService.getExamById(result.examId));
    });

    // Wait for all exam requests to complete
    forkJoin(examRequests).subscribe(
      (examData) => {
        examData.forEach((exam, index) => {
          results[index].examName = exam.title;  // Assuming the exam name is stored in "title"
        });
        this.examResults = results;
        this.isLoading = false;
      },
      (error: any) => {
        console.error('Error fetching exam names:', error);
        this.isLoading = false;
      }
    );
  }
}
