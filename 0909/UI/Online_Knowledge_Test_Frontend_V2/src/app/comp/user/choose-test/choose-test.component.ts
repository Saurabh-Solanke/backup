import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { ApiService } from '../../../core/services/api.service';
import { ToastrService } from 'ngx-toastr';
import { Exam } from '../../../core/interfaces/exam.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-choose-test',
  templateUrl: './choose-test.component.html',
  styleUrl: './choose-test.component.css'
})
export class ChooseTestComponent implements OnInit, OnDestroy {

  exams: Exam[] = [];
  selectedExam: Exam | null = null; 
  
  examSubscription: Subscription = new Subscription();  // Subscription object to store the subscription

  constructor(private examService: ApiService, private toastr: ToastrService, private router: Router) {}

  ngOnInit(): void {
    this.loadExams();
  }

  loadExams(): void {
    this.examService.getExams().subscribe(
      (data: Exam[]) => {
        this.exams = data.filter(exam => exam.isPublished); // Show only published exams
      },
      (err) => {
        console.error('Error fetching exam data', err);
        this.toastr.error('Error fetching exams', 'Error', {
          progressBar: true,
          progressAnimation: 'decreasing'
        });
      }
    );
  }
  
  startExam(examId: number): void {
    sessionStorage.setItem('examId', examId.toString());  // Store examId in session storage
    this.router.navigate(['/user/exam-start']);  // Navigate to the instruction page
  }

  // OnDestroy lifecycle hook to clean up the subscription
  ngOnDestroy(): void {
    if (this.examSubscription) {
      this.examSubscription.unsubscribe(); // Clean up subscription
    }
  }
}