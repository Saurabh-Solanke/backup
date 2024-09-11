import { Component, DestroyRef, inject, OnInit, signal } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { of, debounceTime } from 'rxjs';
import { UserService } from '../../../services/user.service';
import { User } from '../../../models/user.model';
import { RefreshService } from '../../../services/refresh.service';
import Swal from 'sweetalert2';
import { CommonModule, NgIf } from '@angular/common';
import { ToastService } from '../../../services/toast.service';
import { AuthService } from '../../../services/auth.service';
import { LoggedInUser } from '../../../models/loggedinuser.model';




@Component({
  selector: 'app-login',
  standalone: true,
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'], 
  imports:[ReactiveFormsModule,CommonModule,RouterLink]
})
export class LoginComponent{

  myForm: FormGroup;
  invalidUser: boolean = false;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private toastService: ToastService
  ) {
    this.myForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]],
    });
  }

  get emailIsInvalid() {
    return (
      this.myForm.controls['email'].invalid &&
      this.myForm.controls['email'].touched
    );
  }

  get passwordIsInvalid() {
    return (
      this.myForm.controls['password'].invalid &&
      this.myForm.controls['password'].touched
    );
  }

  onSubmit() {
    if (this.myForm.invalid) {
      return;
    }


    const { email, password } = this.myForm.value;
   
   
      this.authService.login(email, password).subscribe(
        (response: LoggedInUser) => {

          if(response.role=='Admin'){
            this.router.navigate(['admin']);
          
          this.invalidUser = false;
          this.toastService.showSuccess('Admin Logged In');
          return
          }
          this.router.navigate(['user-home']);
          
          this.invalidUser = false;
          this.toastService.showSuccess('Login Successfull')
        },
        (error) => {
          console.error('Login failed', error);
        
          this.invalidUser = true;
        }
      );
    
  }
}
