import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { AuthService, User } from '../../service/auth.service';
import { CommonModule } from '@angular/common';
import { SignupComponent } from '../signup/signup.component';
import { HttpClientModule } from '@angular/common/http';
import Swal from 'sweetalert2';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, SignupComponent, RouterOutlet, RouterLink, HttpClientModule],
  providers: [AuthService],
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup = <FormGroup>{};
  errorMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) { }
  ngOnInit(): void  {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

  onSubmit(): void  {
    if (this.loginForm.valid) {
      this.authService.login(this.loginForm.value).subscribe(
        (response: User) => {
          if (response && response.password === this.loginForm.value.password) {
            sessionStorage.setItem('loggedUserEmail', this.loginForm.value.email);
            Swal.fire('Success', 'Login successful!', 'success');
            this.router.navigate(['/start-exam'], { state: { username: response.name } });
          } else {
            Swal.fire('Error', 'Invalid email or password', 'error');
          }
        },
        error => {
          Swal.fire('Error', 'Invalid email or password', 'error'); // Handle invalid credentials here
        }
      );
    } else {
      Swal.fire('Error', 'Please fill in all the fields correctly.', 'error');
    }
  }

  // redirectToSignup(): void  {
  //   this.router.navigate(['/signup']);
  // }
}
