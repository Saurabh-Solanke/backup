import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../../core/services/auth.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ILoggedInUser } from '../../../core/interfaces/user';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  
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
      email: ['', [Validators.required, Validators.email]],
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
    // console.log(this.loginForm.value)
    if (this.loginForm.valid) {
      this.authService.login(this.loginForm.value).subscribe({
        next: (response: ILoggedInUser) => {
          // if(response.isActive == true){
            if (response) {
              // sessionStorage.setItem('loggedInUser', JSON.stringify(response));
              console.log(response)
              sessionStorage.setItem('name', response.fullName);
              sessionStorage.setItem('userId', response.userId);
              sessionStorage.setItem('token', response.token);
              this.displayToast('Login Successful', 'Success', 'success');
              if(response.role=="User"){
                this.router.navigate(['/user/home']);
              }
              else{
                this.router.navigate(['/admin/home']);
              }
            }
          // }
          // else{
          //   this.displayToast('Your account is not active. Please contact us if you need any help', 'Error', 'error');
          // }
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
