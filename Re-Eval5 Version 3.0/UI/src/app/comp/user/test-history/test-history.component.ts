import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../../core/services/api.service';
import { forkJoin, Observable } from 'rxjs';

@Component({
  selector: 'app-test-history',
  templateUrl: './test-history.component.html',
  styleUrls: ['./test-history.component.css']
})
export class TestHistoryComponent implements OnInit {
  userId: string | null = null;
  examResults: any[] = [];
  filteredResults: any[] = [];
  isLoading: boolean = true;
  errorMessage: string | null = null;
  searchTerm: string = '';
  currentPage: number = 1;
  pageSize: number = 5;
  percentageFilter: 'all' | 'top20' | 'top50' | 'bottom20' = 'all';
  sortField: string = 'percentage';
  sortAscending: boolean = true;

  constructor(
    private examResultService: ApiService,
    private examService: ApiService
  ) {}

  ngOnInit(): void {
    this.userId = sessionStorage.getItem('userId');
    if (this.userId) {
      this.loadExamResults();
    }
  }

  loadExamResults(): void {
    this.examResultService.getResultsByUser(this.userId!).subscribe(
      (results: any[]) => {
        if (results.length > 0) {
          this.fetchExamNames(results);
        } else {
          this.examResults = results;
          this.filteredResults = [...this.examResults];
          this.isLoading = false;
        }
      },
      (error: any) => {
        this.errorMessage = error.error ? error.error : 'Failed to load exam results';
        this.isLoading = false;
      }
    );
  }

  fetchExamNames(results: any[]): void {
    const examRequests: Observable<any>[] = [];

    results.forEach((result) => {
      examRequests.push(this.examService.getExamById(result.examId));
    });

    forkJoin(examRequests).subscribe(
      (examData) => {
        examData.forEach((exam, index) => {
          results[index].examName = exam.title;
        });
        this.examResults = results;
        this.filteredResults = [...this.examResults];
        this.isLoading = false;
      },
      (error: any) => {
        console.error('Error fetching exam names:', error);
        this.isLoading = false;
      }
    );
  }

  search(): void {
    this.filteredResults = this.examResults.filter(result =>
      result.examName.toLowerCase().includes(this.searchTerm.toLowerCase())
    );
  }

  applyPercentageFilter(): void {
    switch (this.percentageFilter) {
      case 'top20':
        this.filteredResults = this.examResults.filter(result => result.percentage >= 80);
        break;
      case 'top50':
        this.filteredResults = this.examResults.filter(result => result.percentage >= 50);
        break;
      case 'bottom20':
        this.filteredResults = this.examResults.filter(result => result.percentage < 20);
        break;
      default:
        this.filteredResults = [...this.examResults];
    }
  }

  sortResults(field: string): void {
    this.sortField = field;
    this.sortAscending = !this.sortAscending;
    this.filteredResults.sort((a, b) => {
      if (this.sortAscending) {
        return a[field] > b[field] ? 1 : -1;
      } else {
        return a[field] < b[field] ? 1 : -1;
      }
    });
  }

  get paginatedResults(): any[] {
    const start = (this.currentPage - 1) * this.pageSize;
    const end = start + this.pageSize;
    return this.filteredResults.slice(start, end);
  }

  changePage(step: number): void {
    const newPage = this.currentPage + step;
    if (newPage > 0 && newPage <= Math.ceil(this.filteredResults.length / this.pageSize)) {
      this.currentPage = newPage;
    }
  }
}
