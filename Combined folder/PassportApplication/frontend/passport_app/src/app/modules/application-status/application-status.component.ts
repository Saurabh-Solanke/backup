import { Component, inject, OnInit } from '@angular/core';
import { ApplicationFormService } from '../../services/applicationForm.service';
import { User } from '../../models/user.model';
import { FormsModule } from '@angular/forms';
import { NgClass } from '@angular/common';
import { Router } from '@angular/router';
import { MasterDetails } from '../../models/application.model';
import { ApplicationFormStatus } from '../../models/applicationstatus.model';

@Component({
  selector: 'app-application-status',
  standalone: true,
  imports: [NgClass],
  templateUrl: './application-status.component.html',
  styleUrl: './application-status.component.css'
})
export class ApplicationStatusComponent implements OnInit{

  private applicationFormService=inject(ApplicationFormService);
  applicationForms:[]=[]
  currentUser: User | null = null;
  applicationFee:number=0;
  applicationStatus:ApplicationFormStatus[]=[];
  private router=inject(Router)

  ngOnInit(): void {
    const currentUserJson = localStorage.getItem('currentUser');
    if (currentUserJson) {
      this.currentUser = JSON.parse(currentUserJson);
    }
    if (this.currentUser) {
      this.getApplicationStatus();
    } else {
      console.error('No current user found.');
    }
  }

  getStatusClass(status: string): string {
    switch (status) {
      case 'new':
        return 'status-new';
      case 'pending':
        return 'status-pending';
      case 'approved':
        return 'status-approved';
      case 'rejected':
        return 'status-rejected'
      default:
        return ''
  }

}
 getApplicationStatus(){

  if (!this.currentUser?.userId) {
    console.error('User ID is missing.');
    return;
  }

  this.applicationFormService.getApplicationStatusByUserId(this.currentUser.userId).subscribe({
    next: (data: ApplicationFormStatus[]) => {
      this.applicationStatus = data;
      console.log(this.applicationStatus)
    },
    error: (err: any) => {
      console.error('Error fetching application status:', err);
    }
  });
 }
getPaymentStatusClass(paymentStatus: string): string {
  return paymentStatus === 'unpaid' ? 'payment-unpaid' : 'payment-paid';
}

pay(applicationForm: MasterDetails): void {
  
  if(applicationForm.passportType===1){
    this.applicationFee=350
  }else{
    this.applicationFee=250
  }
  this.router.navigate(['payment',this.applicationFee,applicationForm.applicationNo])
}
}
