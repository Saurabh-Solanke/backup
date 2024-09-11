import { CommonModule } from '@angular/common';
import { Component, inject, NgModule, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { User } from '../../models/user.model';
import { Payment } from '../../models/payment.model';
import { ToastService } from '../../services/toast.service';
import { MasterDetails } from '../../models/application.model';
import { PaymentStatus } from '../../models/enums/enums';

@Component({
  selector: 'app-payment',
  standalone: true,
  imports: [RouterLink,FormsModule,CommonModule,ReactiveFormsModule],
  templateUrl: './payment.component.html',
  styleUrl: './payment.component.css'
})
export class PaymentComponent implements OnInit{

  months:string[]=[];
  years:string[]=[];
  applications: MasterDetails[] = [];
  email: string = '';
  currentUser: User | null = null;
  applicationFee:number=0
  private activatedRoute=inject(ActivatedRoute);
  private router=inject(Router)
  payments:Payment[]=[];
  private toastService=inject(ToastService);
  applicationId:number=0;

  constructor(){}


  paymentForm=new FormGroup({
    email:new FormControl({
      value:'',disabled:true
    }),
    cardHolderName:new FormControl(
      '',{validators:[Validators.required]}
    ),
    cardNumber:new FormControl(
      '',{validators:[Validators.required,Validators.minLength(12)]}
    ),
    expirationMM:new FormControl(
      '',{validators:[Validators.required]}
    ),
    expirationYY:new FormControl(
      '',{validators:[Validators.required]}
    ),
    cvv:new FormControl(
      '',{validators:[Validators.required,Validators.minLength(3)]}
    ),
    applicationFee:new FormControl({
      value:'',disabled:true
    })
  })
  ngOnInit(): void {
      
    this.activatedRoute.params.subscribe((params)=>{
      this.applicationFee=params['applicationFee']
      this.applicationId=params['applicationId']
    })

    const currentUserJson = localStorage.getItem('currentUser');
    if (currentUserJson) {
      this.currentUser = JSON.parse(currentUserJson);
      if (this.currentUser) {
        this.paymentForm.patchValue({
          email:this.currentUser.email,
          applicationFee:this.applicationFee.toString()
        })
      }
    }

    for(let i=1;i<=12;i++){
      this.months.push(i.toString().padStart(2,'0'))
    }

    const currentYear=new Date().getFullYear();
    for(let i=0;i<10;i++){
      this.years.push((currentYear+i).toString());
    }

  }

  loadPayments(){
    const paymentsData=localStorage.getItem('payments');

    if(paymentsData){
      this.payments=JSON.parse(paymentsData);
    }
  }

  onSubmit(){

    if(this.paymentForm.invalid){
      this.markAllFormControlsTouched();
      this.toastService.showWarning("Please fill all reuired Fields")
      return
    }

    const payment:Payment={
      paymentId: new Date().getTime(),
      applicationFee: this.applicationFee,
     // userId: this.currentUser?.userId,
      applicationType: 1,
      applicationId: this.applicationId,
      paymentDate: new  Date(),
      paymentStatus: 0
    }
    this.updateApplicationStatus();
    this.payments.push(payment);
    localStorage.setItem('payments',JSON.stringify(this.payments));
    this.toastService.showSuccess('Payment Successful')
    this.router.navigate(['application-status'])
  }

  loadApplication() {
    const applicationsData = localStorage.getItem('applicationForms');
    this.applications = applicationsData ? JSON.parse(applicationsData) : [];
  }

  updateApplicationStatus(){
    this.applications.forEach(application=>{
      if(application.applicationNo==this.applicationId){
        application.applicationStatus=1
      }
    })
    localStorage.setItem('applicationForms',JSON.stringify(this.applications));
  }
  markAllFormControlsTouched(){
    Object.values(this.paymentForm.controls).forEach((control)=>{
      control.markAsTouched();
    })
  }
}
