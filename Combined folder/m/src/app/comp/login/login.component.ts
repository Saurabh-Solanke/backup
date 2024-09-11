import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { AuthService } from '../../service_integration/auth.service';
import { CommonModule } from '@angular/common';

import { ToastrService } from 'ngx-toastr';
import { ILoggedInUser } from '../../interfaces_integration/user';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  standalone: true,
  imports:[ReactiveFormsModule,CommonModule,RouterOutlet,RouterLink],
  providers: [AuthService],
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm: FormGroup = <FormGroup>{};
  errorMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private toastr: ToastrService
  ) { }
  ngOnInit(): void  {
    this.loginForm = this.fb.group({
      userEmail: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

  /**
 * Method to handle the login form submission.
 * This method checks the form's validity, sends the login request, and handles the response.
 * 
 * @returns void
 */
  onSubmit(): void {
    if (this.loginForm.valid) {
      this.authService.login(this.loginForm.value).subscribe({
        next: (response: ILoggedInUser) => {
          if (response) {
            sessionStorage.setItem('loggedInUser', JSON.stringify(response));
            sessionStorage.setItem('name', response.userFullname);
            this.displayToast('Login Successful', 'Success', 'success');
            this.router.navigate(['/home']);
          }
        },
        error: (error) => {
          this.displayToast('Invalid email or password', 'Error', 'error');
        }
      });
    } else {
      this.displayToast('Please fill in all the fields correctly.', 'Error', 'error');
    }
  }
  
  
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
}
