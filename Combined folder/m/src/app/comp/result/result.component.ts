import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterOutlet } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { HostListener } from '@angular/core';
import { Chart, registerables } from 'chart.js';

import * as Papa from 'papaparse';

import { LoggedInFooterComponent } from '../../shared/components/logged-in-footer/logged-in-footer.component';
import { QuestionService } from '../../service_integration/question.service';
import { IResultData, IUserResultData } from '../../interfaces_integration/result';
import { Subscription } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { ILoggedInUser } from '../../interfaces_integration/user';
import { IExamResult, ISub } from '../../interfaces_integration/exam';

Chart.register(...registerables);

@Component({
  selector: 'app-result',
  standalone: true,
  imports: [CommonModule, RouterOutlet, HttpClientModule, LoggedInFooterComponent],
  templateUrl: './result.component.html',
  styleUrls: ['./result.component.css']
})
export class ResultComponent {

  totalQuestions: number = 0;
  attempted: number = 0;
  unattempted: number = 0;
  correct: number = 0;
  incorrect: number = 0;
  percentage: number = 0;
  timeTaken: number = 0;
  timeTakenStr: string = '';
  username: string = '';
  resultData: IResultData = <IResultData>{}
  userData: ILoggedInUser = <ILoggedInUser>{}
  subjectDetails: ISub = <ISub>{};

  constructor(private router: Router, private toastr: ToastrService, private resultservice: QuestionService) {
    const navigation = this.router.getCurrentNavigation();
    const state = navigation?.extras.state as {
      result: IExamResult,
      timeTaken: number,
    };

    if (state) {
      this.totalQuestions = state.result.totalQuestions || 0;
      this.attempted = state.result.attempted || 0;
      this.unattempted = state.result.unattempted || 0;
      this.correct = state.result.correct || 0;
      this.incorrect = state.result.incorrect || 0;
      this.percentage = state.result.percentage || 0;
      this.timeTaken = state.timeTaken || 0;
      this.resultData.percentageObtained = state.result.percentage || 0;
      this.timeTakenStr = this.formatTime(this.timeTaken);
    } else {
      this.router.navigate(['/']); // Redirect to home if no state is found
    }
  }
  ngOnInit(): void {
    this.GetUserData();


  }

  GetUserData = () => {
    if (typeof window !== 'undefined' && window.sessionStorage) {
      const loggedUser = sessionStorage.getItem('loggedInUser');
      const subjectName = sessionStorage.getItem('subjectName');
      const subjectId = sessionStorage.getItem('subjectId');
      this.resultData.subjectName = subjectName || '';
      this.resultData.subjectId = subjectId ? parseInt(subjectId) : 0;
      if (loggedUser) {
        const userData = JSON.parse(loggedUser);
        this.resultData.userId = parseInt(userData.userId);
      }
    }
  }

  SaveResultData = () => {
    if (typeof window !== 'undefined' && window.sessionStorage) {
      // Assigning properties to resultData object
      this.resultData.totalQuestions = this.totalQuestions;
      this.resultData.attemptedQuestions = this.attempted;
      this.resultData.unAttemptedQuestions = this.unattempted;
      this.resultData.correctQuestions = this.correct;
      this.resultData.inCorrectQuestions = this.incorrect;
      console.log(this.resultData.percentageObtained);
    }

    // Calling the service to save the result data
    this.resultservice.SaveResult(this.resultData).subscribe({
      next: (response: IResultData) => {
        this.toastr.success('You have completed the exam!', 'Thank You', {
          progressBar: true,
          timeOut: 2000, // 2 seconds
          progressAnimation: 'decreasing',
        });
      },
      error: (error: any) => {  // Changed the type to `any` as error might not be of type IResultData
        this.toastr.error("Error while saving result", "Error", { progressBar: true, progressAnimation: 'decreasing' });
      }
    });
  }


  /**
   * Method to format time from seconds into a string format "X minutes Y seconds".
   * 
   * @param seconds - Total time taken in seconds
   * @returns Formatted time string
   */
  formatTime(seconds: number): string {
    const minutes = Math.floor(seconds / 60);
    const remainingSeconds = seconds % 60;
    return `${minutes}m ${remainingSeconds}s`;
  }

  /**
   * Method to handle the completion of the exam.
   * It shows a success message, removes user session, and navigates to the login page.
   * 
   * @returns void
   */
  exit(): void {
    this.SaveResultData();
    this.router.navigate(['/home']);
  }
}