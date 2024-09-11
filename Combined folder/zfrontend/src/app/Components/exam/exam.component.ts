import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import {  Router, RouterOutlet } from '@angular/router';
import Swal from 'sweetalert2';
import { QuestionService } from '../../service/question.service';

export interface Question {
  id: string;
  question: string;
  type: 'single' | 'multiple';
  options: string[];
  answer: number | number[];
}

@Component({
  selector: 'app-exam',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterOutlet, HttpClientModule],
  providers: [QuestionService],
  templateUrl: './exam.component.html',
  styleUrls: ['./exam.component.css']
})
export class ExamComponent implements OnInit {
  questions: Question[] = [];
  currentQuestionIndex :number = 0;
  userResponses: any[] = [];
  reviewedQuestions: boolean[] = [];
  minutes: number = 0;
  seconds:number = 0;
  attempted :number= 0;
  remaining :number= 0;
  timerInterval: any;
  timer: number = 30 * 60; // 30 minutes in seconds
  username: string = '';

  constructor(private questionService: QuestionService, private router: Router) {}

  ngOnInit(): void {
    this.fetchQuestions(); // Fetch questions when component initializes
    this.username = history.state.username;
    if (!this.username) {
      console.error('Username not found in navigation state');
    }
  }

  fetchQuestions(): void  {
    this.questionService.getQuestions().subscribe(data => {
      this.questions = data;
      this.userResponses = new Array(data.length).fill(null);
      this.reviewedQuestions = new Array(data.length).fill(false);
      this.updateStatus();
      this.startTimer();
     // this.calculateAttemptedAndRemaining();
    }, error => {
      console.error('Error fetching questions:', error);
      Swal.fire('Error', 'Failed to load questions. Please try again later.', 'error');
    });
  }

  startTimer(): void  {
    this.timerInterval = setInterval(() => {
      this.timer--;
      if (this.timer === 600) {
        Swal.fire('Alert', '10 minutes remaining!', 'warning');
      }
      if (this.timer <= 0) {
        clearInterval(this.timerInterval);
        this.timeUpAlert();
      }
      this.minutes = Math.floor(this.timer / 60);
      this.seconds = this.timer % 60;
    }, 1000);
  }

  selectOption(index: number): void  {
    this.userResponses[this.currentQuestionIndex] = index;
    this.calculateAttemptedAndRemaining();
  }

  updateMultipleChoiceAnswer(questionIndex: number, optionIndex: number, event: any): void  {
    if (!this.userResponses[questionIndex]) {
      this.userResponses[questionIndex] = [];
    }

    if (event.target.checked) {
      this.userResponses[questionIndex].push(optionIndex);
    } else {
      const optionPosition = this.userResponses[questionIndex].indexOf(optionIndex);
      if (optionPosition !== -1) {
        this.userResponses[questionIndex].splice(optionPosition, 1);
      }
    }

    this.calculateAttemptedAndRemaining();
  }

  calculateAttemptedAndRemaining(): void  {
    this.attempted = this.userResponses.filter(response  => response !== null && (Array.isArray(response) ? response.length > 0 : true)).length;
    this.remaining = this.questions.length - this.attempted;
  }

  updateStatus(): void  {
    this.calculateAttemptedAndRemaining();
  }

  clearResponse(): void  {
    this.userResponses[this.currentQuestionIndex] = null; // Clear user's response for current question
    this.updateStatus(); // Update attempted and remaining counts
  }

  toggleReviewStatus(): void  {
    this.reviewedQuestions[this.currentQuestionIndex] = !this.reviewedQuestions[this.currentQuestionIndex]; // Toggle review status
  }

  previousQuestion(): void  {
    if (this.currentQuestionIndex > 0) {
      this.currentQuestionIndex--;
    }
  }

  nextQuestion(): void  {
    if (this.currentQuestionIndex < this.questions.length - 1) {
      this.currentQuestionIndex++;
    }
  }

  jumpToQuestion(index: number): void  {
    this.currentQuestionIndex = index;
  }

  submitExam(): void  {
    const totalQuestions = this.questions.length;
    const attemptedQuestions = this.userResponses.filter(response => response !== null).length;
    const requiredAttempts = Math.ceil(totalQuestions * 0.8); // 80% of total questions

    if (attemptedQuestions < requiredAttempts) {
      Swal.fire({
        title: 'Insufficient Attempts',
        text: `You must attempt at least 80% of the questions to submit the exam. Currently, you have attempted ${attemptedQuestions} out of ${totalQuestions} questions.`,
        icon: 'warning',
        confirmButtonText: 'OK'
      });
    } else {
      Swal.fire({
        title: 'Submit Exam?',
        text: "Are you sure you want to submit?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, submit!',
        cancelButtonText: 'No, cancel'
      }).then((result) => {
        if (result.isConfirmed) {
          clearInterval(this.timerInterval);
          this.navigateToResult();
        }
      });
    }
  }

  timeUpAlert(): void  {
    Swal.fire({
      title: 'Time is up!',
      text: 'Your time is over. The exam will be submitted automatically.',
      icon: 'info',
      confirmButtonText: 'OK'
    }).then(() => {
      this.navigateToResult();
    });
  }

  navigateToResult(): void  {
    this.router.navigate(['/result'], { state: { userResponses: this.userResponses, questions: this.questions, timeTaken: 30 * 60 - this.timer, username: this.username } });
  }
}
