  import { Component, OnInit } from '@angular/core';
  import { CommonModule } from '@angular/common';
  import { Router, RouterOutlet } from '@angular/router';
  import Swal from 'sweetalert2';
  import { HttpClientModule } from '@angular/common/http';
import { Question } from '../exam/exam.component';


  @Component({
    selector: 'app-result',
    standalone: true,
    imports: [CommonModule,RouterOutlet,HttpClientModule],
    templateUrl: './result.component.html',
    styleUrls: ['./result.component.css']
  })
  export class ResultComponent implements OnInit {
    questions: Question[] = [];
    userResponses: any[] = [];
    attempted: number = 0;
    correct: number = 0;
    incorrect: number = 0;
    percentage: number = 0;
    timeTaken: number = 0;
    timeTakenStr: string = '';
    username: string = '';
    constructor(private router: Router) {
      const navigation = this.router.getCurrentNavigation();
      const state = navigation?.extras.state as { userResponses: any[], questions: Question[], timeTaken: number };
      if (state) {
        this.userResponses = state.userResponses;
        this.questions = state.questions;
        this.timeTaken = state.timeTaken;
        this.timeTakenStr = this.formatTime(this.timeTaken);
        
      } else {
        this.router.navigate(['/']); // Redirect to home if no state is found
      }
    }

    ngOnInit(): void {
      this.calculateResult();
      this.username = history.state.username;
      if (!this.username) {
        // Handle scenario where username is not passed
        console.error('Username not found in navigation state');
      }
    }

    calculateResult(): void  {
      this.attempted = this.userResponses.filter(response => response !== null).length;
      this.correct = this.questions.filter((q, i) => q.answer === this.userResponses[i]).length;
      this.incorrect = this.attempted - this.correct;
      this.percentage = (this.correct / this.questions.length) * 100;
    }

    formatTime(seconds: number): string {
      const minutes = Math.floor(seconds / 60);
      const remainingSeconds = seconds % 60;
      return `${minutes}m ${remainingSeconds}s`;
    }

    exit(): void  {
      Swal.fire('Thank You', 'You have completed the exam!', 'success').then(() => {
        sessionStorage.removeItem('loggedUserEmail');
        this.router.navigate(['/']);
      });
    }
  }
