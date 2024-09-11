import { Component, inject, NgModule, OnInit } from '@angular/core';
import { User } from '../../models/user.model';
import { FormControl, FormGroup, FormsModule, NgModel, ReactiveFormsModule, Validators } from '@angular/forms';
import { ToastService } from '../../services/toast.service';
import { FeedbackComplaint } from '../../models/feedback.model';
import { FeedbackComplaintService } from '../../services/feedbackcomplaint.service';
import { ComplaintStatus, FeedbackComplaintType } from '../../models/enums/enums';

@Component({
  selector: 'app-feedback',
  standalone: true,
  templateUrl: './feedback.component.html',
  styleUrls: ['./feedback.component.css'],
  imports:[FormsModule,ReactiveFormsModule]
})
export class FeedbackComponent implements OnInit {

  private toastService=inject(ToastService)
  private feedbackComplaintService = inject(FeedbackComplaintService);
  
  currentUser: User | null = null;
  feedback:FeedbackComplaint=<FeedbackComplaint>{}


  form = new FormGroup({
    description: new FormControl('', { validators: [Validators.required] }),
    name: new FormControl({ value: '', disabled: true }),
    email: new FormControl('', [Validators.email]),
    feedbackType: new FormControl('feedback', { validators: [Validators.required] }),
    title: new FormControl('', { validators: [Validators.required] })
  });

  ngOnInit(): void {
    const currentUserJson = localStorage.getItem('currentUser');
    if (currentUserJson) {
      this.currentUser = JSON.parse(currentUserJson);
      if (this.currentUser) {
        this.form.patchValue({
          name:this.currentUser.firstname,
          email:this.currentUser.email,
        })
      }
    }
  }

  onSubmit(){

    if (this.form.invalid) {
      Object.values(this.form.controls).forEach(control => {
        control.markAsTouched();
      });
      return;
    }

    const feedbackForm=this.form.value;
    
    const feedbackData: FeedbackComplaint = {
      feedbackComplaintType: feedbackForm.feedbackType ? parseInt(feedbackForm.feedbackType) : 0,
      email: this.currentUser?.email ?? '',
      userName: this.currentUser?.firstname + " " + this.currentUser?.lastname,
      title: feedbackForm.title ?? '',
      userId: this.currentUser?.userId ?? 0,
      description: feedbackForm.description ?? '',
      complaintStatus: ComplaintStatus.Unresolved
    };

    if(this.form.get('feedbackType')?.value == '1'){
      feedbackData.complaintStatus=1
    }else{
      feedbackData.complaintStatus=2
    }

    this.feedbackComplaintService.createFeedbackComplaint(feedbackData).subscribe({
      next: () => {
        this.toastService.showSuccess(
          feedbackData.feedbackComplaintType === 1 ? 'Feedback Given Successfully' : 'Complaint Registered Successfully'
        );
        this.resetForm();
      },
      error: () => {
        this.toastService.showError('An error occurred while submitting your feedback or complaint.');
      }
    });
  }
  private resetForm() {
    this.form.reset({
      description: '',
      title: '',
      email: this.currentUser?.email || '',
      name: this.currentUser?.firstname || '',
      feedbackType: '1' // Default to Feedback
    });
  }
}
