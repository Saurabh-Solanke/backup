import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterOutlet } from '@angular/router';
import { AuthService } from '../../service_integration/auth.service';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-signup',
  standalone:true,
  imports: [ReactiveFormsModule,CommonModule,RouterOutlet,HttpClientModule,RouterModule],
  providers: [AuthService],
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})

export class SignupComponent {
  signupForm: FormGroup= <FormGroup>{} ;
  errorMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private toastr : ToastrService
  ) {}
  ngOnInit(): void {
    this.initializeSignupForm();
  }

  /**
   * This Function is created to initialize the formgroup.
   * This function will be called in the NgOnit method. It returns void. 
   */
  private initializeSignupForm(): void {
    this.signupForm = this.fb.group({
      userFullname: ['', [Validators.required]],
      userEmail: ['', [Validators.required, Validators.email]],
      userPassword: ['', [Validators.required, Validators.minLength(8)]],
      userMobileNo : ['', [Validators.required,Validators.minLength(10)]],
      confirmPassword: ['', Validators.required],
      userAddress: ['', Validators.required],
      userPincode: ['', [Validators.required]]
    }, { validators: this.passwordMatchValidator });
  }

  /**
 * Validator function to check if the password and confirmPassword fields match.
 * This function will be used in the FormGroup as a custom validator.
 * 
 * @param form - The FormGroup instance containing the form controls.
 * @returns null if the password and confirmPassword match; otherwise, an object with a 'mismatch' key.
 */
  private passwordMatchValidator(form: FormGroup) {
    return form.get('userPassword')?.value === form.get('confirmPassword')?.value
      ? null : { mismatch: true };
  }

  /**
 * Method to handle the signup form submission.
 * This method will be triggered when the user submits the form.
 * It checks the form's validity, sends the signup request, and handles the response.
 * 
 * @returns void
 */
  onSubmit(): void {
    console.log(this.signupForm)
    if (this.signupForm.valid) {
      const { confirmPassword, ...user } = this.signupForm.value;
      console.log(user);
      this.authService.signup(user).subscribe({
        next: response => {
          this.displayToast('Signup successful!', 'Success', 'success');
          this.router.navigate(['/login']);
        },
        error: error => {
          this.displayToast('Signup failed. Please try again.', 'Error', 'error');
        },
        complete: () => {
          console.log('Signup process completed.');
        }
      });
    } else {
      this.displayToast('Please fill all the fields correctly.', 'Error', 'error');
    }
  }
  
  
  /**
 * Method to display a toast notification.
 * This method shows a success or error message using Toastr with a progress bar and a specified duration.
 * 
 * @param message - The message to be displayed in the toast.
 * @param title - The title of the toast.
 * @param type - The type of toast notification ('success' or 'error').
 * @param duration - The duration for which the toast should be displayed (default is 2000 ms).
 * @returns void
 */
  displayToast(message: string, title: string, type: 'success' | 'error', duration: number = 2000): void {
    if (type === 'success') {
      this.toastr.success(message, title, {
        progressBar: true,
        timeOut: duration,
        progressAnimation: 'decreasing',
      });
    } else if (type === 'error') {
      this.toastr.error(message, title, {
        progressBar: true,
        timeOut: duration,
        progressAnimation: 'decreasing',
      });
    }
  }

  redirectToLogin(){
    this.router.navigate(['/login']);
  }
}
