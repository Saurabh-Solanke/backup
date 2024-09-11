import { Component, DestroyRef, inject, OnInit, signal } from '@angular/core';
import {
  AbstractControl,
  FormArray,
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { UserService } from '../../../services/user.service';
import { User } from '../../../models/user.model';
import { ToastService } from '../../../services/toast.service';
import { AuthService } from '../../../services/auth.service';
import { CommonModule } from '@angular/common';


function equalValues(controlName1:string,controlName2:string){

  return(control:AbstractControl)=>{
    const val1=control.get(controlName1)?.value;
    const val2=control.get(controlName1)?.value;
  
    if(val1===val2){
      return null;
    }
    return{valuesNotEqual: true}
  };
 
}


@Component({
  selector: 'app-signup',
  standalone: true,
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.css',
  imports: [ReactiveFormsModule,CommonModule,RouterLink],
})
export class SignupComponent implements OnInit {
  
  myForm: FormGroup;
  uniqueEmail: boolean = false;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private toastService:ToastService
  ) {
    this.myForm = this.fb.group({
      firstName: ['', [Validators.required, Validators.pattern('[a-zA-Z]+')]],
      lastName: ['', [Validators.required, Validators.pattern('[a-zA-Z]+')]],
      email: ['', [Validators.required, Validators.email]],
      mobileNo: ['', [Validators.required, Validators.pattern('^[0-9]{10}$')]],
      password: ['', [Validators.required, Validators.minLength(8)]],
      confirmPassword: ['', [Validators.required, this.matchPassword('password')]],
      agree: [false, [Validators.requiredTrue]]
    });
  }

  ngOnInit(): void {}

  isFieldInvalid(field: string): boolean {
    const control = this.myForm.controls[field];
    return control.touched && control.invalid;
  }

  getErrorMessage(field: string, error: string): boolean {
    return this.myForm.controls[field].errors?.[error];
  }

  private matchPassword(password: string) {
    return (control: any) => {
      const passwordControl = this.myForm?.get('password');
      return control.value === passwordControl?.value ? null : { valuesNotEqual: true };
    };
  }

  get isequal(): boolean {
    return this.myForm.get('confirmPassword')?.value !== this.myForm.get('password')?.value;
  }
  onSubmit() {
    if (this.myForm.invalid) {
      return;
    }

    // Check if the email is unique
    this.authService.checkEmailExists(this.myForm.controls['email'].value).subscribe(exists => {
      this.uniqueEmail = exists;
      if (!exists) {
        // Create the payload for registration
        const userPayload = {
          email: this.myForm.controls['email'].value,
          password: this.myForm.controls['password'].value, 
          role: "User", 
          firstname: this.myForm.controls['firstName'].value,
          lastname: this.myForm.controls['lastName'].value,
          mobileNo: this.myForm.controls['mobileNo'].value
        };

        // Use AuthService to register the user
        this.authService.register(userPayload).subscribe(() => {
          this.toastService.showSuccess("Register Successfully")
          this.router.navigate(['/login']);
        }, error => {
          console.error('Signup error', error);
        });
      } else {
        console.log('Email already exists.');
      }
    });
  }

  onReset() {
    this.myForm.reset();
  }
}

