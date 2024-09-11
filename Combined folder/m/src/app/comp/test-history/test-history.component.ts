import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TimeAgoPipe } from '../../shared/pipes/time-ago.pipe';
import { LoggedInFooterComponent } from '../../shared/components/logged-in-footer/logged-in-footer.component';
import { FormatTimePipe } from '../../shared/pipes/format-time.pipe';
import { RouterModule } from '@angular/router';
import { Subscription } from 'rxjs';
import { IUserResultData } from '../../interfaces_integration/result';
import { QuestionService } from '../../service_integration/question.service';
import { ToastrService } from 'ngx-toastr';
import { IUserTestAnswer } from '../../interfaces_integration/exam';

@Component({
  selector: 'app-test-history',
  standalone: true,
  imports: [CommonModule, LoggedInFooterComponent, TimeAgoPipe, FormatTimePipe, RouterModule],
  providers: [QuestionService],
  templateUrl: './test-history.component.html',
  styleUrls: ['./test-history.component.css'],
})
export class TestHistoryComponent implements OnInit {
  name: string | null;
  // testHistory: any[] = [];


  resultData: IUserResultData[] = [];
  paginatedResults: IUserResultData[] = [];
  userId: number = 0;
  subscriber$ = new Subscription();

  // Pagination variables
  currentPage = 1;
  pageSize = 5;  // Number of records per page
  totalPages = 0;
  pagesArray: number[] = [];

  constructor(private service: QuestionService, private toastr: ToastrService) {
    this.name = sessionStorage.getItem('name');
  }

  ngOnInit(): void {
    this.initializeUserData();
    this.getUserResultData(this.userId);
  }

  getUserResultData(userId: number): void {
    this.subscriber$.add(
      this.service.getUserResult(userId).subscribe({
        next: (response: IUserResultData[]) => {
          this.resultData = response;
          this.totalPages = Math.ceil(this.resultData.length / this.pageSize);
          this.pagesArray = Array.from({ length: this.totalPages }, (_, i) => i + 1);
          this.updatePagination();
        },
        error: (error) => {
          console.error(error);
          this.toastr.error("Error fetching user result data", "Error", { progressBar: true, progressAnimation: 'decreasing' });
        }
      })
    );
  }

  updatePagination(): void {
    const startIndex = (this.currentPage - 1) * this.pageSize;
    const endIndex = startIndex + this.pageSize;
    this.paginatedResults = this.resultData.slice(startIndex, endIndex);
  }

  initializeUserData = () => {
    if (typeof window !== 'undefined' && window.sessionStorage) {
      const loggedUser = sessionStorage.getItem('loggedInUser');
      if (loggedUser) {
        const userData = JSON.parse(loggedUser);
        this.userId = parseInt(userData.userId);
      }
    }
  }
  
  downloadCSV(): void {
    const csvData = this.convertToCSV(this.resultData);
    const blob = new Blob([csvData], { type: 'text/csv' });
    const url = window.URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = 'quiz_results.csv';
    a.click();
    window.URL.revokeObjectURL(url);
  }

  
  changePage(page: number): void {
    if (page < 1 || page > this.totalPages) {
      return;
    }
    this.currentPage = page;
    this.updatePagination();
  }

  loadTestHistory() {
    // const userId = sessionStorage.getItem('loggedInUserId'); 
    // this.testResultService.getUser(userId!).subscribe(user => {
    //   this.testHistory = user.testHistory || [];
    //   this.testHistory.reverse();
    // });
  }

  private convertToCSV(data: IUserResultData[]): string {
    const headers = 'SubjectName,TotalQuestions,AttemptedQuestions,UnAttemptedQuestions,CorrectQuestions,InCorrectQuestions,Percentage';
    const rows = data.map(d => `${d.subjectName},${d.totalQuestions},${d.attemptedQuestions},${d.unAttemptedQuestions},${d.correctQuestions},${d.inCorrectQuestions},${d.percentageObtained}`);
    return [headers, ...rows].join('\n');
  }




  downloadTestReportCSV(testId: number): void {
    this.service.getUserTestAnswers(testId).subscribe({
      next: (answers: IUserTestAnswer[]) => {
        const csvData = this.convertToCSVResponse(answers);
        const blob = new Blob([csvData], { type: 'text/csv' });
        const url = window.URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url;
        a.download = `test_${testId}_responses.csv`;
        a.click();
        window.URL.revokeObjectURL(url);
      },
      error: (error) => {
        console.error('Error fetching test answers:', error);
        this.toastr.error('Error fetching test answers', 'Error', { progressBar: true, progressAnimation: 'decreasing' });
      }
    });
  }

  private convertToCSVResponse(data: IUserTestAnswer[]): string {
    const headers = 'QuestionId,OptionId,IsSelected';
    const rows = data.map(d => `${d.questionId},${d.optionId},${d.isSelected}`);
    return [headers, ...rows].join('\n');
  }


  ngOnDestroy(): void {
    this.subscriber$.unsubscribe();
  }
}
