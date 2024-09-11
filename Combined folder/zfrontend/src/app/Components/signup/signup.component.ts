import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { AuthService, User  } from '../../service/auth.service';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import Swal from 'sweetalert2';
import { LoginComponent } from '../login/login.component';


@Component({
  selector: 'app-signup',
  standalone:true,
  imports: [ReactiveFormsModule,CommonModule,RouterOutlet,HttpClientModule,RouterLink,LoginComponent],
  providers: [AuthService],
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {
  signupForm: FormGroup= <FormGroup>{} ;
  errorMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {}
  ngOnInit(): void {
    this.initializeForm();
  }
  private initializeForm(): void {
    this.signupForm = this.fb.group({
      name: ['', [Validators.required, Validators.pattern('^[a-zA-Z ]*$')]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]],
      confirmPassword: ['', Validators.required],
      address: ['', Validators.required],
      pincode: ['', [Validators.required, Validators.pattern('^[0-9]{6}$')]]
    }, { validators: this.passwordMatchValidator });
  }

  private passwordMatchValidator(form: FormGroup) {
    return form.get('password')?.value === form.get('confirmPassword')?.value
      ? null : { mismatch: true };
  }

  onSubmit(): void  {
    if (this.signupForm.valid) {
      const user: User = this.signupForm.value;
      this.authService.signup(user).subscribe(
        response => {
          Swal.fire('Success', 'Signup successful!', 'success');
          this.router.navigate(['/']);
        },
        error => {
          Swal.fire('Error', 'Signup failed. Please try again.', 'error');
        }
      );
    } else {
      Swal.fire('Error', 'Please fill all the fields correctly.', 'error');
    }
  }

  // redirectToLogin(): void  {
  //   this.router.navigate(['/']);
  // }
}
