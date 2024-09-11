import { CommonModule, NgClass } from '@angular/common';
import { Component, inject } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from '../../../models/user.model';
import { ApplicantInfo, AppointmentBooking, Declaration, DocsUpload, PrevPassportDetails, RenewalApplication, RenewalReason } from '../../../models/renewal.model';
import { ToastService } from '../../../services/toast.service';

@Component({
  selector: 'app-renew-application',
  standalone: true,
  imports: [ReactiveFormsModule,NgClass,CommonModule],
  templateUrl: './renewal-section.component.html',
  styleUrl: './renewal-section.component.css'
})
export class RenewalFormComponent {

  sectionNo: number = 1;
  private activatedRoute = inject(ActivatedRoute);
  private router = inject(Router);
  private toastService=inject(ToastService)
  userId:number | undefined;
  renewalApplicationForms: RenewalApplication[] = [];
  currentSection = 1;
  currentUser: User | null = null;

  selectedFilter: string = 'renewalReason';
  filterMap: { [key: number]: string } = {
    1: 'renewalReason',
    2: 'applicantInfo',
    3: 'prevPassportDetails',
    4: 'appointmentBooking',
    5: 'docsUpload',
    6: 'declaration'
  }

  
  renewalForm = new FormGroup({
    renewalReason : new FormGroup({
      ReasonForRenewal: new FormControl('',[Validators.required]),
      appearance: new FormControl(''),
      signature: new FormControl(''),
      name: new FormControl(''),
      surname: new FormControl(''),
      dob : new FormControl(''),
      spouse_name: new FormControl(''),
      address: new FormControl(''),
      delete_ECR : new FormControl(''),
      others: new FormControl('')
    }),
    applicantInfo: new FormGroup({
      first_name: new FormControl('',[Validators.required]),
      last_name: new FormControl('',[Validators.required]),
      date_of_birth: new FormControl('',[Validators.required]),
      email: new FormControl('',[Validators.required, Validators.email]),
      phone_number: new FormControl('',[Validators.required, Validators.pattern('[1-9][0-9]{9}')]),
    }),
    prevPassportDetails: new FormGroup({
      passport_number: new FormControl('',[Validators.required]),
      issue_date: new FormControl('',[Validators.required]),
    }),
    appointmentBooking: new FormGroup({
      appdocver: new FormControl('',[Validators.required]),
      apppolice: new FormControl('',[Validators.required]),
    }),
    docsUpload: new FormGroup({
      documents: new FormControl(''),
    }),
    declaration: new FormGroup({
      place: new FormControl('',[Validators.required]),
      applicant_date: new FormControl('',[Validators.required]),
      applicant_signature: new FormControl('',[Validators.required]),
    })
  })


  ngOnInit(){
    this.activatedRoute.params.subscribe((params) => {
      this.sectionNo = +params['sectionNo'];
      this.updateSelectedFilter();
    });
  }
  
  private updateSelectedFilter(): void {
    this.selectedFilter = this.filterMap[this.sectionNo] || 'renewalReason';
  }


  onClickNext(sectionNo: number): void {
    // Get the current section's form group
    const sectionName = this.filterMap[sectionNo - 1];

    const currentGroup = this.renewalForm.get(`${sectionName}`) as FormGroup; // Adjust this based on current section


    if (currentGroup && currentGroup.valid) {
      // If the current form group is valid, move to the next section
      console.log('Valid Section group');
      this.currentSection = sectionNo;
      this.router.navigate(['application-form', 'renewal-section', sectionNo]);
    } else {
      // Mark all controls as touched to trigger validation messages
      console.log('Invalid Section group');
      this.markFormGroupTouched(currentGroup);
    }
  }

  selectFilter(filter: string, sectionNo: string): void {
    this.selectedFilter = filter;
    this.router.navigate(['application-form', 'renewal-section', sectionNo]);
  }
 
  markFormGroupTouched(formGroup: FormGroup): void {
    Object.values(formGroup.controls).forEach((control: AbstractControl) => {
      control.markAsTouched();
      if ((control as FormGroup).controls) {
        this.markFormGroupTouched(control as FormGroup);
      }
    });
  }

  markAllFormGroupsTouched(): void {
    Object.values(this.renewalForm.controls).forEach((control) => {
      control.markAsTouched();
      if (control.controls) {
        this.markFormGroupTouched(control);
      }
    });
  }
  onSubmit(){
   
    if(this.renewalForm.invalid){
      this.markAllFormGroupsTouched();
      return;
    }

    const currentUserJson = localStorage.getItem('currentUser');
    if (currentUserJson) {
      this.currentUser = JSON.parse(currentUserJson);
      this.userId=this.currentUser?.userId ?? 0;//providing a default valaue if currenetuserid is null
    }else{
      this.userId=0;
    }

    const formValue:RenewalApplication={
      applicationNo:new Date().getTime(),
      applicationType: 'renewal',
      applicationStatus: 'Pending',
      paymentStatus: 'unpaid',

      renewalReason:this.renewalForm.value.renewalReason as RenewalReason,
      applicantInfo:this.renewalForm.value.applicantInfo as ApplicantInfo,
      prevPassportDetails:this.renewalForm.value.prevPassportDetails as PrevPassportDetails,
      appointmentBooking:this.renewalForm.value.appointmentBooking as AppointmentBooking,
      docsUpload:this.renewalForm.value.docsUpload as DocsUpload,
      declaration:this.renewalForm.value.declaration as Declaration,
      userId:this.userId
    };

    this.renewalApplicationForms.push(formValue);
    localStorage.setItem(
      'renewalApplicationForms',
      JSON.stringify(this.renewalApplicationForms)
    );

    this.toastService.showSuccess("Form Submitted Successfully")
    console.log('Renewal Form Submitted Successfully');
  }
}
