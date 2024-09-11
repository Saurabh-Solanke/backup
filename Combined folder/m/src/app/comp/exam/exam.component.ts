import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { Router, RouterOutlet } from '@angular/router';
import Swal from 'sweetalert2';
import { QuestionService } from '../../service_integration/question.service';

import { LoggedInFooterComponent } from '../../shared/components/logged-in-footer/logged-in-footer.component';
import { IExamResult, IExamSubmission, IQuestion, IQuestionResponse, ISubject, IUserTestAnswer } from '../../interfaces_integration/exam';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-exam',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterOutlet, HttpClientModule, LoggedInFooterComponent],
  providers: [QuestionService],
  templateUrl: './exam.component.html',
  styleUrls: ['./exam.component.css']
})
export class ExamComponent {
  questions: IQuestion[] = [];
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
  subjectId: number = 0;
  constructor(private questionService: QuestionService, private router: Router,private toastr : ToastrService) {}

  ngOnInit(): void {
    this.GetSubjectId();
    this.fetchQuestions(); // Fetch questions when component initializes
  }

  GetSubjectId = () =>{
    if(typeof window.localStorage !== 'undefined' && window.localStorage) {
      this.subjectId = Number(sessionStorage.getItem('subjectId'));
    }
  }

  /**
 * Method to fetch questions from the server.
 * This method retrieves questions, initializes user responses, review statuses, and starts the timer.
 * 
 * @returns void
 */
  fetchQuestions(): void  {
    this.questionService.getSubjectById(this.subjectId).subscribe((data : ISubject)  => {
      this.questions = data.questions;
      this.userResponses = [];
      this.reviewedQuestions = [];
      for (let i = 0; i < data.questions.length; i++) {
        this.userResponses.push(null);
        this.reviewedQuestions.push(false);
      }
      this.startTimer();
     this.calculateAttemptedAndRemaining();
    }, error => {
      console.error('Error fetching questions:', error);
      this.toastr.error("Error while fetching questions","Error",{progressBar: true,progressAnimation: 'decreasing'});
    });
  }

  /**
 * Method to start the exam timer.
 * This method decreases the timer every second and triggers alerts when the time is running low or up.
 * 
 * @returns void
 */
  startTimer(): void  {
    this.timerInterval = setInterval(() => {
      this.timer--;
      if (this.timer === 600) {
       this.toastr.warning("10 minutes remaining!","Warning",{progressBar: true,progressAnimation: 'decreasing',timeOut: 2000})
      }
      if (this.timer <= 0) {
        clearInterval(this.timerInterval);
        this.timeUpAlert();
      }
      this.minutes = Math.floor(this.timer / 60);
      this.seconds = this.timer % 60;
    }, 1000);
  }

  /**
 * Method to select an option for the current question.
 * It updates the user's response for the current question and recalculates the number of attempted questions.
 * 
 * @param index - The index of the selected option
 * @returns void
 */
  selectOption(index: number): void  {
    this.userResponses[this.currentQuestionIndex] = index;
    this.calculateAttemptedAndRemaining();
  }

  /**
 * Method to update multiple choice answers for a question.
 * It handles both selection and deselection of multiple choice options.
 * 
 * @param questionIndex - The index of the question
 * @param optionIndex - The index of the selected/deselected option
 * @param event - The event triggered by the user's interaction (checkbox change)
 * @returns void
 */
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

  /**
 * Method to calculate the number of attempted and remaining questions.
 * It updates the count based on the user's responses.
 * 
 * @returns void
 */
  calculateAttemptedAndRemaining(): void  {
    this.attempted = this.userResponses.filter(response  => response !== null && (Array.isArray(response) ? response.length > 0 : true)).length;
    this.remaining = this.questions.length - this.attempted;
  }

  /**
 * Method to clear the user's response for the current question.
 * It resets the response and updates the count of attempted and remaining questions.
 * 
 * @returns void
 */
  clearResponse(): void  {
    this.userResponses[this.currentQuestionIndex] = null; // Clear user's response for current question
    this.calculateAttemptedAndRemaining(); // Update attempted and remaining counts
  }

    /**
   * Method to toggle the review status of the current question.
   * It allows the user to mark a question for review.
   * 
   * @returns void
   */
  toggleReviewStatus(): void  {
    this.reviewedQuestions[this.currentQuestionIndex] = !this.reviewedQuestions[this.currentQuestionIndex]; // Toggle review status
  }

  /**
 * Method to navigate between questions.
 * It adjusts the current question index based on the direction ('previous' or 'next').
 * 
 * @param direction - The direction to navigate ('previous' or 'next')
 * @returns void
 */
  navigateQuestion(direction: 'previous' | 'next'): void {
    switch (direction) {
      case 'previous':
        if (this.currentQuestionIndex > 0) {
          this.currentQuestionIndex--;
        }
        break;
      case 'next':
        if (this.currentQuestionIndex < this.questions.length - 1) {
          this.currentQuestionIndex++;
        }
        break;
    }
  }
    /**
   * Method to jump to a specific question.
   * It updates the current question index to the specified index.
   * 
   * @param index - The index of the question to jump to
   * @returns void
   */
  jumpToQuestion(index: number): void  {
    this.currentQuestionIndex = index;
  }

    /**
   * Method to submit the exam.
   * It checks if the user has attempted enough questions (80% of total) before allowing submission.
   * 
   * @returns void
   */
    submitExam(): void {
      const totalQuestions = this.questions.length;
      const attemptedQuestions = this.userResponses.filter(response => response !== null).length;
      const requiredAttempts = Math.ceil(totalQuestions * 0.8); // 80% of total questions
      if (attemptedQuestions < requiredAttempts) {
        Swal.fire({
          title: 'Insufficient Attempts',
          text: `You must attempt at least ${requiredAttempts} questions to submit the exam. Currently, you have attempted ${attemptedQuestions}.`,
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
            this.sendExamSubmission(); // Send the submission to the backend
          }
        });
      }
    }
  
    timeUpAlert(): void {
      Swal.fire({
        title: 'Time is up!',
        text: 'Your time is over. The exam will be submitted automatically.',
        icon: 'info',
        confirmButtonText: 'OK'
      }).then(() => {
        this.sendExamSubmission(); // Automatically submit the exam when time is up
      });
    }
  
    /**
     * Method to prepare and send the exam submission to the backend.
     * 
     * @returns void
     */
    sendExamSubmission(): void {
      const responses: IQuestionResponse[] = this.questions
        .map((question, index) => {
          if (this.userResponses[index] !== null) {
            return {
              questionId: question.id, 
              selectedOptions: Array.isArray(this.userResponses[index]) ? this.userResponses[index] : [this.userResponses[index]]
            };
          }
          return null; // Return null for unanswered questions
        })
        .filter(response => response !== null) as IQuestionResponse[]; // Filter out null values
    
      const examSubmission: IExamSubmission = {
        responses: responses
      };
    
      this.questionService.submitExam(examSubmission).subscribe(
        (result: IExamResult) => {
          this.router.navigate(['/result'], { state: { result, timeTaken: 30 * 60 - this.timer} });
        },
        error => {
          console.error('Error submitting exam:', error);
          this.toastr.error("Error while submitting exam", "Error", { progressBar: true, progressAnimation: 'decreasing' });
        }
      );
    }

    // sendExamSubmission(): void {
    //   // Step 1: Map user responses to IUserTestAnswer and prepare the data for saveUserTestAnswer
    //   const userTestAnswers: IUserTestAnswer[] = this.questions
    //     .map((question, index) => {
    //       if (this.userResponses[index] !== null) {
    //         const selectedOptions = Array.isArray(this.userResponses[index])
    //           ? this.userResponses[index].map((option: number) => Number(option)) // Explicitly define option type
    //           : [Number(this.userResponses[index])]; // For single-choice questions
            
    //         return {
    //           testId: this.subjectId, // Assuming subjectId is the testId
    //           questionId: question.id,
    //           optionId: selectedOptions[0], // Saving the first selected option for now
    //           isSelected: true // Assuming all selected options are "true"
    //         };
    //       }
    //       return null;
    //     })
    //     .filter(answer => answer !== null) as IUserTestAnswer[]; // Filter out null values
    
    //   // Step 2: Save user test answers using saveUserTestAnswers API
    //   this.questionService.saveUserTestAnswers(userTestAnswers).subscribe(
    //     () => {
    //       console.log('User test answers saved successfully.');
    
    //       // Step 3: Proceed with submitting the exam after saving answers
    //       const responses: IQuestionResponse[] = this.questions
    //         .map((question, index) => {
    //           if (this.userResponses[index] !== null) {
    //             return {
    //               questionId: question.id,
    //               selectedOptions: Array.isArray(this.userResponses[index]) 
    //                 ? this.userResponses[index].map((option: number) => Number(option)) // Explicitly define option type
    //                 : [this.userResponses[index]]
    //             };
    //           }
    //           return null; // Return null for unanswered questions
    //         })
    //         .filter(response => response !== null) as IQuestionResponse[]; // Filter out null values
    
    //       const examSubmission: IExamSubmission = {
    //         responses: responses
    //       };
    
    //       // Step 4: Submit the exam via submitExam API
    //       this.questionService.submitExam(examSubmission).subscribe(
    //         (result: IExamResult) => {
    //           this.router.navigate(['/result'], { state: { result, timeTaken: 30 * 60 - this.timer } });
    //         },
    //         error => {
    //           console.error('Error submitting exam:', error);
    //           this.toastr.error("Error while submitting exam", "Error", { progressBar: true, progressAnimation: 'decreasing' });
    //         }
    //       );
    //     },
    //     error => {
    //       console.error('Error saving test answers:', error);
    //       this.toastr.error("Error while saving test answers", "Error", { progressBar: true, progressAnimation: 'decreasing' });
    //     }
    //   );
    // }  
}
