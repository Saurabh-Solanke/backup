import { CommonModule, NgClass } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { from } from 'rxjs';
import {
  Address,
  ApplicantDetails,
  ApplicationDocument,
  EmergencyContact,
  FamilyDetails,
  MasterDetails,
  OtherDetails,
  ServiceRequired
} from '../../../models/application.model';
import { User } from '../../../models/user.model';
import { ToastService } from '../../../services/toast.service';
import { ApplicationFormService } from '../../../services/applicationForm.service';
import { StateEnum } from '../../../models/enums/enums';


// Function to map state name to the corresponding number
const stateNameToNumber = (stateName: string): number => {
  return StateEnum[stateName as keyof typeof StateEnum] || 0; // Default to 0 if not found
};

@Component({
  selector: 'app-section',
  standalone: true,
  imports: [RouterLink, NgClass, ReactiveFormsModule,CommonModule],
  templateUrl: './section.component.html',
  styleUrls: ['./section.component.css'],
})
export class SectionComponent implements OnInit {
  sectionNo: number = 1;
  selectedFilter: string = 'service_required';

  private activatedRoute = inject(ActivatedRoute);
  private router = inject(Router);

  private toastService=inject(ToastService)
  private applicationFormService=inject(ApplicationFormService);

  masterDetails:MasterDetails=<MasterDetails>{};
  currentSection = 1;
  currentUser: User | null = null;

  selectedFiles: { [key: string]: File | null } = {
    aadharCard: null,
    pancard: null,
    dobProof: null,
    photo: null,
  };
  filterMap: { [key: number]: string } = {
    1: 'service_required',
    2: 'applicant_details',
    3: 'family_details',
    4: 'residential_address',
    5: 'emergency_contact',
    6: 'other_details',
    7: 'upload_documents',
  };
  
  //validators
  form = new FormGroup({
    service_required: new FormGroup({
      applicationType: new FormControl('', Validators.required),
      pagesRequired: new FormControl('', Validators.required),
      validityReq: new FormControl('', Validators.required),
    }),
  
    applicant_details: new FormGroup({
      applicantFirstName: new FormControl('', Validators.required),
      applicantLastName: new FormControl('', Validators.required),
      alias: new FormControl('',Validators.required),
      aliasName:new FormControl(''),
      nameChanged: new FormControl('', Validators.required),
      changedName: new FormControl(''),
      dob: new FormControl('', Validators.required),
      placeOfBirth: new FormControl('', Validators.required),
      district: new FormControl(''),
      state: new FormControl(''),
      country: new FormControl(''),
      gender: new FormControl('', Validators.required),
      maritialStatus: new FormControl('', Validators.required),
      citizenshipBy: new FormControl('', Validators.required),
      pancard: new FormControl(''),
      voterId: new FormControl(''),
      employeeType: new FormControl('', Validators.required),
      organizationalName: new FormControl(''),
      govermentServent: new FormControl('', Validators.required),
      education: new FormControl('', Validators.required),
      nonECR: new FormControl('', Validators.required),
      distinguishingMark: new FormControl('', Validators.required),
      applicantEmail:new FormControl('',Validators.required),
      applicantMobileNo:new FormControl('',Validators.required),
      aadharcard: new FormControl('', [Validators.required, Validators.minLength(12), Validators.maxLength(12)]),
    }),
  
    family_details: new FormGroup({
      fathersFirstName: new FormControl('', Validators.required),
      fathersLastName: new FormControl('', Validators.required),
      mothersFirstName: new FormControl('', Validators.required),
      mothersLastName: new FormControl('', Validators.required),
      leagalGuardianFirstName: new FormControl(''),
      leagalGuardianLastName: new FormControl(''),
      spouceFirstName: new FormControl(''),
      spouceLastName: new FormControl(''),
      isMinor: new FormControl(''),
      fatherPassportNo: new FormControl(''),
      fatherNationality: new FormControl(''),
      motherPassportNo: new FormControl(''),
      motherNationality: new FormControl(''),
    }),
  
    residential_address: new FormGroup({
      tHouseNo: new FormControl('', Validators.required),
      tStreet: new FormControl('', Validators.required),
      tDistrict: new FormControl(''),
      tPoliceStation: new FormControl(''),
      tState: new FormControl('', Validators.required),
      tPincode: new FormControl('', [Validators.required,Validators.minLength(6)]),
      isPermanent: new FormControl('', Validators.required),

      HouseNo: new FormControl({ value: '', disabled: true }),
      Street: new FormControl({ value: '', disabled: true }),
      District: new FormControl({ value: '', disabled: true }),
      PoliceStation: new FormControl({ value: '', disabled: true }),
      State: new FormControl({ value: '', disabled: false }),
      Pincode: new FormControl({ value: '', disabled: true }),
    }),
  
    emergency_contact: new FormGroup({
      contactName: new FormControl('', Validators.required),
      mobile: new FormControl('', [Validators.required, Validators.pattern('^[0-9]{10}$')]),
      telephone: new FormControl('', Validators.pattern('^[0-9]{10}$')),
      email: new FormControl('', [Validators.required, Validators.email]),
      address: new FormControl('',Validators.required),
      city: new FormControl('',Validators.required),
      state: new FormControl('',Validators.required),
      pincode: new FormControl('',Validators.required),
      country: new FormControl('',Validators.required),
    }),
  
    passport_details: new FormGroup({
      applied_but_not_issued: new FormControl('', Validators.required),
      file_number: new FormControl(''),
      application_month_year: new FormControl(''),
      passport_office: new FormControl(''),
    }),
  
    other_details: new FormGroup({
      criminalConvictions: new FormControl('', Validators.required),
      refusedPassport: new FormControl('', Validators.required),
      impoundedPassport: new FormControl('', Validators.required),
      revokedPassport: new FormControl('', Validators.required),
      grantedCitizenship: new FormControl('', Validators.required),
      heldForeignPassport: new FormControl('', Validators.required),
      surrenderedIndianPassport: new FormControl('', Validators.required),
      appliedRenunciation: new FormControl('', Validators.required),
      passportSurrendered: new FormControl('', Validators.required),
      emergencyCertificate: new FormControl('', Validators.required),
      deported: new FormControl('', Validators.required),
      repatriated: new FormControl('', Validators.required),
      registeredMission: new FormControl('', Validators.required),
      registeredMissionName: new FormControl(''),
    
    }),
  
  
    upload_documents: new FormGroup({
      aadharCard: new FormControl('', Validators.required),
      pancard: new FormControl('', Validators.required),
      dobProof: new FormControl('', Validators.required),
      photo: new FormControl('', Validators.required),
    }),
  });
  
  
  ngOnInit(): void {
    this.activatedRoute.params.subscribe((params) => {
      this.sectionNo = +params['sectionNo'];
      this.updateSelectedFilter();
    });

    this.form.get('residential_address.isPermanent')?.valueChanges.subscribe((value) => {
      this.togglePermanentFields(value === 'true');
    });
  }


  togglePermanentFields(isPermanent: boolean) {
    const residentialAddress = this.form.get('residential_address') as FormGroup;
    if (isPermanent) {
      // Disable permanent fields and copy values from temporary fields
      residentialAddress.get('HouseNo')?.disable();
      residentialAddress.get('Street')?.disable();
      residentialAddress.get('District')?.disable();
      residentialAddress.get('PoliceStation')?.disable();
      residentialAddress.get('State')?.disable();
      residentialAddress.get('Pincode')?.disable();

      // Copy values to make fields valid
      residentialAddress.patchValue({
        HouseNo: residentialAddress.get('tHouseNo')?.value,
        Street: residentialAddress.get('tStreet')?.value,
        District: residentialAddress.get('tDistrict')?.value,
        PoliceStation: residentialAddress.get('tPoliceStation')?.value,
        State: residentialAddress.get('tState')?.value,
        Pincode: residentialAddress.get('tPincode')?.value,
      });
    } else {
      // Enable the permanent fields when isPermanent is false
      residentialAddress.get('HouseNo')?.enable();
      residentialAddress.get('Street')?.enable();
      residentialAddress.get('District')?.enable();
      residentialAddress.get('PoliceStation')?.enable();
      residentialAddress.get('State')?.enable();
      residentialAddress.get('Pincode')?.enable();
    }
  }
  selectFilter(filter: string, sectionNo: string): void {
    this.selectedFilter = filter;
    this.router.navigate(['application-form', 'section', sectionNo]);
  }

  onClickNext(sectionNo: number): void {
    const sectionName = this.filterMap[sectionNo];
    const currentGroup = this.form.get(`${sectionName}`) as FormGroup;
    console.log(currentGroup)
    if (currentGroup && currentGroup.valid) {
      this.saveSectionData(sectionName);
      this.currentSection = sectionNo + 1;
      this.router.navigate(['application-form', 'section', this.currentSection]);
    } else {
      console.log("Invalid Form");
      this.markFormGroupTouched(currentGroup);
    }
  }

  saveSectionData(sectionName: string): void {
    switch (sectionName) {
      case 'service_required': {
        const serviceRequiredFormValue = this.form.get('service_required')?.value;
        if (serviceRequiredFormValue) {
          const serviceRequired: ServiceRequired = {
            applicationType: serviceRequiredFormValue.applicationType ? parseInt(serviceRequiredFormValue.applicationType) : 0,
            pagesRequired: serviceRequiredFormValue.pagesRequired ? parseInt(serviceRequiredFormValue.pagesRequired) : 0,
            validityReq: serviceRequiredFormValue.validityReq ? parseInt(serviceRequiredFormValue.validityReq) : 0,
          };
          this.applicationFormService.createServiceRequired(serviceRequired).subscribe({
            next: (data) => {
              this.masterDetails.serviceRequiredId=data.serviceRequiredId;
              this.updateMasterDetailsInStorage()
              this.toastService.showSuccess('Service Required section saved successfully!')
            },
            error: (err) => this.toastService.showError('Error saving Service Required section!'),
          });
        } else {
          this.toastService.showError('Service Required section form data is invalid or incomplete!');
        }
        break;
      }
  
      case 'applicant_details': {
        const applicantDetailsFormValue = this.form.get('applicant_details')?.value;
        if (applicantDetailsFormValue) {
          const applicantDetails: ApplicantDetails = {
            applicantFirstName: applicantDetailsFormValue.applicantFirstName ?? '',
            applicantLastName: applicantDetailsFormValue.applicantLastName ?? '',
            dob: applicantDetailsFormValue.dob ? new Date(applicantDetailsFormValue.dob) : new Date(),
            gender: applicantDetailsFormValue.gender ? parseInt(applicantDetailsFormValue.gender) : 0,
            placeOfBirth: applicantDetailsFormValue.placeOfBirth ?? '',
            district: applicantDetailsFormValue.district ?? '',
            state: applicantDetailsFormValue.state ?? '',
            country: applicantDetailsFormValue.country ?? '',
            pancard: applicantDetailsFormValue.pancard ?? '',
            aadharcard: applicantDetailsFormValue.aadharcard ?? '',
            voterId: applicantDetailsFormValue.voterId ?? '',
            maritialStatus: applicantDetailsFormValue.maritialStatus ? parseInt(applicantDetailsFormValue.maritialStatus) : 0,
            citizenshipBy: applicantDetailsFormValue.citizenshipBy ? parseInt(applicantDetailsFormValue.citizenshipBy) : 0,
            education: applicantDetailsFormValue.education ? parseInt(applicantDetailsFormValue.education) : 0,
            employeeType: applicantDetailsFormValue.employeeType ? parseInt(applicantDetailsFormValue.employeeType) : 0,
            govermentServent: applicantDetailsFormValue.govermentServent === "true",
            organizationalName: applicantDetailsFormValue.organizationalName ?? '',
            nonECR: applicantDetailsFormValue.nonECR === "true",
            distinguishMark: applicantDetailsFormValue.distinguishingMark ?? '',
            nameChanged: applicantDetailsFormValue.nameChanged === "true",
            changedName: applicantDetailsFormValue.changedName ?? '',
            alias: applicantDetailsFormValue.alias === "true",
            aliasName: applicantDetailsFormValue.aliasName ?? '',
            applicantEmail:applicantDetailsFormValue.applicantEmail ?? '',
            mobileNo: applicantDetailsFormValue.applicantMobileNo ?? '',
            passportNo: ''
          };
          this.applicationFormService.createApplicantDetails(applicantDetails).subscribe({
            next: (data) =>{
              this.masterDetails.applicantDetailsTableID=data.applicantDetailsTableID
              this.updateMasterDetailsInStorage()
              this.toastService.showSuccess('Applicant Details section saved successfully!')
            },
            error: (err) => this.toastService.showError('Error saving Applicant Details section!'),
          });
        } else {
          this.toastService.showError('Applicant Details section form data is invalid or incomplete!');
        }
        break;
      }
  
      case 'family_details': {
        const familyDetailsFormValue = this.form.get('family_details')?.value;
        if (familyDetailsFormValue) {
          const familyDetails: FamilyDetails = {
            fathersFirstName: familyDetailsFormValue.fathersFirstName ?? '',
            fathersLastName: familyDetailsFormValue.fathersLastName ?? '',
            mothersFirstName: familyDetailsFormValue.mothersFirstName ?? '',
            mothersLastName: familyDetailsFormValue.mothersLastName ?? '',
            spouceFirstName: familyDetailsFormValue.spouceFirstName ?? undefined,
            spouceLastName: familyDetailsFormValue.spouceLastName ?? undefined,
            isMinor: familyDetailsFormValue.isMinor === "true",
            leagalGuardianFirstName: familyDetailsFormValue.leagalGuardianFirstName ?? undefined,
            leagalGuardianLastName: familyDetailsFormValue.leagalGuardianLastName ?? undefined,
          };
          this.applicationFormService.createFamilyDetails(familyDetails).subscribe({
            next: (data) =>{

              this.masterDetails.familyDetailsId=data.familyDetailsId
              this.updateMasterDetailsInStorage()
              this.toastService.showSuccess('Family Details section saved successfully!')
            }, 
            error: (err) => this.toastService.showError('Error saving Family Details section!'),
          });
        } else {
          this.toastService.showError('Family Details section form data is invalid or incomplete!');
        }
        break;
      }
  
      case 'residential_address': {
        const residentialAddressFormValue = this.form.get('residential_address')?.value;
        if (residentialAddressFormValue) {
          
          const isPermanent = residentialAddressFormValue.isPermanent === 'true';
          const tStateNumber = stateNameToNumber(residentialAddressFormValue.tState || '');
          const stateNumber = stateNameToNumber(
            residentialAddressFormValue.State || residentialAddressFormValue.tState || ''
          );
          const residentialAddress: Address = {
            tHouseNo: residentialAddressFormValue.tHouseNo ? parseInt(residentialAddressFormValue.tHouseNo) : 0,
            tStreet: residentialAddressFormValue.tStreet ?? '',
            tDistrict: residentialAddressFormValue.tDistrict ?? '',
            tPoliceStation: residentialAddressFormValue.tPoliceStation ?? '',
            tState: tStateNumber,
            tPincode: residentialAddressFormValue.tPincode ?? '',
            isPermanent: isPermanent,
          
            houseNo: isPermanent 
                    ? parseInt(residentialAddressFormValue.tHouseNo ?? '0') 
                    : parseInt(residentialAddressFormValue.HouseNo ?? '0'),
          
                    street: isPermanent 
                    ? residentialAddressFormValue.tStreet ?? '' 
                    : residentialAddressFormValue.Street ?? '',
                
                  district: isPermanent 
                    ? residentialAddressFormValue.tDistrict ?? '' 
                    : residentialAddressFormValue.District ?? '',
                
                  policeStation: isPermanent 
                    ? residentialAddressFormValue.tPoliceStation ?? '' 
                    : residentialAddressFormValue.PoliceStation ?? '',
                
                  state: isPermanent 
                    ? tStateNumber 
                    : stateNumber,
                
                  pincode: isPermanent 
                    ? residentialAddressFormValue.tPincode ?? '' 
                    : residentialAddressFormValue.Pincode ?? '',
          };
      
          this.applicationFormService.createAddress(residentialAddress).subscribe({
            next: (data) => {
              this.masterDetails.addressTableId = data.addressTableId;
              this.updateMasterDetailsInStorage();
              this.toastService.showSuccess('Residential Address section saved successfully!');
            },
            error: (err) => this.toastService.showError('Error saving Residential Address section!'),
          });
        } else {
          this.toastService.showError('Residential Address section form data is invalid or incomplete!');
        }
        break;
      }
  
      case 'emergency_contact': {
        const emergencyContactFormValue = this.form.get('emergency_contact')?.value;
        if (emergencyContactFormValue) {
          const emergencyContact: EmergencyContact = {
            contactName: emergencyContactFormValue.contactName ?? '',
            mobile: emergencyContactFormValue.mobile ?? '',
            email: emergencyContactFormValue.email ?? '',
            address: emergencyContactFormValue.address ?? '',
            city: emergencyContactFormValue.city ?? '',
            state: emergencyContactFormValue.state ?? '',
            pincode: emergencyContactFormValue.pincode ?? '',
            country: emergencyContactFormValue.country ?? '',
          };
          this.applicationFormService.createEmergencyContact(emergencyContact).subscribe({
            next: (data) =>{
              this.masterDetails.emergencyContactDetailsId=data.emergencyContactDetailsId
              this.updateMasterDetailsInStorage()
              this.toastService.showSuccess('Emergency Contact section saved successfully!')
            },
            error: (err) => this.toastService.showError('Error saving Emergency Contact section!'),
          });
        } else {
          this.toastService.showError('Emergency Contact section form data is invalid or incomplete!');
        }
        break;
      }
  
      case 'other_details': {
        const otherDetailsFormValue = this.form.get('other_details')?.value;
        if (otherDetailsFormValue) {
          const otherDetails: OtherDetails = {
            criminalConvictions: otherDetailsFormValue.criminalConvictions === 'true',
            refusedPassport: otherDetailsFormValue.refusedPassport === 'true',
            impoundedPassport: otherDetailsFormValue.impoundedPassport === 'true',
            revokedPassport: otherDetailsFormValue.revokedPassport === 'true',
            grantedCitizenship: otherDetailsFormValue.grantedCitizenship === 'true',
            heldForeignPassport: otherDetailsFormValue.heldForeignPassport === 'true',
            surrenderedIndianPassport: otherDetailsFormValue.surrenderedIndianPassport === 'true',
            appliedRenunciation: otherDetailsFormValue.appliedRenunciation === 'true',
            passportSurrendered: otherDetailsFormValue.passportSurrendered === 'true',
            emergencyCertificate: otherDetailsFormValue.emergencyCertificate === 'true',
            deported: otherDetailsFormValue.deported === 'true',
            repatriated: otherDetailsFormValue.repatriated === 'true',
            registeredMission: otherDetailsFormValue.registeredMission === 'true',
            registeredMissionName: otherDetailsFormValue.registeredMissionName ?? undefined,
            renunciation: false
          };
          this.applicationFormService.createOtherDetails(otherDetails).subscribe({
            next: (data) =>{
              this.masterDetails.otherDetailsId=data.otherDetailsId
              this.updateMasterDetailsInStorage()
              this.toastService.showSuccess('Other Details section saved successfully!')
            },
            error: (err) => this.toastService.showError('Error saving Other Details section!'),
          });
        } else {
          this.toastService.showError('Other Details section form data is invalid or incomplete!');
        }
        break;
      }
      default:
        console.error('Unknown section:', sectionName);
    }
  }
  
  getStatesList(): { key: string, value: number }[] {
    return Object.entries(StateEnum)
      .filter(([key, value]) => typeof value === 'number') // Ensure to get only the numerical values
      .map(([key, value]) => ({ key, value: value as number })); // Convert into an array of key-value pairs
  }
  
  convertToEnum(value: string | null): number {

    return value ? parseInt(value) : 0;
  }

  private base64ToUint8Array(base64: string): Uint8Array {
    const binaryString = atob(base64);
    const len = binaryString.length;
    const bytes = new Uint8Array(len);
    for (let i = 0; i < len; i++) {
      bytes[i] = binaryString.charCodeAt(i);
    }
    return bytes;
  }

  onFileSelected(event: any, type: string) {
    this.selectedFiles[type] = event.target.files[0];
  }

  private updateSelectedFilter(): void {
    this.selectedFilter = this.filterMap[this.sectionNo] || 'service_required';
  }

  markFormGroupTouched(formGroup: FormGroup): void {
    Object.values(formGroup.controls).forEach((control: AbstractControl) => {
      control.markAsTouched();
      if ((control as FormGroup).controls) {
        this.markFormGroupTouched(control as FormGroup);
      }
    });
  }

  private updateMasterDetailsInStorage(): void {
    const existingMasterDetailsJson = localStorage.getItem('masterDetails');

    const existingMasterDetails: MasterDetails = existingMasterDetailsJson
      ? JSON.parse(existingMasterDetailsJson)
      : {};
  
    this.masterDetails = {
      ...existingMasterDetails, 
      ...this.masterDetails,    
    };
  
    localStorage.setItem('masterDetails', JSON.stringify(this.masterDetails));
}

  onSubmit(): void {
    if (this.form.get('upload_documents')?.invalid) {
      console.log( "InVALID FORM",this.form)
      const currentSec=this.form.get('upload_documents') as FormGroup;
      this.markFormGroupTouched(currentSec);
      return;
    }

    const currentUserJson = localStorage.getItem('currentUser');
    if (currentUserJson) {
      this.currentUser = JSON.parse(currentUserJson);
     
    }

    const formData = new FormData();
    if (this.selectedFiles['aadharCard']) formData.append('aadharCard', this.selectedFiles['aadharCard']);
    if (this.selectedFiles['pancard']) formData.append('pancard', this.selectedFiles['pancard']);
    if (this.selectedFiles['dobProof']) formData.append('dobProof', this.selectedFiles['dobProof']);
    if (this.selectedFiles['photo']) formData.append('photo', this.selectedFiles['photo']);

    this.applicationFormService.createNewDocument(formData).subscribe({
      next: (data) =>{
        this.masterDetails.documentTableId=data.documentTableId
        this.updateMasterDetailsInStorage()
        this.toastService.showSuccess('Upload Documents section saved successfully!')
      } ,
      error: (err) => this.toastService.showError('Error saving Upload Documents section!'),
    });


    const masterDetailsJson = localStorage.getItem('masterDetails');
    if (masterDetailsJson) {
        this.masterDetails = JSON.parse(masterDetailsJson);
    }

    this.masterDetails.passportType=1;
    this.masterDetails.applicationStatus=1;
    this.masterDetails.paymentStatus=1;
    this.masterDetails.userId=this.currentUser?.userId ?? 0;
    
    this.updateMasterDetailsInStorage();
    this.applicationFormService.createApplication(this.masterDetails).subscribe({
      next:()=>{
        this.toastService.showSuccess("Form Submitted Successfully");
            console.log('Form Submitted Successfully',this.form);
            localStorage.removeItem('masterDetails');
            this.router.navigate(['application-status'])
      },
      error:(err)=>{
        this.toastService.showError("Error submitting the form!");
        console.error('Form submission failed', err);
      }
    })
  }
}
